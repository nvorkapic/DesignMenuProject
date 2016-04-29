using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DesignTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemperaturePage : Page
    {
        public double ConvertedTemp;
        public string CurrentTemp;

        public string MaxValTemperature;
        public string MinValTemperature;

        public TemperaturePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            (Application.Current as DesignTest.App).SensorReader.Tick += SensorReader_Tick;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            (Application.Current as DesignTest.App).SensorReader.Tick -= SensorReader_Tick;
        }

        private void SensorReader_Tick(SenseHat.SensorReader reader, SenseHat.SensorData data)
        {
            ConvertedTemp = data.Temperature;
            CurrentTemp = string.Format("Temperature:\n{0:f2} °C" + ConvertedTemp);

            var items = from r in reader.Data
                        select new
                        {
                            Temperature = r.Temperature,
                            DTReading = r.Date.ToString("HH:mm:ss")
                        };
            foreach (var temp in items)
            {
                (Application.Current as DesignTest.App).MaxTemp = temp.Temperature > (Application.Current as DesignTest.App).MaxTemp ? temp.Temperature : (Application.Current as DesignTest.App).MaxTemp;
                (Application.Current as DesignTest.App).MinTemp = temp.Temperature < (Application.Current as DesignTest.App).MinTemp ? temp.Temperature : (Application.Current as DesignTest.App).MinTemp;
            }
            MaxValTemperature = "Maximal\nMeasured\nTemperature:\n" + string.Format("{0:f2} °C", (Application.Current as DesignTest.App).MaxTemp);
            MinValTemperature = "Minimal\nMeasured\nTemperature:\n" + string.Format("{0:f2} °C", (Application.Current as DesignTest.App).MinTemp);

            (TempChart.Series[0] as LineSeries).ItemsSource = items;
        }
    }
}
