using MMEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MetroMan
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool MetroMap = true; //true:Metro false:Map

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Init App
            await LogicCommon.InitApp();
            //Init Map
            mapControl.MapServiceToken = "BhJcJKjKhTwjxaxKfnp7~kJHm7Fr49HPM55VRFnhAMQ~AtVurPSSvWUgixZBtLcI36pcxvXSVUqtqXXCmzK8J8_a1AkZvpT9wINOWSlFDr58";
            //Init Setting
            listView.ItemsSource = LogicCommon.SettingCityList;
            listView.SelectedIndex = LogicCommon.CityList.FindIndex(obj => obj.CD == LogicCommon.AppCity);
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void btnMetroMap_Click(object sender, RoutedEventArgs e)
        {
            MetroMap = !MetroMap;
            mapControl.SetValue(Canvas.ZIndexProperty, MetroMap ? 0 : 1);
            scrollViewer.SetValue(Canvas.ZIndexProperty, MetroMap ? 1 : 0);
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            splitView.IsPaneOpen = false;
            City city = LogicCommon.CityList[listView.SelectedIndex];
            //Reset Title
            tbTitle.Text = city.GetMainName(Helper.GetOSLang());
            //Reset Map
            mapControl.Center = new Geopoint(new BasicGeoposition() { Latitude = city.Latitude, Longitude = city.Longitude });
            mapControl.ZoomLevel = 12;
            //Reset Metro
            BitmapImage image = new BitmapImage(new Uri(string.Format("ms-appx:///Assets/routemap/routemap_{0}_{1}.png", city.CD, GetMetroLang())));
            image.ImageOpened += (pSender, pE) => {
                //Offset Metro
                int offsetX = (image.PixelWidth - (int)scrollViewer.ViewportWidth) / 2;
                int offsetY = (image.PixelHeight - (int)scrollViewer.ViewportHeight) / 2;
                if (offsetX < 0) offsetX = 0;
                if (offsetY < 0) offsetY = 0;
                //Task.Delay(1);
                scrollViewer.ChangeView(offsetX, offsetY, 1.0f);
            };
            metroControl.Source = image;
        }

        private string GetMetroLang()
        {
            string result = string.Empty;
            Language lang = Helper.GetOSLang();
            if (lang == MMEngine.Language.English) result = "en";
            else if (lang == MMEngine.Language.Simplified) result = "cn";
            else if (lang == MMEngine.Language.Traditional) result = "tw";
            else if (lang == MMEngine.Language.Japanese) result = "ja";
            else result = "en";
            return result;
        }

        private void canvasControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mapControl.Width = e.NewSize.Width;
            mapControl.Height = e.NewSize.Height;
            scrollViewer.Width = e.NewSize.Width;
            scrollViewer.Height = e.NewSize.Height;
        }
    }
}
