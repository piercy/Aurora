using Aurora.Utils;
using Plugin_PushBullet.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Plugin_PushBullet.Layers
{
    /// <summary>
    /// Interaction logic for Control_DefaultLayer.xaml
    /// </summary>
    public partial class Control_PushBullet : UserControl
    {
        private bool settingsset = false;
        

        public Control_PushBullet()
        {
            InitializeComponent();
        }

        public Control_PushBullet(PushBulletLayerHandler datacontext)
        {
            InitializeComponent();

            this.DataContext = datacontext;

        }

        public void SetSettings()
        {
           

            if(this.DataContext is PushBulletLayerHandler && !settingsset)
            {
                var dataContext = (this.DataContext as PushBulletLayerHandler);

                if (String.IsNullOrEmpty(dataContext.Properties.Settings.PushbulletAccessToken))
                {
                    this.lblHelp.Visibility = Visibility.Visible;
                    this.tbAccessToken.Visibility = Visibility.Visible;
                    this.lblAccessToken.Visibility = Visibility.Visible;
                    this.btnUpdateAccessToken.Visibility = Visibility.Visible;

                    this.lblColor.Visibility = Visibility.Collapsed;
                    this.lblComboBox.Visibility = Visibility.Collapsed;
                    this.lblKeys.Visibility = Visibility.Collapsed;
                    this.ComboBox_NotificationType.Visibility = Visibility.Collapsed;
                    this.Selected_keys.Visibility = Visibility.Collapsed;
                    this.ColorPicker_Color.Visibility = Visibility.Collapsed;
                }
                else
                {


                    foreach (var notificationTarget in dataContext.Properties.Settings.NotificationTargets)
                    {
                        this.ComboBox_NotificationType.Items.Add(new ComboBoxItem()
                        {
                            Content = notificationTarget.Name
                        });
                    }

                    this.ComboBox_NotificationType.SelectedValue = dataContext.Properties.SelectedApplication;
                    this.ColorPicker_Color.SelectedColor =
                        ColorUtils.DrawingColorToMediaColor(dataContext.Properties._PrimaryColor ??
                                                            System.Drawing.Color.Empty);
                    this.Selected_keys.Sequence = dataContext.Properties._Sequence;

                }

                settingsset = true;
            }
        }

        private void ColorPicker_Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (IsLoaded && settingsset && this.DataContext is PushBulletLayerHandler &&
                sender is Xceed.Wpf.Toolkit.ColorPicker &&
                (sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.HasValue)
            {

                (this.DataContext as PushBulletLayerHandler).Properties._PrimaryColor = ColorUtils.MediaColorToDrawingColor((sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.Value);
            }
        }

        private void Selected_keys_SequenceUpdated(object sender, EventArgs e)
        {
            if (IsLoaded && settingsset && this.DataContext is PushBulletLayerHandler && sender is Aurora.Controls.KeySequence)
                (this.DataContext as PushBulletLayerHandler).Properties._Sequence = (sender as Aurora.Controls.KeySequence).Sequence;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetSettings();

            this.Loaded -= UserControl_Loaded;
        }


        private void ComboBox_NotificationType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem) ComboBox_NotificationType.SelectedItem;

            (this.DataContext as PushBulletLayerHandler).Properties.SelectedApplication = item.Content?.ToString();

        }

        private void btnUpdateAccessToken_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as PushBulletLayerHandler).Properties.Settings.PushbulletAccessToken = tbAccessToken.Text;
            
            SaveSettings();
        }

        void SaveSettings()
        {
            var dataContext = (this.DataContext as PushBulletLayerHandler);

            if(dataContext != null)
                    File.WriteAllText(dataContext.Properties.SettingsSavePath, JsonConvert.SerializeObject(dataContext.Properties.Settings, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented }));
        }

    }
}
