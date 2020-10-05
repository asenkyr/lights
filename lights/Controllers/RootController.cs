using System;
using System.Threading.Tasks;
using lights.Attributes;

namespace lights.Controllers
{
    class RootController : AbstractController
    {
        protected override string Usage
            => "This application allows control of the phillips hue bridge.";

        private readonly LightsController _lightsController;
        private readonly SensorsController _sensorsController;

        public RootController(LightsController lightsController, SensorsController sensorsController)
        {
            _lightsController = lightsController ?? throw new ArgumentNullException(nameof(lightsController));
            _sensorsController = sensorsController ?? throw new ArgumentNullException(nameof(sensorsController));
        }

        [Command("lights", "Control the light api.")]
        public Task LightsAsync()
        {
            return _lightsController.ProcessAsync(Arguments);
        }

        [Command("sensors", "Control the sensors api.")]
        public Task SensorsAsync()
        {
            return _sensorsController.ProcessAsync(Arguments);
        }
    }
}
