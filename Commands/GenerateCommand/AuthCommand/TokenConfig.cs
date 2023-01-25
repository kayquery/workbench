using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workbench.Models.Enums;

namespace workbench.Commands.GenerateCommand.AuthCommand
{
    public class TokenConfig
    {
        public string Secret { get; }
        public string Issuer { get; }
        public int ExpirerHour { get; }

        public TokenConfig(EEnvironment environment)
        {
            if(environment == EEnvironment.Hml)
            {
                Secret = "SECRET-HML-SECRET-HML-SECRET-HML";
                Issuer = "ISSUER-HML";
                ExpirerHour = 72;
            }
            else
            {
                Secret = "SECRET-DEV-SECRET-DEV-SECRET-DEV";
                Issuer = "ISSUER-DEV";
                ExpirerHour = 72;
            }
        }
    }
}