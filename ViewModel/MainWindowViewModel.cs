using System.Windows;
using System.Windows.Input;
using MecProgramming.Tools;
using System.Threading.Tasks;

namespace MecProgramming.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private static string OhYeahString = "Oh Yeah!";

        private SpeechSynthesizerManager synthesizerManager;
        private MailSender mailSender;
        private Autocomplete autocomplete;

        private string displayedText = "";
        private bool isKeyBoardFeatureEnabled = true;
        private bool isCommonFeatureEnabled = false;
        private string predictedWord1 = "";
        private string predictedWord2 = "";
        private string predictedWord3 = "";

        public string DisplayedText
        {
            get { return displayedText; }
            set
            {
                displayedText = value;
                OnPropertyChanged("DisplayedText");
                UpdateAutoComplete();
            }
        }

        public CommonPhrasesViewModel CommonPhrasesViewModel { get; set;  }

        private bool IsKeyBoardFeatureEnabled
        {
            get { return isKeyBoardFeatureEnabled; }
            set
            {
                isKeyBoardFeatureEnabled = value;
                OnPropertyChanged("KeyBoardVisibility");
            }
        }

        public bool IsCommonFeatureEnabled
        {
            get { return isCommonFeatureEnabled; }
            private set
            {
                isCommonFeatureEnabled = value;
                OnPropertyChanged("IsCommonFeatureEnabled");
                OnPropertyChanged("CommonVisibility");
            }
        }

        public string PredictedWord1
        {
            get { return predictedWord1; }
            set
            {
                predictedWord1 = value;
                OnPropertyChanged("PredictedWord1");
            }
        }

        public string PredictedWord2
        {
            get { return predictedWord2; }
            set
            {
                predictedWord2 = value;
                OnPropertyChanged("PredictedWord2");
            }
        }

        public string PredictedWord3
        {
            get { return predictedWord3; }
            set
            {
                predictedWord3 = value;
                OnPropertyChanged("PredictedWord3");
            }
        }

        public Visibility KeyBoardVisibility
        {
            get { return IsKeyBoardFeatureEnabled ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility CommonVisibility
        {
            get { return IsCommonFeatureEnabled ? Visibility.Visible : Visibility.Collapsed; }
        }

        public ICommand ClearCommand { get { return new ButtonCommand(()=> { DisplayedText = string.Empty; }); } }

        public ICommand SpeakCommand { get { return new ButtonCommand(SpeakDisplayedText); } }

        public ICommand SendEmailCommand { get { return new ButtonCommand(SendDisplayedTextAsEmail); } }

        public ICommand CommonCommand { get { return new ButtonCommand(ToggleCommonFeature); } }

        public ICommand OhYeahCommand { get { return new ButtonCommand(SpeakOhYeah); } }

        public ICommand Pred1Command { get { return new ButtonCommand(() => AutocompliteDisplayedText(predictedWord1)); } }

        public ICommand Pred2Command { get { return new ButtonCommand(() => AutocompliteDisplayedText(predictedWord2)); } }

        public ICommand Pred3Command { get { return new ButtonCommand(() => AutocompliteDisplayedText(predictedWord3)); } }

        public MainWindowViewModel(SpeechSynthesizerManager synthesizerManager, MailSender mailSender)
        {
            this.synthesizerManager = synthesizerManager;
            this.mailSender = mailSender;
            CommonPhrasesViewModel = new CommonPhrasesViewModel(this.synthesizerManager);
            autocomplete = new Autocomplete("proverbs.txt");
        }

        public void SpeakDisplayedText()
        {
            Task.Run(() => synthesizerManager.Speak(DisplayedText));
        }

        public void SendDisplayedTextAsEmail()
        {
            mailSender.SendMail(DisplayedText);
        }

        public void UpdateAutoComplete()
        {
            var predicted = autocomplete.predict(DisplayedText);
            if(predicted.Count > 0)
                PredictedWord1 = predicted[0];
            if (predicted.Count > 1)
                PredictedWord2 = predicted[1];
            if (predicted.Count > 2)
                PredictedWord3 = predicted[2];
        }

        public void AutocompliteDisplayedText(string text)
        {
            if (DisplayedText.Length <= 0)
            {
                DisplayedText = text + " ";
                return;
            }
            else
            {
                if (DisplayedText[DisplayedText.Length - 1] == ' ')
                {
                    DisplayedText += text + " ";
                }
                else
                {
                    bool flag = false;
                    for(int i = DisplayedText.Length - 1; i >= 0; i--)
                    {
                        if(DisplayedText[i] == ' ')
                        {
                            DisplayedText = string.Format("{0} {1} ", DisplayedText.Remove(i), text);
                            flag = true;
                            break;
                        }
                    }
                    if(!flag)
                    {
                        DisplayedText = string.Format("{0} ", text);
                    }
                }
            }
            DisplayedText = DisplayedText.ToUpper();
        }

        public void ToggleCommonFeature()
        {
            if(IsCommonFeatureEnabled)
            {
                IsCommonFeatureEnabled = false;
                IsKeyBoardFeatureEnabled = true;
            }
            else
            {
                IsCommonFeatureEnabled = true;
                IsKeyBoardFeatureEnabled = false;
            }
        }

        public void SpeakOhYeah()
        {
            Task.Run(() => synthesizerManager.Speak(OhYeahString));
        }
    }
}
