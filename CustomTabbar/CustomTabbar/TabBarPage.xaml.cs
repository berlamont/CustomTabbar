using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomTabbar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabBarPage : ContentPage
    {
        public TabBarPage()
        {
            InitializeComponent();
            BindingContext = this;

            SelectedTab = 0;
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand TabItemTappedCommand => new Command<string>(TabItemTappedAction);
        void TabItemTappedAction(string tabIndex)
        {
            int newSelection = 0;
            int.TryParse(tabIndex, out newSelection);
            SelectedTab = newSelection;
        }
    }
}