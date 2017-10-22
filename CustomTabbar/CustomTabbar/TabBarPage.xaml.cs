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
            BindingContext = new SearchBarPageViewModel(this.Navigation);

        }

       
    }
}