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
        private readonly RulesController _rulesController;

        public RootController(LightsController lightsController, SensorsController sensorsController, ScenesController scenesController,
            GroupsController groupsController, RulesController rulesController)
        {
            _lightsController = lightsController ?? throw new ArgumentNullException(nameof(lightsController));
            _sensorsController = sensorsController ?? throw new ArgumentNullException(nameof(sensorsController));
            _scenesController = scenesController ?? throw new ArgumentNullException(nameof(scenesController));
            _groupsController = groupsController ?? throw new ArgumentNullException(nameof(groupsController));
            _rulesController = rulesController ?? throw new ArgumentNullException(nameof(rulesController));
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

        [Command("scenes", "Control the scenes api.")]
        public Task ScenesAsync()
        {
            return _scenesController.ProcessAsync(Arguments);
        }

        [Command("groups", "Control the groups api.")]
        public Task GroupsAsync()
        {
            return _groupsController.ProcessAsync(Arguments);
        }

        [Command("rules", "Control the rules api.")]
        public Task RulesAsync()
        {
            return _rulesController.ProcessAsync(Arguments);
        }
    }
}
