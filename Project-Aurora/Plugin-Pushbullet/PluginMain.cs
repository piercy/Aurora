using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Profiles;
using Aurora.Settings;
using Plugin_PushBullet.Layers;
using Plugin_PushBullet.PushBullet;

namespace Plugin_PushBullet
{
    public class PluginMain : IPlugin
    {
        public string ID { get; private set; } = "PushBulletPlugin";

        public string Title { get; private set; } = "PushBullet Plugin";

        public string Author { get; private set; } = "Piercy";

        public Version Version { get; private set; } = new Version(0, 1);

        private IPluginHost pluginHost;

        public IPluginHost PluginHost { get { return pluginHost; }
            set {
                pluginHost = value;
                //Add stuff to the plugin manager
            }
        }

        public PluginMain()
        {
            DataStream.StartListening();
            
        }

        public void ProcessManager(object manager)
        {
            if (manager is LightingStateManager)
            {
                ((LightingStateManager)manager).RegisterLayerHandler(new LayerHandlerEntry("PushBulletLayer", "PushBullet Layer", typeof(PushBulletLayerHandler)));
            }
        }
    }
}
