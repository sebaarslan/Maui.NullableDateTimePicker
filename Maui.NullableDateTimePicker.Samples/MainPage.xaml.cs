﻿using Maui.NullableDateTimePicker.Enums;
using Maui.NullableDateTimePicker.Interfaces;
using Maui.NullableDateTimePicker.Models;
using Maui.NullableDateTimePicker.Modes;
using Maui.NullableDateTimePicker.Popup;

namespace Maui.NullableDateTimePicker.Samples
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            BindingContext = this;
            InitializeComponent();



            // Create Datetimepicker 
            CreateDateTimePickerProgrammatically();
        }

        DateTime? myDateTime = DateTime.Now;
        public DateTime? MyDateTime
        {
            get => myDateTime;
            set
            {
                myDateTime = value;
                OnPropertyChanged(nameof(MyDateTime));
            }
        }

        readonly FontImageSource calendarIcon = new()
        {
            Glyph = IconFont.CalendarDays,
            FontFamily = "FontAwesome",
            FontAutoScalingEnabled = true,
            Color = Colors.Black,
            Size = 20
        };
        public FontImageSource CalendarIcon => calendarIcon;

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void CreateDateTimePickerProgrammatically()
        {
            Maui.NullableDateTimePicker.NullableDateTimePicker datePicker = new()
            {
                Mode = PickerMode.Date,
                Format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern,
                ShowWeekNumbers = true,
                ShowOtherMonthDays = true,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };

            datePicker.SetAppThemeColor(Maui.NullableDateTimePicker.NullableDateTimePicker.ForeColorProperty, Microsoft.Maui.Graphics.Colors.Black, Microsoft.Maui.Graphics.Colors.White);
            datePicker.SetAppThemeColor(Maui.NullableDateTimePicker.NullableDateTimePicker.BackgroundColorProperty, Microsoft.Maui.Graphics.Colors.White, Microsoft.Maui.Graphics.Colors.Black);


            // binding
            datePicker.BindingContext = this;
            datePicker.SetBinding(Maui.NullableDateTimePicker.NullableDateTimePicker.NullableDateTimeProperty, nameof(MyDateTime), BindingMode.TwoWay);

            DateTimePlaceStackLayout.Add(datePicker);
        }

        private async void OpenCalendarButton_Clicked(object sender, EventArgs e)
        {
            INullableDateTimePickerOptions nullableDateTimePickerOptions = new NullableDateTimePickerOptions
            {
                InitDateTimeValue = MyDateTime,
                ShowClearButton = true,
                ShowWeekNumbers = true
            };

            INullableDateTimePickerPopup nullableDateTimePickerPopup = new NullableDateTimePickerPopup(nullableDateTimePickerOptions);
            
            var result = await nullableDateTimePickerPopup.OpenPopupAsync();
            if (result is PopupResult popupResult && popupResult.ButtonResult != PopupButtonResult.Cancel)
            {
                MyDateTime = popupResult.DateTimeResult;
            }
        }
    }

    static class IconFont
    {
        public const string Calendar = "\uf133";
        public const string CalendarDay = "\uf783";
        public const string CalendarDays = "\uf073";
    }
}