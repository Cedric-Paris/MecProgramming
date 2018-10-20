using System.Speech.Synthesis;

namespace MecProgramming.Tools
{
    public class SpeechSynthesizerManager
    {
        private SpeechSynthesizer synthesizer;

        public SpeechSynthesizerManager()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                lock (synthesizer)
                {
                    Prompt speech = new Prompt(text.Trim());
                    synthesizer.Speak(speech);
                }
            }
        }
    }
}
