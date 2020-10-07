using System;
using System.Threading.Tasks;
using lights.api.Models;
using lights.api.Proxy;
using lights.Attributes;
using lights.PrettyPrinters;

namespace lights.Controllers
{
    class LightsController : AbstractController
    {
        protected override string Usage
            => "Control lights api.";

        private readonly LightsProxy _proxy;

        public LightsController(LightsProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        [Command("ls", "List all accessible lights.")]
        public async Task GetLightsAsync()
        {
            var response = await _proxy.GetLightsAsync();
            Console.WriteLine(LightPrettyPrinter.BasicInfo(response));
        }

        [Command("toggle", "ToggleAsync light by it's ID.", "id")]
        public async Task ToggleAsync(string id)
        {
            await _proxy.ToggleAsync(new LightId(id));
        }

        [Command("on", "Turn on light by it's ID.", "id")]
        public async Task TurnOnAsync(string id)
        {
            var brightness = Arguments.PopValue<int?>("brightness");
            var temperature = Arguments.PopValue<int?>("temperature");
            await _proxy.SetStateAsync(new LightId(id), 
                new LightState {On = true, Brightness = brightness, ColorTemperature = temperature});
        }

        [Command("off", "Turn off light by it's ID.", "id")]
        public async Task TurnOffAsync(string id)
        {
            await _proxy.SetStateAsync(new LightId(id), new LightState { On = false });
        }
    }
}
