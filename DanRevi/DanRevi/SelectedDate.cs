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

        private ObservableCollection<Deadlines> GetDeadlinesByDate(DateTime date)
        {

            string UrlDL = $"http://danrevi.stuhrs.dk/api/deadlines/date/{date.ToString("yyyy-MM-dd")}"; //The link for Daniels API
            string content = _client.GetStringAsync(UrlDL).Result; //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation
            List<Deadlines> deadlines = JsonConvert.DeserializeObject<List<Deadlines>>(content); //Deserializes or converts JSON String into a collection of Deadlines
            return new ObservableCollection<Deadlines>(deadlines); //Converting the List to ObservalbleCollection of Deadlines

        }

        private ObservableCollection<Courses> GetCoursesByDate(DateTime date)
        {

            string UrlC = $"http://danrevi.stuhrs.dk/api/courses/date/{date.ToString("yyyy-MM-dd")}"; //The link for Daniels API
            string content = _client.GetStringAsync(UrlC).Result; //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation
            List<Courses> courses = JsonConvert.DeserializeObject<List<Courses>>(content); //Deserializes or converts JSON String into a collection of Courses
            return new ObservableCollection<Courses>(courses); //Converting the List to ObservalbleCollection of Courses

        }

        BoxView outerBox;
        BoxView innerBox;
        Label labelDay;
        Label labelMonth;
        Label labelYear;
        ListView LV_Deadlines;
        ListView LV_Courses;


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
            //------------ListView--------------------------

            


            

            //LV_Courses = new ListView
            //{
            //    ItemsSource = GetCoursesByDate(e.DateTime),
            //    ItemTemplate = new DataTemplate(() =>
            //    {
            //        Label courseName = new Label();
            //        courseName.SetBinding(Label.TextProperty,
            //            new Binding("name"));

                    

            //        // Return an assembled ViewCell.
            //        return new ViewCell
            //        {
                        
            //            View = new StackLayout
            //            {
            //                Padding = new Thickness(0, 5),
            //                Orientation = StackOrientation.Horizontal,
            //                Children =
            //                {
            //                       courseName
            //                }
            //            }
            //        };
            //    }),
            //    BackgroundColor = Color.LightGreen,
            //    HeightRequest = 75,
            //    WidthRequest = 400
            //};
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
            if (GetDeadlinesByDate(e.DateTime).Count != 0)
            {
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
                listLayout.Children.Add(LV_Deadlines);
            }
            else
            {
                listLayout.Children.Add(new Label { Text = "No deadlines" });
                    }

            
            //listLayout.Children.Add(LV_Courses);

            outerLayout.Children.Add(dateLayout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            outerLayout.Children.Add(listLayout);

            this.Content = outerLayout;
        }
	}
}