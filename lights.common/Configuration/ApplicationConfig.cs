
namespace lights.common.Configuration
{
    public class ApplicationConfig
    {
        public BridgeConfig BridgeConfig { get; set; }
    }

    public class BridgeConfig
    {
        public string HueBridgeUri { get; set; }
        public string UserName { get; set; }
    }
}
