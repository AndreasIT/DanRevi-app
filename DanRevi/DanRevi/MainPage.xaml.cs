using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DanRevi
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://danrevi-api.azurewebsites.net/api/deadlines"; //The link for Daniels API
        private readonly HttpClient _client = new HttpClient(); //Creating a new instance of HttpClient. (Microsoft.Net.Http)
        private ObservableCollection<Deadlines> _deadlines; //Refreshing the state of the UI in realtime when updating the ListView's Collection

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }
        /// <inheritdoc />
        /// <summary>
        /// This method gets called before the UI appears.
        /// Async and await to get the value of the Task and for user experience
        /// </summary>
        protected override async void OnAppearing()
        {
            string content = await _client.GetStringAsync(Url); //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation
            List<Deadlines> deadlines = JsonConvert.DeserializeObject<List<Deadlines>>(content); //Deserializes or converts JSON String into a collection of Deadlines
            _deadlines = new ObservableCollection<Deadlines>(deadlines); //Converting the List to ObservalbleCollection of Deadlines
            DeadlinesListView.ItemsSource = _deadlines; //Assigning the ObservableCollection to DeadlinesListView in the XAML of this file
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarContentPage());
        }
    }

    public class DataClassMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is Deadlines)
            {
                Deadlines data = values[0] as Deadlines;
                return String.Format("{0} {1}", data.name, data.description);
            }
            else { return ""; }
        }
    }

    public interface IMultiValueConverter
    {
        object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture);
    }
}
