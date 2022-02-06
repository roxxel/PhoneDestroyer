using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDestroyer.Services
{
    public abstract class Service
    {
        public abstract string Name { get; }
        public abstract CountryCodes AvailableFor { get; }
        public abstract Task<bool> SendSmsAsync();
        public CountryCodes CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public HttpClient Client { get; set; }
        public bool LogDebug { get; set; }

        private int _delay;
        public bool CanExecute
        {
            get
            {
                var value = DateTimeOffset.Now.ToUnixTimeSeconds() > DelaySetTime + _delay;
                if (value)
                {
                    _delay = 0;
                    DelaySetTime = 0;
                }
                return value;
            }
        }
        public int NextExecutionAvailableIn
        {
            get
            {
                return (int)((DelaySetTime + _delay) - DateTimeOffset.Now.ToUnixTimeSeconds());
            }
        }
        public int Delay
        {
            get => _delay;
            set
            {
                _delay = value;
                DelaySetTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
        }
        public long DelaySetTime { get; private set; }
    }
}
