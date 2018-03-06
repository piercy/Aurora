using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Settings.Layers;
using System.Windows.Controls;
using Aurora.EffectsEngine;
using Aurora.Profiles;
using Plugin_Pushbullet.Pushbullet;

namespace Plugin_Example.Layers
{
    public class ExampleLayerHandlerProperties : LayerHandlerProperties<ExampleLayerHandlerProperties>
    {

    }

    public class ExampleLayerHandler : LayerHandler<LayerHandlerProperties>
    {
        public ExampleLayerHandler()
        {
            _ID = "ExampleLayer";
        }

        protected override UserControl CreateControl()
        {
            return new Control_ExampleLayer(this);
        }

        public override EffectLayer Render(IGameState gamestate)
        {
            EffectLayer solidcolor_layer = new EffectLayer();

            //if(DateTime.Now.Second > 0 && DateTime.Now.Second < 30)
            if(DataStream.Calls.Count > 0)
                solidcolor_layer.Set(Properties.Sequence, Properties.PrimaryColor);
            



            return solidcolor_layer;
        }
    }
}
