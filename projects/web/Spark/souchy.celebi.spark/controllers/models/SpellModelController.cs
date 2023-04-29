using Microsoft.AspNetCore.Mvc;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Route(Routes.Models + "[controller]")]
    public class SpellModelController : ControllerBase
    {
        private readonly SpellModelService spellService;
        public SpellModelController(SpellModelService spells) => spellService = spells;
    }
}
