using System.Windows;
using System.Windows.Input;
using MecProgramming.Tools;
using System.Threading.Tasks;

namespace MecProgramming.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        /// String storing "oh yeah" a really used sentence
        /// </summary>
        private static string OhYeahString = "Oh Yeah!";

        /// <summary>
        /// Speech synthesizer of Microsoft to convert text to speech 
        /// </summary>
        private SpeechSynthesizerManager synthesizerManager;

        /// <summary>
        /// Our custom class to send mails
        /// </summary>
        private MailSender mailSender;

        /// <summary>
        /// Our custom class for autocompletion of words
        /// </summary>
        private Autocomplete autocomplete;

        /// <summary>
        /// Text displayed at the beginning
        /// </summary>
        private string displayedText = "";
        private bool isKeyBoardFeatureEnabled = true;
        private bool isCommonFeatureEnabled = false;
        private string predictedWord1 = "";
        private string predictedWord2 = "";
        private string predictedWord3 = "";

        /// <summary>
        /// Display the text in the correct location
        /// </summary>
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

        /// <summary>
        /// Accessors for the common phrases
        /// </summary>
        public CommonPhrasesViewModel CommonPhrasesViewModel { get; set;  }

        /// <summary>
        /// Accessors for the keyboards features
        /// </summary>
        private bool IsKeyBoardFeatureEnabled
        {
            get { return isKeyBoardFeatureEnabled; }
            set
            {
                isKeyBoardFeatureEnabled = value;
                OnPropertyChanged("KeyBoardVisibility");
            }
        }

        /// <summary>
        /// Accessors for the commun features
        /// </summary>
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

        /// <summary>
        /// Accessors for the first predicted word
        /// </summary>
        public string PredictedWord1
        {
            get { return predictedWord1; }
            set
            {
                predictedWord1 = value;
                OnPropertyChanged("PredictedWord1");
            }
        }

        /// <summary>
        /// Accessors for the second predicted word
        /// </summary>
        public string PredictedWord2
        {
            get { return predictedWord2; }
            set
            {
                predictedWord2 = value;
                OnPropertyChanged("PredictedWord2");
            }
        }

        /// <summary>
        /// Accessors for the third predicted word
        /// </summary>
        public string PredictedWord3
        {
            get { return predictedWord3; }
            set
            {
                predictedWord3 = value;
                OnPropertyChanged("PredictedWord3");
            }
        }

        /// <summary>
        /// Accessors for the keyboard visibiily
        /// </summary>
        public Visibility KeyBoardVisibility
        {
            get { return IsKeyBoardFeatureEnabled ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Accessors for the common visibiily
        /// </summary>
        public Visibility CommonVisibility
        {
            get { return IsCommonFeatureEnabled ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Accessors for the differents command of the GUI
        /// </summary>
        public ICommand ClearCommand { get { return new ButtonCommand(()=> { DisplayedText = string.Empty; }); } }

        public ICommand SpeakCommand { get { return new ButtonCommand(SpeakDisplayedText); } }

        public ICommand SendEmailCommand { get { return new ButtonCommand(SendDisplayedTextAsEmail); } }

        public ICommand CommonCommand { get { return new ButtonCommand(ToggleCommonFeature); } }

        public ICommand OhYeahCommand { get { return new ButtonCommand(SpeakOhYeah); } }

        public ICommand Pred1Command { get { return new ButtonCommand(() => AutocompliteDisplayedText(predictedWord1)); } }

        public ICommand Pred2Command { get { return new ButtonCommand(() => AutocompliteDisplayedText(predictedWord2)); } }

        public ICommand Pred3Command { get { return new ButtonCommand(() => AutocompliteDisplayedText(predictedWord3)); } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="synthesizerManager"></param>
        /// <param name="mailSender"></param>
        public MainWindowViewModel(SpeechSynthesizerManager synthesizerManager, MailSender mailSender)
        {
            this.synthesizerManager = synthesizerManager;
            this.mailSender = mailSender;
            CommonPhrasesViewModel = new CommonPhrasesViewModel(this.synthesizerManager);
            autocomplete = new Autocomplete("proverbs.txt");
        }

        /// <summary>
        /// Convert the text into voice when we press the button speak
        /// </summary>
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
