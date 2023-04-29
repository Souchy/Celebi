using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.spark.services.models;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    [ApiController]
    [Route(Routes.Models + "[controller]")]
    public class StatusModelController : ControllerBase
    {
        private readonly StatusModelService _statusService;
        public StatusModelController(StatusModelService statuses) => _statusService = statuses;
    }
}
