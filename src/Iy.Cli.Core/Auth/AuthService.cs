using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Iy.Cli.Auth
{
    public class AuthService 
    {
        private const string UserName = "admin";
        private const string Password = "pass";

        public async Task LoginAsync(string userName, string password)
        {
            if (UserName != userName || Password != password)
            {
                return;
            }

            var accessToken = Guid.NewGuid().ToString();

            await File.WriteAllTextAsync(CliPaths.AccessToken, accessToken, Encoding.UTF8);
        }

        public Task LogoutAsync()
        {
            if (File.Exists(CliPaths.AccessToken))
            {
                File.Delete(CliPaths.AccessToken);
            }

            return Task.CompletedTask;
        }

        public static bool IsLoggedIn()
        {
            return File.Exists(CliPaths.AccessToken);
        }
    }
}
