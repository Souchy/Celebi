using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums.characteristics;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Route(Routes.Models + "[controller]")]
    public class EnumsController
    {

        [HttpGet("characteristicCategory")]
        public CharacteristicCategory[] GetCharacteristicCategories()
        {
            return Enum.GetValues<CharacteristicCategory>();
        }




    }
}
