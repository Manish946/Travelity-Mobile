using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentBackground : Frame
    {
        public ContentBackground()
        {
            InitializeComponent();
        }
    }
}