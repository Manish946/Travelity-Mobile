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
[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessRenderer))]
namespace Travelity.iOS.Custom_Renderer
{
    public class BorderlessRenderer:EntryRenderer
    {
        //public BorderlessRenderer(Context context) : base(context)
        //{

        //}
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;

            }
        }
    }
}