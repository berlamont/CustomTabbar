using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomTabbar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtSearchBar : ContentView
    {
        CancellationTokenSource cts = null;

        public ExtSearchBar()
        {
            InitializeComponent();
        }

        #region Properties

        private int _textChangedDelay;
        public int TextChangedDelay
        {
            get { return _textChangedDelay; }
            set { _textChangedDelay = value; }
        }

        #endregion

        #region Bindable Properties

        #region HasFilterIcon 
        public static BindableProperty HasFilterIconProperty =
           BindableProperty.Create(
               nameof(HasFilterIcon),
               typeof(bool),
               typeof(ExtSearchBar),
               defaultValue: default(bool),
               defaultBindingMode: BindingMode.OneWay
           );

        public bool HasFilterIcon
        {
            get { return (bool)GetValue(HasFilterIconProperty); }
            set { SetValue(HasFilterIconProperty, value); }
        }
        #endregion

        #region Placeholder 
        public static BindableProperty PlaceholderProperty =
           BindableProperty.Create(
               nameof(Placeholder),
               typeof(string),
               typeof(ExtSearchBar),
               defaultValue: default(string),
               defaultBindingMode: BindingMode.OneWay
           );

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        #endregion

        #region Text 
        public static BindableProperty TextProperty =
           BindableProperty.Create(
               nameof(Text),
               typeof(string),
               typeof(SearchBar),
               defaultValue: default(string),
               defaultBindingMode: BindingMode.TwoWay
           );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region TextChangedCommand 
        public static BindableProperty TextChangedCommandProperty =
           BindableProperty.Create(
               nameof(TextChangedCommand),
               typeof(Command),
               typeof(SearchBar),
               defaultValue: default(Command),
               defaultBindingMode: BindingMode.OneWay
           );

        public Command TextChangedCommand
        {
            get { return (Command)GetValue(TextChangedCommandProperty); }
            set { SetValue(TextChangedCommandProperty, value); }
        }
        #endregion

        #region FilterCommand
        public static BindableProperty FilterCommandProperty =
            BindableProperty.Create(
                nameof(FilterCommand),
                typeof(ICommand),
                typeof(SearchBar),
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay
            );

        public ICommand FilterCommand
        {
            get { return (ICommand)GetValue(FilterCommandProperty); }
            set { SetValue(FilterCommandProperty, value); }
        }
        #endregion

        #region FilterCommandParameter
        public static BindableProperty FilterCommandParameterProperty =
            BindableProperty.Create(
                nameof(FilterCommandParameter),
                typeof(object),
                typeof(SearchBar),
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay
            );

        public object FilterCommandParameter
        {
            get { return (object)GetValue(FilterCommandParameterProperty); }
            set { SetValue(FilterCommandParameterProperty, value); }
        }
        #endregion

        #region SearchCommand 
        public static BindableProperty SearchCommandProperty =
           BindableProperty.Create(
               nameof(SearchCommand),
               typeof(Command),
               typeof(SearchBar),
               defaultValue: default(Command),
               defaultBindingMode: BindingMode.OneWay
           );

        public Command SearchCommand
        {
            get { return (Command)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }
        #endregion

        #endregion


        #region Methods

        public async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HasFilterIcon)
            {
                var sbar = sender as Xamarin.Forms.SearchBar;
                if (string.IsNullOrWhiteSpace(sbar?.Text))
                {
                    iconFilter.TranslateTo(0, 0);
                }
                else
                {
                    int transX = -25; //Good for Android
                    if (Device.RuntimePlatform == Device.iOS)
                        transX = -80; //Good for iOS

                    if (iconFilter.TranslationX != transX)
                        iconFilter.TranslateTo(transX, 0, 100);
                }
            }

            if (TextChangedCommand != null)
            {
                if (cts != null) cts.Cancel();
                cts = new CancellationTokenSource();
                var ctoken = cts.Token;

                try
                {
                    var millisDelay = TextChangedDelay > 0 ? TextChangedDelay : 650;
                    await Task.Delay(millisDelay, ctoken);

                    if (ctoken.IsCancellationRequested)
                        return;

                    if (TextChangedCommand.CanExecute(null))
                        TextChangedCommand?.Execute(null);
                }
                catch (OperationCanceledException)
                {
                    // Expected
                }
            }
        }

        private void IconFilter_Tapped(object sender, EventArgs e)
        {
            if (FilterCommand?.CanExecute(FilterCommandParameter) ?? false)
                FilterCommand?.Execute(FilterCommandParameter);
        }

        #endregion
    }
}