﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Aurora.Utils;
using Plugin_PushBullet.Models;

namespace Plugin_PushBullet.Layers
{
    /// <summary>
    /// Interaction logic for Control_DefaultLayer.xaml
    /// </summary>
    public partial class Control_ExampleLayer : UserControl
    {
        private bool settingsset = false;
        private MobileApplicationType CurrentNotificationType = MobileApplicationType.None;

        public Control_ExampleLayer()
        {
            InitializeComponent();
        }

        public Control_ExampleLayer(PushBulletLayerHandler datacontext)
        {
            InitializeComponent();

            this.DataContext = datacontext;
        }

        public void SetSettings()
        {
            if(this.DataContext is PushBulletLayerHandler && !settingsset)
            {
                this.ColorPicker_Color.SelectedColor = ColorUtils.DrawingColorToMediaColor((this.DataContext as PushBulletLayerHandler).Properties._PrimaryColor ?? System.Drawing.Color.Empty);
                this.Selected_keys.Sequence = (this.DataContext as PushBulletLayerHandler).Properties.;

                settingsset = true;
            }
        }

        private void ColorPicker_Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (IsLoaded && settingsset && this.DataContext is PushBulletLayerHandler &&
                sender is Xceed.Wpf.Toolkit.ColorPicker &&
                (sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.HasValue && CurrentNotificationType != MobileApplicationType.None)
            {

                (this.DataContext as PushBulletLayerHandler).Properties.ApplicationColors[CurrentNotificationType] = ColorUtils.MediaColorToDrawingColor((sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.Value);
            }
        }

        private void Selected_keys_SequenceUpdated(object sender, EventArgs e)
        {
            if (IsLoaded && settingsset && this.DataContext is PushBulletLayerHandler && sender is Aurora.Controls.KeySequence && CurrentNotificationType != MobileApplicationType.None)
                (this.DataContext as PushBulletLayerHandler).Properties.ApplicationKeys[CurrentNotificationType] = (sender as Aurora.Controls.KeySequence).Sequence;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetSettings();

            this.Loaded -= UserControl_Loaded;
        }


        private void ComboBox_NotificationType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem) ComboBox_NotificationType.SelectedItem;

            switch (item.Content?.ToString())
            {
                case "PhoneCall":
                    CurrentNotificationType = MobileApplicationType.Phone;
                    break;
                case "Whatsapp":
                    CurrentNotificationType = MobileApplicationType.Whatsapp;
                    break;
                case "Email":
                    CurrentNotificationType = MobileApplicationType.Email;
                    break;
                case "Snapchat":
                    CurrentNotificationType = MobileApplicationType.Snapchat;
                    break;
                case "Facebook":
                    CurrentNotificationType = MobileApplicationType.Facebook;
                    break;
            }
        }
    }
}
