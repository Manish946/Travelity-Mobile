using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Templates.TemplateViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmptyTemplate : ViewCell
    {
        public EmptyTemplate()
        {
            InitializeComponent();
        }
    }
}