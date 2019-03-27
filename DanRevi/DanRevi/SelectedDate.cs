using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamForms.Controls;
using Xamarin.Forms;

namespace DanRevi
{
	public class SelectedDate : ContentPage
	{
        Label centerLabel;
        BoxView outerBox;

		public SelectedDate (DateTimeEventArgs e)
		{
            AbsoluteLayout outerLayout = new AbsoluteLayout();
            RelativeLayout layout = new RelativeLayout();

            //content = new stacklayout {
            //	children = { new label {
            //                 text = "you have arrived at your selected date: " + e.datetime.tostring("dd-mm-yy") + "!" }
            //	}
            //};

            outerBox = new BoxView { Color = Color.LightGray, HeightRequest = 8, WidthRequest = 5 };
            centerLabel = new Label { Text = "Hi" };

            layout.Children.Add(outerBox, Constraint.RelativeToParent((parent) => {
                return parent.X + 85;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Y + 20;
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Width * .3);
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Height * .3);
            }));
            layout.Children.Add(centerLabel, Constraint.RelativeToParent((parent) => {
                return parent.X + 85;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Y + 20;
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Width * .5) - 50;
            }), Constraint.RelativeToParent((parent) => {
                return (parent.Height * .5) - 50;
            }));

            outerLayout.Children.Add(layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            this.Content = outerLayout;
        }
	}
}