using PlayFab.ClientModels;
using PlayFab;

namespace souchy.celebi.umbreon.amethyst.net
{
    public class Signin
    {
        public string username, password;

        public Signin(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public async Task loginPlayfab()
        {
            var req = new LoginWithPlayFabRequest()
            {
                Username = username, //"sushi",
                Password = password, //"zmxn12;",
                TitleId = "AF2D6"
            };
            var response = await PlayFabClientAPI.LoginWithPlayFabAsync(req);
            // playerAccountID in the gameTitle : response.Result?.AuthenticationContext.EntityId
            // playerAccountID in the Master account : response.Result?.PlayFabId
            //this.logged = response.Result;
            Console.WriteLine($"Login result: {response.Error?.ErrorMessage} vs {response.Result?.PlayFabId}");
        }

    }
}
