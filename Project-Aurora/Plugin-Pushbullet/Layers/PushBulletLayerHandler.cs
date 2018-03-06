using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Settings.Layers;
using System.Windows.Controls;
using Aurora.EffectsEngine;
using Aurora.Profiles;
using Aurora.Settings;
using Plugin_PushBullet.PushBullet;

namespace Plugin_PushBullet.Layers
{
    public class PushBulletLayerHandlerProperties : LayerHandlerProperties2Color<PushBulletLayerHandlerProperties>
    {
        public PushBulletLayerHandlerProperties() : base() { }

        public PushBulletLayerHandlerProperties(bool assign_default = false) : base(assign_default) { }
    }

    public class PushBulletLayerHandler : LayerHandler<PushBulletLayerHandlerProperties>
    {
        public PushBulletLayerHandler()
        {
            _ID = "PushBulletLayer";
        }

        protected override UserControl CreateControl()
        {
            return new Control_ExampleLayer(this);
        }

        public override EffectLayer Render(IGameState gamestate)
        {
            EffectLayer solidcolor_layer = new EffectLayer();

            //if(DateTime.Now.Second > 0 && DateTime.Now.Second < 30)
            if (DataStream.Calls.Count > 0)
            {
                //Console.WriteLine("Call Count Greater than zero");

                solidcolor_layer.Set(Properties.Sequence, Properties.PrimaryColor);
            }
            else
            {
                solidcolor_layer.Set(Properties.Sequence, Properties.SecondaryColor);
            }
            




            return solidcolor_layer;
        }
    }
}
