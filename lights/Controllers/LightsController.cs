using System;
using System.Threading.Tasks;
using lights.api.Models;
using lights.api.PrettyPrinters;
using lights.api.Proxy;
using lights.Attributes;

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
            var (intId, _) = ParseId(id);
            await _proxy.ToggleAsync(intId);
        }

        [Command("on", "Turn on light by it's ID.", "id")]
        public async Task TurnOnAsync(string id)
        {
            var (intId, brigthness) = ParseId(id);
            await _proxy.SetStateAsync(intId, new LightState {On = true, Brightness = brigthness});
        }

        [Command("off", "Turn off light by it's ID.", "id")]
        public async Task TurnOffAsync(string id)
        {
            var (intId, _) = ParseId(id);
            await _proxy.SetStateAsync(intId, new LightState { On = false });
        }

        private (int id, int brightness) ParseId(string id)
        {
            if (!id.Contains(":"))
                return (int.Parse(id), 150);

            var parts = id.Split(":");
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}
