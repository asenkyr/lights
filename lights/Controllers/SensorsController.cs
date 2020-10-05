using System;
using System.Threading.Tasks;
using lights.api.Proxy;
using lights.Attributes;
using lights.PrettyPrinters;

namespace lights.Controllers
{
    class SensorsController : AbstractController
    {
        protected override string Usage
            => "Control sensors api.";

        private readonly SensorsProxy _proxy;

        public SensorsController(SensorsProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }


        [Command("ls", "List all accessible sensors.")]
        public async Task GetSensorsAsync()
        {
            var response = await _proxy.GetSensorsAsync();
            Console.WriteLine(SensorsPrettyPrinter.BasicInfo(response));
        }
    }
}
