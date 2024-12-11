using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Interfaces.Services
{
    public interface INavigationService
    {
        Frame NavFrame { get; set; }
        void NavigateTo(string pageKey);
        void GoBack();
    }
}
