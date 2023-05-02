using PlayFab.ClientModels;
using PlayFab;

namespace Umbreon.amethyst.net
{
    public class Signup
    {
        public string email, displayName, username, password;

        public Signup(string email, string displayName, string username, string password)
        {
            this.email = email;
            this.displayName = displayName;
            this.username = username;
            this.password = password;
        }

        public async Task signupPlayfab()
        {
            // https://titleId.playfabapi.com/Client/RegisterPlayFabUser
            var request = new RegisterPlayFabUserRequest()
            {
                Email = email, //"music_inme@hotmail.fr",
                DisplayName = displayName, //"Sushi",
                Username = username, // "sushi",
                Password = password, //"zmxn12;",
                TitleId = "AF2D6"
            };
            var response = await PlayFabClientAPI.RegisterPlayFabUserAsync(request);
            Console.WriteLine($"Signup result: {response.Error?.ErrorMessage} vs {response.Result?.Username}");
        }

    }
}
