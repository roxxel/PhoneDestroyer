using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services
{
    [Flags]
    public enum CountryCodes
    {
        UA = 380,
        RU = 7,
        Any = ~0,
    }
}
