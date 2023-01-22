using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using workbench.Models.Enums;
using workbench.Util;

namespace workbench.Commands.GenerateCommand.AuthCommand
{
    public static class AuthCommandBuilder
    {
        private static Argument<EGenerateDataType> DataTypeArgument = new("data-type", "The data type to generate.");

        private static Option<EEnvironment> EnvironmentOption = new(new string[] {"-e","--environment"}, "Set the environment to generate the data.");
        private static Option<int> UserOption = new(new string[] {"-u","--userId"}, "Set the user to generate data.");
        private static Option<ERole> RoleOption = new(new string[] {"-r","--role"}, "set the role to generate data.");

        private static Command _command = CommandConstruct();
        public static Command GetCommand() => _command;


        private static Command CommandConstruct()
        {
            Command command = new("auth", "generate authentication data.");

            command.AddAlias("a");

            command.AddArgument(DataTypeArgument);

            command.AddOption(EnvironmentOption);
            command.AddOption(UserOption);
            command.AddOption(RoleOption);

            command.SetHandler((dataType, environment, userId, role) => {
                string tokenResult = dataType switch
                {
                    EGenerateDataType.JWT => "Bearer " + GenerateJWT(environment, userId, role),
                    _ => "" 
                };

                PrintResult(tokenResult);
                
            }, DataTypeArgument, EnvironmentOption, UserOption, RoleOption);

            return command;
        }

        private static void PrintResult(string result)
        {
            Console.WriteLine("The follow auth token was generated:\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(result);
            ClipboardManager.CopyToClipboard(result);
        }

        private static string GenerateJWT(EEnvironment environment, int userId, ERole role )
        {
            userId = ValidateUserId(userId, environment);
            TokenConfig tokenConfig = new(environment);

            SymmetricSecurityKey authSigningKey = new(Encoding.ASCII.GetBytes(tokenConfig.Secret));
            List<Claim> claims = GetClaims(userId, role);

            JwtSecurityToken token = new(
                issuer: tokenConfig.Issuer,
                expires: DateTime.UtcNow.AddHours(tokenConfig.ExpirerHour),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private static int ValidateUserId(int userId, EEnvironment environment)
        {
            if(userId == 0)
            {
                if(environment == EEnvironment.Hml)
                    userId = 2723344;
                else
                    userId = 401;
            }
            return userId;
        }

        private static List<Claim> GetClaims(int userId, ERole role)
            => new(){
                new Claim("uid", userId.ToString()),
                new Claim("role", (Enum.GetName<ERole>(role)?? "Client"))
            };
    }
}