using System;
using System.Threading.Tasks;
using lights.api.Proxy;
using lights.Attributes;
using lights.PrettyPrinters;

namespace lights.Controllers
{
    class GroupsController : AbstractController
    {
        protected override string Usage
            => "Control scenes api.";

        private readonly GroupsProxy _proxy;

        public GroupsController(GroupsProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }


        [Command("ls", "List all accessible scenes.")]
        public async Task GetScenesAsync()
        {
            var response = await _proxy.GetGroupsAsync();
            Console.WriteLine(GroupsPrettyPrinter.BasicInfo(response));
        }
    }
}
