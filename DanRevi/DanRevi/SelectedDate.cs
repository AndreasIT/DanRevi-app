using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamForms.Controls;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DanRevi
{
	public class SelectedDate : ContentPage
	{
         private readonly HttpClient _client = new HttpClient(); //Creating a new instance of HttpClient. (Microsoft.Net.Http)
         private const string UrlDL = "http://danrevi.stuhrs.dk/api/deadlines";
         private ObservableCollection<Deadlines> deadlines; //Refreshing the state of the UI in realtime when updating the ListView's Collection

        private ObservableCollection<Deadlines> GetDeadlinesByDate(DateTime date)
        {

            string UrlDL = $"http://danrevi.stuhrs.dk/api/deadlines/date/{date.ToString("yyyy-MM-dd")}"; //The link for Daniels API
            string content = _client.GetStringAsync(UrlDL).Result; //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation


            if (!content.Contains("message"))
            {
                List<Deadlines> deadlines = JsonConvert.DeserializeObject<List<Deadlines>>(content); //Deserializes or converts JSON String into a collection of Deadlines
                return new ObservableCollection<Deadlines>(deadlines); //Converting the List to ObservalbleCollection of Deadlines
            }
            else
            {
                
                return new ObservableCollection<Deadlines>();
            }
        }

        private ObservableCollection<Courses> GetCoursesByDate(DateTime date)
        {

            string UrlC = $"http://danrevi.stuhrs.dk/api/courses/date/{date.ToString("yyyy-MM-dd")}"; //The link for Daniels API
            string content = _client.GetStringAsync(UrlC).Result; //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation

            if (!content.Contains("message"))
            {
                List<Courses> courses = JsonConvert.DeserializeObject<List<Courses>>(content); //Deserializes or converts JSON String into a collection of Courses
                return new ObservableCollection<Courses>(courses); //Converting the List to ObservalbleCollection of Courses
            }
            else
            {
                return new ObservableCollection<Courses>();
            }
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            Deadlines _deadlines = deadlines[0]; //Assigning the first Post object of the Post Collection to a new instance of Post
            await _client.DeleteAsync(UrlDL + "/" + _deadlines.id); //Send a DELETE request to the specified Uri as an asynchronous 
            deadlines.Remove(_deadlines); //Removes the first occurrence of a specific object from the Post collection. This will be visible on the UI instantly
        }

        BoxView outerBox;
        BoxView innerBox;
        Label labelDay;
        Label labelMonth;
        Label labelYear;
        ListView LV_Deadlines;
        ListView LV_Courses;
        Button Delete_Deadline;


        public SelectedDate (DateTimeEventArgs e)
		{
            AbsoluteLayout outerLayout = new AbsoluteLayout();
            RelativeLayout dateLayout = new RelativeLayout();
            StackLayout listLayout = new StackLayout();

            //content = new stacklayout {
            //	children = { new label {
            //                 text = "you have arrived at your selected date: " + e.datetime.tostring("dd-mm-yy") + "!" }
            //	}
            //};
            DateTime wholeDate = e.DateTime;
            outerBox = new BoxView { Color = Color.Black, HeightRequest = 7, WidthRequest = 5 };
            innerBox = new BoxView { Color = Color.LightSlateGray, HeightRequest = 5, WidthRequest = 3 };
            labelDay = new Label {Text = e.DateTime.Day + ".", FontSize = 24 };
            labelMonth = new Label { Text = wholeDate.ToString("MMMM"), FontSize = 11 };
            labelYear = new Label { Text = e.DateTime.Year.ToString(), FontSize = 6 };
            Delete_Deadline = new Button { Text = "Delete"};
            Delete_Deadline.Clicked += OnDelete;

            //------------ListView--------------------------

            LV_Deadlines = new ListView
            {
                ItemsSource = GetDeadlinesByDate(e.DateTime),

                ItemTemplate = new DataTemplate(() =>
                {
                    Label deadlineName = new Label();
                    deadlineName.SetBinding(Label.TextProperty,
                        new Binding("name"));



                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                    deadlineName
                            }
                        }
                    };
                }),
                BackgroundColor = Color.LightSkyBlue,
                HeightRequest = 75,
                WidthRequest = 400
            };

            LV_Courses = new ListView
            {
                ItemsSource = GetCoursesByDate(e.DateTime),
                ItemTemplate = new DataTemplate(() =>
                {
                    Label courseName = new Label();
                    courseName.SetBinding(Label.TextProperty,
                        new Binding("name"));



                    // Return an assembled ViewCell.
                    return new ViewCell
                    {

                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                   courseName
                            }
                        }
                    };
                }),
                BackgroundColor = Color.LightGreen,
                HeightRequest = 75,
                WidthRequest = 400
            };
            //------------------ListView-------------------

            dateLayout.Children.Add(outerBox, Constraint.RelativeToParent((parent) => {
                return parent.X + 90;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Y + 20;
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Width * .2);
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Height * .2);
            }));
            dateLayout.Children.Add(innerBox, Constraint.RelativeToView(outerBox, (parent, sibling) => {
                return sibling.X + 5;
            }), Constraint.RelativeToView(outerBox, (parent, sibling) => {
                return sibling.Y + 5;
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Width * .2);
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Height * .2);
            }));
            dateLayout.Children.Add(labelDay, Constraint.RelativeToView(innerBox, (parent, sibling) => {
                return sibling.X + 15;
            }), Constraint.RelativeToView(innerBox, (parent, sibling) => {
                return sibling.Y + 5;
            }) 
            //Constraint.RelativeToParent((parent) => {
            //    return (parent.Width * .5);
            //}), 
            //Constraint.RelativeToParent((parent) => {
            //    return (parent.Height * .5);
            //})
            );
            dateLayout.Children.Add(labelMonth, Constraint.RelativeToView(innerBox, (parent, sibling) => {
                return sibling.X + 17;
            }), Constraint.RelativeToView(innerBox, (parent, sibling) => {
                return sibling.Y + 35;
            }) 
            //Constraint.RelativeToParent((parent) => {
            //    return (parent.Width * .5);
            //}), 
            //Constraint.RelativeToParent((parent) => {
            //    return (parent.Height * .5);
            //})
            );
            dateLayout.Children.Add(labelYear, Constraint.RelativeToView( innerBox, (parent, sibling) => {
                return sibling.X + 17;
            }), 
            Constraint.RelativeToView(innerBox, (parent, sibling) => {
                return sibling.Y + 55;
            }) 
            //Constraint.RelativeToParent((parent) => {
            //    return (parent.Width * .5);
            //}), 
            //Constraint.RelativeToParent((parent) => {
            //    return (parent.Height * .5);
            //})
            );

            //dateLayout.Children.Add(LV_Deadlines, Constraint.RelativeToParent((parent) =>
            //{
            //    return parent.X + 10;
            //}), Constraint.RelativeToParent((parent) =>
            //{
            //    return parent.Y + 100;
            //}), Constraint.RelativeToParent((parent) =>
            //{
            //    return (parent.Width * .9);
            //}), Constraint.RelativeToParent((parent) =>
            //{
            //    return (parent.Height * .5);
            //}));

            //dateLayout.Children.Add(LV_Courses, Constraint.RelativeToView(LV_Deadlines, (parent, sibling) => {
            //    return sibling.X + 5;
            //}), Constraint.RelativeToView(LV_Deadlines, (parent, sibling) => {
            //    return sibling.Y + 50;
            //}), Constraint.RelativeToParent((parent) => {
            //    return (parent.Width * .9);
            //}), Constraint.RelativeToParent((parent) => {
            //    return (parent.Height * .5);
            //}));


            //flyt begge list views ned hvor jeg tilføjer dem til stacklayoutet. Defter skal jeg placere dem i disse if statements.
            //Altså så den kun køre de 2 listviews hvis der retuneres noget/ der er mere end 1 ting i collection.
            //if (GetDeadlinesByDate(e.DateTime) == null)
            //{
            //    return;
            //}
            //if (GetDeadlinesByDate(e.DateTime).Count != 0)
            //{
                
                
            //}
            //else
            //{
            //    listLayout.Children.Add(new Label { Text = "No deadlines" });
            //        }

            listLayout.Children.Add(LV_Deadlines);
            listLayout.Children.Add(LV_Courses);
            listLayout.Children.Add(Delete_Deadline);

            outerLayout.Children.Add(dateLayout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            outerLayout.Children.Add(listLayout);

            this.Content = outerLayout;
        }
	}
}