using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ELib.Navigation
{
    public class NavigationService: INavigationService
    {
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        public Frame NavFrame {  get; set; }

        public NavigationService(Frame frame)
        {
            NavFrame = frame;
            RegisterPages();
        }

        private void RegisterPages()
        {
        }

        public void NavigateTo(string pageKey)
        {
            if (_pages.ContainsKey(pageKey))
            {
                NavFrame.Navigate(Activator.CreateInstance(_pages[pageKey]));
            }
        }

        public void GoBack()
        {
            if (NavFrame.CanGoBack)
            {
                NavFrame.GoBack();
            }
        }
    }
}
