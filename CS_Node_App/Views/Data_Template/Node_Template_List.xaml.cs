using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace CS_Node_App.Views.Data_Template
{
    public partial class Node_Template_List : BaseTemplate
    {
        public Node_Template_List()
        {
            InitializeComponent();
            var entries = new[]
            {
                new ChartEntry(212)
                {
                    //Label = "UWP",
                    //ValueLabel = "112",
                    Color = SKColor.Parse("#4b86b4")
                },
                new ChartEntry(248)
                {
                    //Label = "Android",
                    //ValueLabel = "648",
                    Color = SKColor.Parse("#4b86b4")
                },
                new ChartEntry(128)
                {
                    //Label = "iOS",
                    //ValueLabel = "428",
                    Color = SKColor.Parse("#4b86b4")
                },
                new ChartEntry(514)
                {
                    //Label = "Forms",
                    //ValueLabel = "214",
                    Color = SKColor.Parse("#4b86b4")
                }
            };
            chartView.Chart = new LineChart
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent

            };
        }
    }
}
