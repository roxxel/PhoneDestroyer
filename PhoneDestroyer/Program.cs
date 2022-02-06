using PhoneDestroyer.Extensions;
using PhoneDestroyer.Utility;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;


var phoneNumber = ConsoleE.ReadLine("Номер телефона (без кода страны. Пример: 739865478): ");
var country = ConsoleE.ReadLine("Код страны (UA, RU и т.д): ");
var showDebug = ConsoleE.ReadLine("Показывать отладочную информацию? (Y/N): ").ToLower() == "y";
int repeatCount = 1;
int.TryParse(ConsoleE.ReadLine("Количество повторов (1 по умолчанию): "), out repeatCount);

var bomber = new Bomber(phoneNumber, country, repeatCount, showDebug);
bomber.InitializeServices();
await bomber.RunAsync();
await Task.Delay(-1);