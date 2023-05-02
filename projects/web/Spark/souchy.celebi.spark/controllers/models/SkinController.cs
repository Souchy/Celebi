using Microsoft.AspNetCore.Mvc;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Route(Routes.Models + "skins")]
    public class SkinController
    {
        private readonly SkinService _skinService;
        public SkinController(SkinService skins) => _skinService = skins;
    }
}
