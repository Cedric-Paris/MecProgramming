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

        private string displayedText = "";
        private bool isKeyBoardFeatureEnabled = true;
        private bool isCommonFeatureEnabled = false;

        public string DisplayedText
        {
            get { return displayedText; }
            set
            {
                displayedText = value;
                OnPropertyChanged("DisplayedText");
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

        public MainWindowViewModel(SpeechSynthesizerManager synthesizerManager, MailSender mailSender)
        {
            this.synthesizerManager = synthesizerManager;
            this.mailSender = mailSender;
            CommonPhrasesViewModel = new CommonPhrasesViewModel(this.synthesizerManager);
        }

        public void SpeakDisplayedText()
        {
            Task.Run(() => synthesizerManager.Speak(DisplayedText));
        }

        public void SendDisplayedTextAsEmail()
        {
            mailSender.SendMail(DisplayedText);
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
