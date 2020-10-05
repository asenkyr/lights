﻿using System;
using System.Threading.Tasks;
using lights.api.Proxy;
using lights.Attributes;
using lights.PrettyPrinters;

namespace lights.Controllers
{
    class RulesController : AbstractController
    {
        protected override string Usage
            => "Control scenes api.";

        private readonly RulesProxy _proxy;

        public RulesController(RulesProxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }


        [Command("ls", "List all accessible scenes.")]
        public async Task GetScenesAsync()
        {
            var response = await _proxy.GetScenesAsync();
            Console.WriteLine(RulesPrettyPrinter.BasicInfo(response));
        }
    }
}
