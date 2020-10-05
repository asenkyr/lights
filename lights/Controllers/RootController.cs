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
        private readonly ScenesController _scenesController;
        private readonly GroupsController _groupsController;

        public RootController(LightsController lightsController, SensorsController sensorsController, ScenesController scenesController,
            GroupsController groupsController)
        {
            _lightsController = lightsController ?? throw new ArgumentNullException(nameof(lightsController));
            _sensorsController = sensorsController ?? throw new ArgumentNullException(nameof(sensorsController));
            _scenesController = scenesController ?? throw new ArgumentNullException(nameof(scenesController));
            _groupsController = groupsController ?? throw new ArgumentNullException(nameof(groupsController));
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

        [Command("scenes", "Control the sensors api.")]
        public Task ScenesAsync()
        {
            return _scenesController.ProcessAsync(Arguments);
        }

        [Command("groups", "Control the sensors api.")]
        public Task GroupsAsync()
        {
            return _groupsController.ProcessAsync(Arguments);
        }
    }
}
