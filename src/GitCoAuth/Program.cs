using System;
using System.Threading.Tasks;
using RestSharp;

namespace GitCoAuth {

    class Program {

        class UserProfile {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        async static Task<int> Main(string[] args) {
            if(args.Length != 1) {
                PrintHelp();
                return 1;
            }

            (var name, var email) = await GetUserData(args[0]);

            var output = string.Format("Co-authored-by: {0} <{1}>", name, email);

            try {
                await TextCopy.ClipboardService.SetTextAsync(output);
            }
            catch(Exception ex) {
                Console.Error.WriteLine("Failed to copy to clipboard ({0})", ex.Message);
            }
            Console.Out.WriteLine(output);

            return 0;
        }

        private static void PrintHelp() {
            Console.Error.WriteLine("  gitcoauth <username>");
            Console.Error.WriteLine("Prints out a commit message trailer that indicates that GitHub user <username> contributed to a commit (and copies it to clipboard, if possible).");
        }

        private static async Task<(string Name, string Email)> GetUserData(string username) {
            var client = new RestClient("https://api.github.com");

            try {
                var resp = await client.GetAsync<UserProfile>($"/users/{username}");

                return (
                    resp.Name ?? username,
                    resp.Email ?? GetDefaultEmail(username)
                );
            }
            catch (Exception ex) {
                Console.Error.WriteLine("Failed to get user data ({0})", ex.Message);
                return (username, GetDefaultEmail(username));
            }            
        }

        private static string GetDefaultEmail(string username) {
            return $"{username}@users.noreply.github.com";
        }

    }

}
