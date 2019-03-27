using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamForms.Controls;
using Xamarin.Forms;

namespace DanRevi
{
    public class CalendarContentPage : ContentPage
    {

        public CalendarContentPage()
        {
            //this.Padding = new Thickness(10, 10, 10, 10);
            StackLayout panel = new StackLayout();
            // panel.Spacing = 10;

            //panel.Children.Add(new TableView {  });
            // panel.Children.Add(new Label { Text = "hi" });
            var calendar = new Calendar();

            calendar.DateClicked += Calendar_DateClicked;

            calendar.BorderColor = Color.Gray;
            calendar.BorderWidth = 3;
            calendar.BackgroundColor = Color.PaleGoldenrod;
            calendar.StartDay = DayOfWeek.Sunday;
            calendar.StartDate = DateTime.Now;
            calendar.WeekdaysTextColor = Color.Black;

            calendar.SpecialDates = new List<SpecialDate>
            {
                new SpecialDate(DateTime.Now) { BackgroundColor = Color.Green, BorderColor = Color.Black, BorderWidth=4, Selectable = true }
            };

            panel.Children.Add(calendar);

            this.Content = panel;
        }

        private async void Calendar_DateClicked(object sender, DateTimeEventArgs e)
        {
            
            await Navigation.PushAsync(new SelectedDate(e));
        }
    }
}