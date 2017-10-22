using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomTabbar
{
    public class SearchBarPageViewModel : BasePageViewModel
    {

        int _selectedTab = 0;
        public int SelectedTab
        {
            get => _selectedTab;
            set => SetPropertyValue(ref _selectedTab, value);
        }

        public ICommand TabItemTappedCommand => new Command<string>(TabItemTappedAction);
        public void TabItemTappedAction(string tabIndex)
        {
            int.TryParse(tabIndex, out var newSelection);
            SelectedTab = newSelection;
        }

        readonly INavigation Navigation;

        public SearchBarPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PageTitle = "Extended SearchBar";
        }

        #region Properties

        private string _SearchText;

        public string SearchText
        {
            get { return _SearchText; }
            set { SetPropertyValue(ref _SearchText, value); }
        }

        #endregion


        #region Commands

        public ICommand SearchCommand => new Command(SearchAction);
        public ICommand FilterTappedCommand => new Command(FilterTappedAction);

        #endregion


        #region Methods

        void SearchAction()
        {
            Debug.WriteLine("SearchAction");
        }

        void FilterTappedAction()
        {
            Debug.WriteLine("FilterTappedAction");
        }

        #endregion
    }
}
