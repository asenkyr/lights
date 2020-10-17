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
        private readonly LightsController _lightsController;
        private readonly LightsProxy _lightsProxy;

        public ScenesController(ScenesProxy scenesProxy, LightsController lightsController,
            LightsProxy lightsProxy)
        {
            _scenesProxy = scenesProxy ?? throw new ArgumentNullException(nameof(scenesProxy));
            _lightsController = lightsController ?? throw new ArgumentNullException(nameof(lightsController));
            _lightsProxy = lightsProxy ?? throw new ArgumentNullException(nameof(lightsProxy));
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
            Console.WriteLine("Available lights:");
            await _lightsController.GetLightsAsync();

            Console.Write("Enter scene name:");
            var sceneName = Console.ReadLine();
            if (string.IsNullOrEmpty(sceneName))
                return;
            Console.WriteLine();

            Console.Write("List light id's to include in this scene: ");
            var lightIds = Console.ReadLine()
                ?.Split(" ")
                .Where(str => !string.IsNullOrEmpty(str))
                .Select(str => new LightId(str))
                .ToArray();

            if (lightIds == null || lightIds.Length == 0)
                return;

            var lightStates = new Dictionary<LightId, LightState>();
            foreach (var id in lightIds)
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
                Lights = lightIds,
                LightStates = lightStates,
                Name = sceneName,
                Type = SceneType.LightScene,
            };

            var responseId = await _scenesProxy.CreateScene(scene);
            var stri = JsonConvert.SerializeObject(scene);
            if (responseId == null)
            {
                Console.WriteLine("Error creating scene.");
                return;
            }

            Console.WriteLine($"Created scene {responseId}.");

        }
    }
}
