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


        }

        public PushBulletLayerHandlerProperties(bool assign_default = false) : base(assign_default) { }

        
        public MobileApplicationType SelectedApplication { get; set; }

    }

    public class PushBulletLayerHandler : LayerHandler<PushBulletLayerHandlerProperties>
    {
        private float current_sine = 0.0f;

        private Color current_primary_color = Color.Transparent;
        private Color current_secondary_color = Color.Transparent;

        public PushBulletLayerHandler()
        {
            _ID = "PushBulletLayer";
        }

        protected override UserControl CreateControl()
        {
            return new Control_PushBullet(this);
        }

        public override EffectLayer Render(IGameState gamestate)
        {
            EffectLayer solidcolorLayer = new EffectLayer();

            


            solidcolorLayer = processRenderForApplication(solidcolorLayer, Properties.SelectedApplication);



            return solidcolorLayer;
        }

        private EffectLayer processRenderForApplication(EffectLayer breathing_layer, MobileApplicationType mobileApplicationType)
        {
            
            if (DataStream.ActiveNotificationsList.ContainsKey(mobileApplicationType))
            {
                if (DataStream.ActiveNotificationsList[mobileApplicationType].Count > 0)
                {
                    var effectSpeed = 3.0f;

                    current_sine = (float)Math.Pow(Math.Sin((double)((Aurora.Utils.Time.GetMillisecondsSinceEpoch() % 10000L) / 10000.0f) * 2 * Math.PI * effectSpeed), 2);

                    current_primary_color = Properties.PrimaryColor;

                    breathing_layer.Set(Properties.Sequence, Aurora.Utils.ColorUtils.BlendColors(current_primary_color, current_secondary_color, current_sine));

                }
            }
            return breathing_layer;
        }


    }
}
