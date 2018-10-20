using System.Speech.Synthesis;

namespace MecProgramming.Tools
{
    /// <summary>
    /// Wrapper class that use the Microsoft speech synthesis
    /// This class allow us to transform text into voice
    /// </summary>
    public class SpeechSynthesizerManager
    {
        /// <summary>
        /// Object of the Microsoft Speech Synthesis we want to wrap
        /// </summary>
        private SpeechSynthesizer synthesizer;

        /// <summary>
        /// Constructeur, construct a SpeechSynthesizer with default settings
        /// Settings can be change to select another voice or speed
        /// </summary>
        public SpeechSynthesizerManager()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
        }

        /// <summary>
        /// Convert a text string into voice using the Microsoft Speech Synthesis
        /// Wait for the previous voice to finish before being able to convert another text
        /// </summary>
        /// <param name="text">Text with want to get the audio from</param>
        public void Speak(string text)
        {
            // Check if the text to say is not empty
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
