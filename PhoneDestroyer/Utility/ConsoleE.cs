using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDestroyer.Extensions;

public static class ConsoleE
{
    public static string ReadLine(string text = null)
    {
        if (text != null)
            Console.Write(text);
        return Console.ReadLine();
    }
}
