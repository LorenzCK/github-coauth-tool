using System;
using RestSharp;

namespace GitCoAuth {

    class Program {

        static int Main(string[] args) {
            if(args.Length != 1) {
                PrintHelp();
                return 1;
            }

            (var name, var email) = GetUserData(args[0]);

            var output = string.Format("Co-authored-by: {0} <{1}>", name, email);

            try {
                TextCopy.Clipboard.SetText(output);
            }
            catch(Exception ex) {
                Console.Error.WriteLine("Failed to copy to clipboard ({0})", ex.Message);
            }
            Console.Out.WriteLine(output);

            return 0;
        }

        private static void PrintHelp() {
            Console.Error.WriteLine("  gitcoauth <username>");
            Console.Error.WriteLine("Prints out a commit message trailer that indicates that GitHub user <username> contributed to a commit (and copied it to clipboard, if possible).");
        }

        private static (string Name, string Email) GetUserData(string username) {
            var client = new RestClient("https://api.github.com");
            var req = new RestRequest($"/users/{username}", Method.GET, DataFormat.Json);
            var resp = client.Execute<UserProfile>(req);

            if (!resp.IsSuccessful) {
                Console.Error.WriteLine("Failed to get user data");
                return (username, GetDefaultEmail(username));
            }

            return (
                resp.Data.Name ?? username,
                resp.Data.Email ?? GetDefaultEmail(username)
            );
        }

        private static string GetDefaultEmail(string username) {
            return $"{username}@users.noreply.github.com";
        }

    }

}
