using PlayFab;
using PlayFab.ClientModels;

namespace Umbreon.amethyst.net
{
    public class GetProfileData
    {

        public async Task getUserDetail()
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

        public async Task getProfile()
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


    }
}
