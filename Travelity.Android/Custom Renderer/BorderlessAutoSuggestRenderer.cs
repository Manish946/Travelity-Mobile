using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using dotMorten.Xamarin.Forms;
using dotMorten.Xamarin.Forms.Platform.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travelity.Custom_Renderer;
using Travelity.Droid.Custom_Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessAutoSuggestBox), typeof(BorderlessAutoSuggestRenderer))]

namespace Travelity.Droid.Custom_Renderer
{
    public class BorderlessAutoSuggestRenderer:AutoSuggestBoxRenderer
    {
        public BorderlessAutoSuggestRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<AutoSuggestBox> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);

            }
            //if (Control != null)
            //{
            //    Control.style = (Style)App.Current.Resources["myStyle"];
            //}
        }
    }
}