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
            var brightness = Arguments.TakeValue<int?>(Constants.Arguments.Brightness);
            var temperature = Arguments.TakeValue<int?>(Constants.Arguments.Temperature);
            var transitionTime = Arguments.TakeValue<int?>(Constants.Arguments.TransitionTime);
            await _proxy.SetStateAsync(new LightId(id), 
                new LightState
                {
                    On = true, 
                    Brightness = brightness, 
                    ColorTemperature = temperature,
                    TransitionTime = transitionTime
                });
        }

        [Command("set", "Set light state by it's ID.", "id")]
        public Task SetStateAsync(string id)
        {
            var interactive = Arguments.PeekFlag(Constants.Arguments.Interactive);
            if (interactive)
                return SetStateAsyncInteractive(new LightId(id));
            return SetStateAsyncInternal(new LightId(id));
        }

        private Task SetStateAsyncInternal(LightId id)
        {
            var brightness = Arguments.TakeValue<int?>(Constants.Arguments.Brightness);
            var temperature = Arguments.TakeValue<int?>(Constants.Arguments.Temperature);
            var transitionTime = Arguments.TakeValue<int?>(Constants.Arguments.TransitionTime);
            return _proxy.SetStateAsync(id,
                new LightState
                {
                    Brightness = brightness,
                    ColorTemperature = temperature,
                    TransitionTime = transitionTime
                });
        }

        private async Task SetStateAsyncInteractive(LightId id)
        {
            Console.WriteLine("Use arrow keys to adjust brigtnes and temperature. Use 'q' to quit.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        await _proxy.SetStateAsync(id,
                            new LightState { BrightnessIncrement = 25, TransitionTime = 1 });
                        break;

                    case ConsoleKey.DownArrow:
                        await _proxy.SetStateAsync(id,
                            new LightState { BrightnessIncrement = -25, TransitionTime = 1 });
                        break;
                    case ConsoleKey.LeftArrow:
                        await _proxy.SetStateAsync(id,
                            new LightState { ColorTemperatureIncrement = -25, TransitionTime = 1 });
                        break;

                    case ConsoleKey.RightArrow:
                        await _proxy.SetStateAsync(id,
                            new LightState { ColorTemperatureIncrement = 25, TransitionTime = 1 });
                        break;
                }
            } while (key.Key != ConsoleKey.Q);
        }

        [Command("off", "Turn off light by it's ID.", "id")]
        public async Task TurnOffAsync(string id)
        {
            await _proxy.SetStateAsync(new LightId(id), new LightState { On = false });
        }
    }
}
