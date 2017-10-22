using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomTabbar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabBar : ContentView
    {
        private static BoxView underline;

        public TabBar()
        {
            InitializeComponent();

            underline = new BoxView()
            {
                ClassId = "underlinebar",
                BackgroundColor = Color.Red,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };

            grdContainer.Children.Add(underline, SelectedIndex, 1);

            grdContainer.ChildAdded += GrdContainer_ChildAdded;
        }

        private void GrdContainer_ChildAdded(object sender, ElementEventArgs e)
        {
            if (grdContainer.ColumnDefinitions.Count < grdContainer.Children.Count && e.Element.ClassId != "underlinebar")
            {
                grdContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                var columnIndex = grdContainer.ColumnDefinitions.Count - 1;
                Grid.SetRow(e.Element, 0);
                Grid.SetColumn(e.Element, columnIndex);
                SelectedIndexChanged(this, 0, SelectedIndex);
            }
        }

        public IList<View> Items { get { return grdContainer.Children; } }


        #region SelectedIndex 
        public static BindableProperty SelectedIndexProperty =
           BindableProperty.Create(
               nameof(SelectedIndex),
               typeof(int),
               typeof(TabBar),
               defaultValue: default(int),
               defaultBindingMode: BindingMode.OneWay,
               propertyChanged: SelectedIndexChanged
           );

        private static void SelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            int newColIndex = 0;
            var ctrl = (bindable as TabBar);

            var currentRow = Grid.GetRow(underline);
            var currentCol = Grid.GetColumn(underline);

            if (newValue is string)
                int.TryParse(newValue as string, out newColIndex);
            else if (newValue is int)
                newColIndex = (int)newValue;

            if (newColIndex < ctrl.grdContainer.ColumnDefinitions.Count)
            {
                Grid.SetRow(underline, 1);
                Grid.SetColumn(underline, newColIndex);

                var elementsInFirstRow = ctrl.grdContainer.Children.Where(w => Grid.GetRow(w) == 0);
                foreach (var element in elementsInFirstRow)
                {
                    if (Grid.GetColumn(element) == newColIndex)
                        element.ClassId = "selected";
                    else
                        element.ClassId = "";
                }
            }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        #endregion
    }
}