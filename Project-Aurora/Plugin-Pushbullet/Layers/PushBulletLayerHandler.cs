using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Settings.Layers;
using System.Windows.Controls;
using Aurora.EffectsEngine;
using Aurora.Profiles;
using Aurora.Settings;
using Plugin_PushBullet.Models;
using Plugin_PushBullet.PushBullet;

namespace Plugin_PushBullet.Layers
{
    public class PushBulletLayerHandlerProperties : LayerHandlerProperties<PushBulletLayerHandlerProperties>
    {
        public PushBulletLayerHandlerProperties() : base()
        {
            ApplicationKeys = new Dictionary<MobileApplicationType, KeySequence>();
            ApplicationColors = new Dictionary<MobileApplicationType, Color>();
        }

        public PushBulletLayerHandlerProperties(bool assign_default = false) : base(assign_default) { }

        public Dictionary<MobileApplicationType, Color> ApplicationColors { get; set; }

        public Dictionary<MobileApplicationType, KeySequence> ApplicationKeys { get; set; }
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
            EffectLayer solidcolorLayer = new EffectLayer();




            solidcolorLayer = processRenderForApplication(solidcolorLayer, MobileApplicationType.Phone);
            solidcolorLayer = processRenderForApplication(solidcolorLayer, MobileApplicationType.Whatsapp);
            solidcolorLayer = processRenderForApplication(solidcolorLayer, MobileApplicationType.Email);
            solidcolorLayer = processRenderForApplication(solidcolorLayer, MobileApplicationType.Snapchat);
            solidcolorLayer = processRenderForApplication(solidcolorLayer, MobileApplicationType.Facebook);



            return solidcolorLayer;
        }

        private EffectLayer processRenderForApplication(EffectLayer solidcolorLayer, MobileApplicationType mobileApplicationType)
        {
            if (DataStream.ActiveNotificationsList.ContainsKey(mobileApplicationType))
            {
                if (DataStream.ActiveNotificationsList[mobileApplicationType].Count > 0)
                    solidcolorLayer.Set(Properties.ApplicationKeys[mobileApplicationType], Properties.ApplicationColors[mobileApplicationType]);
            }
            return solidcolorLayer;
        }


    }
}
