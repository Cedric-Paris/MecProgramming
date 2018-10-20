using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MecProgramming.View
{
    /// <summary>
    /// Logique d'interaction pour PromptControl.xaml
    /// </summary>
    public partial class PromptControl : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(PromptControl), new UIPropertyMetadata(""));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public PromptControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Text += ((sender as Button).Content as string);
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if(Text.Length > 0)
                Text = Text.Remove(Text.Length - 1);
        }

        private void SpaceButton_Click(object sender, RoutedEventArgs e)
        {
            Text += " ";
        }
    }
}
