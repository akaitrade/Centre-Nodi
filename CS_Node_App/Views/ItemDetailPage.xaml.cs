using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using CS_Node_App.Models;
using CS_Node_App.ViewModels;
using SkiaSharp;

namespace CS_Node_App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
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
            chartView.Chart = new LineChart {
                Entries = entries,
                BackgroundColor = SKColors.Transparent

        };
            BindingContext = this.viewModel = viewModel;
        }


        public ItemDetailPage()
        {
            InitializeComponent();
            
            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };
            
            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}