﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ESRI.ArcGIS.Client;
using MapControlLib;

namespace MapViewer_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MapControl mapControlLib = new MapControl();

        private readonly Dictionary<string, Action> voiceMap = new Dictionary<string, Action>();

        public MainWindow()
        {
            InitializeComponent();

            // Add voice commands and actions
            voiceMap.Add("Exit Application", () => Close());
            voiceMap.Add("Zoom In", () => MyMap.Zoom(2));
            voiceMap.Add("Zoom Out", () => MyMap.Zoom(0.5));
            voiceMap.Add("Street View", () => 
                            {
                                StreetsRadioButton.IsChecked = true;
                                ArcGISTiledMapServiceLayer arcgisLayer = MyMap.Layers["AGOLayer"] as ArcGISTiledMapServiceLayer;
                                arcgisLayer.Url = StreetsRadioButton.Tag as string;
                            });
            voiceMap.Add("Topo View", () => 
                            {
                                TopoRadioButton.IsChecked = true;
                                ArcGISTiledMapServiceLayer arcgisLayer = MyMap.Layers["AGOLayer"] as ArcGISTiledMapServiceLayer;
                                arcgisLayer.Url = TopoRadioButton.Tag as string;
                            });
            voiceMap.Add("Imagery View", () => 
                            {
                                BlendRadioButton.IsChecked = true;
                                ArcGISTiledMapServiceLayer arcgisLayer = MyMap.Layers["AGOLayer"] as ArcGISTiledMapServiceLayer;
                                arcgisLayer.Url = BlendRadioButton.Tag as string;
                            });
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            ArcGISTiledMapServiceLayer arcgisLayer = MyMap.Layers["AGOLayer"] as ArcGISTiledMapServiceLayer;
            arcgisLayer.Url = ((RadioButton)e.OriginalSource).Tag as string;
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            MyMap.Focus();

            mapControlLib.StartVoiceRecognition("Command", voiceMap);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            mapControlLib.StopVoiceRecognition();
        }
    }
}
