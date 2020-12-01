using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lights.api.Models;
using lights.api.Proxy;
using lights.Attributes;
using lights.PrettyPrinters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lights.Controllers
{
    class ScenesController : AbstractController
    {
        protected override string Usage
            => "Control scenes api.";

        private readonly ScenesProxy _scenesProxy;
        private readonly GroupsController _groupsController;
        private readonly GroupsProxy _groupsProxy;
        private readonly LightsController _lightsController;
        private readonly LightsProxy _lightsProxy;

        public ScenesController(ScenesProxy scenesProxy, GroupsController groupsController,
            LightsProxy lightsProxy, LightsController lightsController, GroupsProxy groupsProxy)
        {
            _scenesProxy = scenesProxy ?? throw new ArgumentNullException(nameof(scenesProxy));
            _groupsController = groupsController ?? throw new ArgumentNullException(nameof(groupsController));
            _lightsProxy = lightsProxy ?? throw new ArgumentNullException(nameof(lightsProxy));
            _lightsController = lightsController ?? throw new ArgumentNullException(nameof(lightsController));
            _groupsProxy = groupsProxy ?? throw new ArgumentNullException(nameof(groupsProxy));
        }


        [Command("ls", "List all accessible scenes.")]
        public async Task GetScenesAsync()
        {
            var response = await _scenesProxy.GetScenesAsync();
            Console.WriteLine(ScenesPrettyPrinter.BasicInfo(response));
        }

        [Command("create", "Creates new scene.")]
        public Task CreateSceneAsync()
        {
            if (!Arguments.PeekFlag(Constants.Arguments.Interactive))
            {
                Console.WriteLine("This command can be used only in interactive mode.");
                return Task.CompletedTask;
            }

            return CreateSceneInternalAsync();
        }

        [Command("delete", "Deletes scene by id.", "id")]
        public Task DeleteSceneAsync(string id)
        {
            return _scenesProxy.DeleteSceneAsync(new SceneId(id));
        }

        public async Task CreateSceneInternalAsync()
        {
            Console.Write("Enter scene name:");
            var sceneName = Console.ReadLine();
            if (string.IsNullOrEmpty(sceneName))
                return;
            Console.WriteLine();

            Console.WriteLine("Available groups:");
            await _groupsController.GetGroupsAsync();

            Console.Write("Select group for this scene: ");

            var groupIdstr = Console.ReadLine();

            if (string.IsNullOrEmpty(groupIdstr))
                return;

            var groupId = new GroupId(groupIdstr);
            var group = await _groupsProxy.GetGroupAsync(groupId);

            var lightStates = new Dictionary<LightId, LightState>();
            foreach (var id in group.Lights)
            {
                await _lightsController.SetStateAsyncInteractive(id);
                var light = await _lightsProxy.GetLightAsync(id);
                lightStates.Add(id, new LightState
                {
                    Brightness = light.State.Brightness,
                    ColorTemperature = light.State.ColorTemperature
                });
            }

            var scene = new Scene
            {
                Group = groupId,
                LightStates = lightStates,
                Name = sceneName,
                Type = SceneType.GroupScene
            };

            var responseId = await _scenesProxy.CreateScene(scene);
            if (responseId == null)
            {
                Console.WriteLine("Error creating scene.");
                return;
            }

            Console.WriteLine($"Created scene {responseId}.");

        }
    }
}
