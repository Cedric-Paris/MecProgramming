using MecProgramming.Tools;
using System.Windows.Input;

namespace MecProgramming.ViewModel
{
    public class CommonPhrasesViewModel : ViewModel
    {
        /// <summary>
        /// List of common words that can be used 
        /// Those words will have shortcuts to be said
        /// </summary>
        private static string YesString = "Yes";
        private static string NoString = "No";
        private static string OkayString = "Okay";
        private static string MaybeString = "Maybe";
        private static string IString = "I";
        private static string HappyString = "Happy";
        private static string SadString = "Sad";
        private static string DontString = "Don't";
        private static string CantString = "Can't";
        private static string KnowString = "Know";
        private static string WantString = "Want";
        private static string OhYeahString = "Oh Yeah";
        private static string WowString = "Wow";
        private static string WoahString = "Woah";
        private static string ThankString = "Thank you";

        /// <summary>
        /// Speech synthesizer of Microsoft that can convert text to voice
        /// </summary>
        private SpeechSynthesizerManager synthesizerManager;

        /// <summary>
        /// Map the commun words to their shorcuts buttons
        /// </summary>
        public ICommand YesCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(YesString)); } }
        public ICommand NoCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(NoString)); } }
        public ICommand OkayCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(OkayString)); } }
        public ICommand MaybeCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(MaybeString)); } }
        public ICommand ICommand { get { return new ButtonCommand(() => synthesizerManager.Speak(IString)); } }
        public ICommand HappyCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(HappyString)); } }
        public ICommand SadCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(SadString)); } }
        public ICommand DontCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(DontString)); } }
        public ICommand CantCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(CantString)); } }
        public ICommand KnowCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(KnowString)); } }
        public ICommand WantCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(WantString)); } }
        public ICommand OhYeahCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(OhYeahString)); } }
        public ICommand WowCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(WowString)); } }
        public ICommand WoahCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(WoahString)); } }
        public ICommand ThankCommand { get { return new ButtonCommand(() => synthesizerManager.Speak(ThankString)); } }

        /// <summary>
        /// Allow the voice to be used in the view
        /// </summary>
        /// <param name="synthesizerManager">Speech synthesizer of Microsoft that can convert text to voice</param>
        public CommonPhrasesViewModel(SpeechSynthesizerManager synthesizerManager)
        {
            this.synthesizerManager = synthesizerManager;
        }
    }
}
