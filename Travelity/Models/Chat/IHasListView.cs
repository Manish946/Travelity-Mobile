using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Travelity.Models.Chat
{
    public interface IHasListView
    {
        ListView ListView { get; }
    }
}
