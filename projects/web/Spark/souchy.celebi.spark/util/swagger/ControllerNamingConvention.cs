using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace souchy.celebi.spark.util.swagger
{
    public class ControllerNamingConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller == null)
                return;

            controller.ControllerName = controller.ControllerType.Name;
        }
    }
}
