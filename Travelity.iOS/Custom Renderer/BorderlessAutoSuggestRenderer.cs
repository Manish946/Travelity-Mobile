using dotMorten.Xamarin.Forms;
using dotMorten.Xamarin.Forms.Platform.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Travelity.Custom_Renderer;
using Travelity.iOS.Custom_Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessAutoSuggestBox), typeof(BorderlessAutoSuggestRenderer))]

namespace Travelity.iOS.Custom_Renderer
{
    public class BorderlessAutoSuggestRenderer: AutoSuggestBoxRenderer
    {
        //public BorderlessAutoSuggestRenderer(Context context): base(context)
        //{

        //}
        protected override void OnElementChanged(ElementChangedEventArgs<AutoSuggestBox> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.BackgroundColor = null;
                Control.Layer.BorderWidth = 0;
                Control.ShowBottomBorder = false;

            }
            //if (Control != null)
            //{
            //    Control.style = (Style)App.Current.Resources["myStyle"];
            //}
        }
    }
}