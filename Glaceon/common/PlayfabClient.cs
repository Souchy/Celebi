using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Celebi.common
{

    public class PlayfabClient
    {
        private static bool _running = true;
        public async void thing()
        {

            PlayFabSettings.staticSettings.TitleId = "AF2D6"; // Please change this value to your own titleId from PlayFab Game Manager

            var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
            var loginTask = PlayFabClientAPI.LoginWithCustomIDAsync(request);
            // If you want a synchronous result, you can call loginTask.Wait() - Note, this will halt the program until the function returns

            await loginTask;
            OnLoginComplete(loginTask);

            var zxc = new GetUserDataRequest()
            {
                PlayFabId = "A12AA7469923A5DC"
            };
            var res = await PlayFabClientAPI.GetUserDataAsync(zxc);
            Console.WriteLine("UserData version: " + res.Result.DataVersion);


            var req = new GetPlayerProfileRequest()
            {
                PlayFabId = "A12AA7469923A5DC"
            };
            var asd = await PlayFabClientAPI.GetPlayerProfileAsync(req);
            Console.WriteLine("Player name: " + asd.Result.PlayerProfile.DisplayName);
            //Console.WriteLine("Player name: " + asd.Result.Result.PlayerProfile.Locations[0].CountryCode + ", " + asd.Result.Result.PlayerProfile.Locations[0].City);
            //Console.WriteLine("Player name: " + asd.Result.Result.PlayerProfile.Origination);

            Console.WriteLine("Done! Press any key to close");
            Console.ReadKey(); // This halts the program and waits for the user
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
