using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace MultiViewApp.View
{
    using ViewModel;
    /// <summary>
    /// Logika interakcji dla klasy View3.xaml
    /// </summary>
    public partial class View3 : UserControl
    {
        private readonly View3_ViewModel viewmodel;

        public View3()
        {
            InitializeComponent();

            viewmodel = new View3_ViewModel(SetButtonColor);
            DataContext = viewmodel;

            /* Button matrix grid definition */
            for (int i = 0; i < viewmodel.DisplaySizeX; i++)
            {
                ButtonMatrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ButtonMatrixGrid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
            }

            for (int i = 0; i < viewmodel.DisplaySizeY; i++)
            {
                ButtonMatrixGrid.RowDefinitions.Add(new RowDefinition());
                ButtonMatrixGrid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
            }

            for (int i = 0; i < viewmodel.DisplaySizeX; i++)
            {
                for (int j = 0; j < viewmodel.DisplaySizeY; j++)
                {
                    // <Button
                    Button led = new Button()
                    {
                        // Name = "LEDij"
                        Name = viewmodel.LedIndexToTag(i, j),
                        // CommandParameter = "LEDij"
                        CommandParameter = viewmodel.LedIndexToTag(i, j),
                        // Style="{StaticResource LedButtonStyle}"

                        // Bacground="{StaticResource ... }"
                        Background = new SolidColorBrush(viewmodel.DisplayOffColor),
                        // BorderThicness="2"
                        BorderThickness = new Thickness(2),
                    };
                    // Command="{Binding CommonButtonCommand}" 
                    led.SetBinding(Button.CommandProperty, new Binding("CommonButtonCommand"));
                    // Grid.Column="i" 
                    Grid.SetColumn(led, i);
                    // Grid.Row="j"
                    Grid.SetRow(led, j);
                    // />

                    ButtonMatrixGrid.Children.Add(led);
                    ButtonMatrixGrid.RegisterName(led.Name, led);
                }
            }
        }
        private void SetButtonColor(string name, Color color)
        {
            (ButtonMatrixGrid.FindName(name) as Button).Background = new SolidColorBrush(color);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
