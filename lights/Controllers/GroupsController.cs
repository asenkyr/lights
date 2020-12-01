using System;
using System.Threading.Tasks;
using lights.api.Models;
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
        public async Task GetGroupsAsync()
        {
            var response = await _proxy.GetGroupsAsync();
            Console.WriteLine(GroupsPrettyPrinter.BasicInfo(response));
        }

        [Command("on", "Turn on all lights in a group.", "id")]
        public Task TurnOnAsync(string id)
        {
            return _proxy.TurnOnAsync(new GroupId(id));
        }

        [Command("off", "Turn off all lights in a group.", "id")]
        public Task TurnOffAsync(string id)
        {
            return _proxy.TurnOffAsync(new GroupId(id));
        }

        [Command("set-scene", "Set group to a scene.", "id", "sceneId")]
        public Task SetSceneAsync(string id, string sceneId)
        {
            return _proxy.SetScene(new GroupId(id), new SceneId(sceneId));
        }
    }
}
