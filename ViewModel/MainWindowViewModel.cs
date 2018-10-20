using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MecProgramming.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private static string OhYeahString = "Oh Yeah!";
        private SpeechSynthesizer synthesizer;

        private string displayedText = "";

        public string DisplayedText
        {
            get { return displayedText; }
            set
            {
                displayedText = value;
                OnPropertyChanged("DisplayedText");
            }
        }

        public ICommand ClearCommand { get { return new ButtonCommand(()=> { DisplayedText = string.Empty; }); } }

        public ICommand SpeakCommand { get { return new ButtonCommand(SpeakDisplayedText); } }

        public ICommand OhYeahCommand { get { return new ButtonCommand(SpeakOhYeah); } }

        public MainWindowViewModel()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
        }

        public void SpeakDisplayedText()
        {
            if(!string.IsNullOrWhiteSpace(DisplayedText))
            {
                lock (synthesizer)
                {
                    Prompt speech = new Prompt(DisplayedText.Trim());
                    synthesizer.Speak(speech);
                }
            }
        }

        public void SpeakOhYeah()
        {
            lock (synthesizer)
            {
                Prompt speech = new Prompt(OhYeahString);
                synthesizer.Speak(speech);
            }
        }
    }
}
