using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamForms.Controls;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DanRevi
{
    public class CalendarContentPage : ContentPage
    {
        private readonly HttpClient _client = new HttpClient(); //Creating a new instance of HttpClient. (Microsoft.Net.Http)
        //Deadlines
        private const string UrlDL = "http://danrevi.stuhrs.dk/api/deadlines"; //The link for Daniels API



        protected override async void OnAppearing()
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

            var deadlines = await GetDeadlines();

            calendar.SpecialDates = deadlines.Select(d => new SpecialDate(d.date) { BackgroundColor = Color.Blue, Selectable = true }).ToList();
            calendar.SpecialDates.Add(new SpecialDate(DateTime.Now) { BackgroundColor = Color.Green, BorderColor = Color.Black, BorderWidth = 4, Selectable = true });

            panel.Children.Add(calendar);

            this.Content = panel;

            base.OnAppearing();
        }

        //public CalendarContentPage()
        //{
            
        //}

        private async Task<ObservableCollection<Deadlines>> GetDeadlines()
        {
            string content = await _client.GetStringAsync(UrlDL); //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation
            List<Deadlines> deadlines = JsonConvert.DeserializeObject<List<Deadlines>>(content); //Deserializes or converts JSON String into a collection of Deadlines
            return new ObservableCollection<Deadlines>(deadlines); //Converting the List to ObservalbleCollection of Deadlines

        }

        private async void Calendar_DateClicked(object sender, DateTimeEventArgs e)
        {
            await Navigation.PushAsync(new SelectedDate(e));
        }
    }
}