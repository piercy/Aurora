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

        private EffectLayer processRenderForApplication(EffectLayer solidcolorLayer, MobileApplicationType mobileApplicationType)
        {
            
            if (DataStream.ActiveNotificationsList.ContainsKey(mobileApplicationType))
            {
                if (DataStream.ActiveNotificationsList[mobileApplicationType].Count > 0)
                    solidcolorLayer.Set(Properties.Sequence, Properties.PrimaryColor);
            }
            return solidcolorLayer;
        }


    }
}
