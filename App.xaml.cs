using System.Windows;
using MecProgramming.Tools;
using MecProgramming.ViewModel;

namespace MecProgramming
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Start(object sender, StartupEventArgs e)
        {
            var mailSender = new MailSender();

            var wnd = new MainWindow();
            var dataContext = new MainWindowViewModel(new SpeechSynthesizerManager(), mailSender);
            wnd.DataContext = dataContext;
            wnd.Show();
        }
    }
}
