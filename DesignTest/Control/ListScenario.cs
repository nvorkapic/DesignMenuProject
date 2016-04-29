using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace DesignTest
{
    public partial class MainPage : Page
    {

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title = "Temperature", ClassType = typeof(TemperaturePage) },
            new Scenario() { Title = "Pressure", ClassType = typeof(PressurePage) },
            new Scenario() { Title = "Humidity", ClassType = typeof(HumidityPage) },
            new Scenario() { Title = "Usage Calls", ClassType = typeof(UsagePage) },
            new Scenario() { Title = "Report", ClassType = typeof(ReportPage) },
            new Scenario() { Title = "Additional Applications", ClassType = typeof(AddAppPage) },

        };
    }

    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
