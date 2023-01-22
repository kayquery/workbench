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
                Secret = "8ue4iwNl3oc8u0gvBGPPq5obtcq5xV8Vxkgp";
                Issuer = "PrivacyHom";
                ExpirerHour = 72;
            }
            else
            {
                Secret = "8dcb743a98d0482e8f2aa78ca128e986e63f498ebf2f4ff8";
                Issuer = "PrivacyDev";
                ExpirerHour = 72;
            }
        }
    }
}