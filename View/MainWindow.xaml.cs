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
using System.Speech.Synthesis;

namespace MecProgramming
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                // Configure the audio output.   
                synth.SetOutputToDefaultAudioDevice();

                // Create a prompt from a string.  
                Prompt color = new Prompt("What is your favorite color?");

                // Speak the contents of the prompt synchronously.  
                synth.Speak(color);
            }
        }
    }
}
