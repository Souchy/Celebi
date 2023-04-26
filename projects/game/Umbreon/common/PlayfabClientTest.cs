using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Umbreon.common
{

    public class PlayfabClientTest
    {
        private static bool _running = true;

        private LoginResult logged;

        public async void thing()
        {
            PlayFabSettings.staticSettings.TitleId = "AF2D6"; // Please change this value to your own titleId from PlayFab Game Manager

            await signupPlayfab();
            await loginPlayfab();
            //await loginCustom();
            await getUserDetail();
            await getProfile();


            Console.WriteLine("Done! Press any key to close");
            Console.ReadKey(); // This halts the program and waits for the user
        }

        private async Task signupPlayfab()
        {
            // https://titleId.playfabapi.com/Client/RegisterPlayFabUser
            var request = new RegisterPlayFabUserRequest()
            {
                Email = "music_inme@hotmail.fr",
                DisplayName = "Sushi",
                Password = "zmxn12;",
                Username = "sushi",
                TitleId = "AF2D6"
            };
            var response = await PlayFabClientAPI.RegisterPlayFabUserAsync(request);
            Console.WriteLine($"Signup result: {response.Error?.ErrorMessage} vs {response.Result?.Username}");
        }

        private async Task loginPlayfab()
        {
            var req = new LoginWithPlayFabRequest()
            {
                Username = "sushi",
                Password = "zmxn12;",
                TitleId = "AF2D6"
            };
            var response = await PlayFabClientAPI.LoginWithPlayFabAsync(req);
            // playerAccountID in the gameTitle : response.Result?.AuthenticationContext.EntityId
            // playerAccountID in the Master account : response.Result?.PlayFabId
            this.logged = response.Result;
            Console.WriteLine($"Login result: {response.Error?.ErrorMessage} vs {response.Result?.PlayFabId}");
        }

        private async Task loginCustom()
        {
            var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
            var loginTask = PlayFabClientAPI.LoginWithCustomIDAsync(request);
            // If you want a synchronous result, you can call loginTask.Wait() - Note, this will halt the program until the function returns
            await loginTask;
            OnLoginComplete(loginTask);
        }

        private async Task getUserDetail()
        {
            var zxc = new GetUserDataRequest()
            {
                //PlayFabId = "A12AA7469923A5DC"
            };
            var res = await PlayFabClientAPI.GetUserDataAsync(zxc);
            Console.WriteLine("UserData values: " + string.Join(
                ", ",
                res.Result.Data.Select(p => p.Key + ":" + p.Value.Value)
            ));
        }

        private async Task getProfile()
        {
            var req = new GetPlayerProfileRequest()
            {
                //PlayFabId = "A12AA7469923A5DC"
            };
            var asd = await PlayFabClientAPI.GetPlayerProfileAsync(req);
            Console.WriteLine($"Player name: {asd.Result.PlayerProfile.DisplayName} {asd.Result.PlayerProfile.PlayerId}");
            //Console.WriteLine("Player name: " + asd.Result.Result.PlayerProfile.Locations[0].CountryCode + ", " + asd.Result.Result.PlayerProfile.Locations[0].City);
            //Console.WriteLine("Player name: " + asd.Result.Result.PlayerProfile.Origination);
        }

        private static void OnLoginComplete(Task<PlayFabResult<LoginResult>> taskResult)
        {
            var apiError = taskResult.Result.Error;
            var apiResult = taskResult.Result.Result;

            if (apiError != null)
            {
                Console.ForegroundColor = ConsoleColor.Red; // Make the error more visible
                Console.WriteLine("Something went wrong with your first API call.  :(");
                Console.WriteLine("Here's some debug information:");
                Console.WriteLine(PlayFabUtil.GenerateErrorReport(apiError));
                Console.ForegroundColor = ConsoleColor.Gray; // Reset to normal
            }
            else if (apiResult != null)
            {
                Console.WriteLine("Congratulations, you made your first successful API call!");
            }

            _running = false; // Because this is just an example, successful login triggers the end of the program
        }

    }
}
