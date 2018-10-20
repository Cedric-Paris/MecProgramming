using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MecProgramming.ViewModel
{
    /// <summary>
    /// Abstract class for the view model we are using in the GUI
    /// We are using the Viewmodel design pattern
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event when a property change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Do an action when a property is changed
        /// </summary>
        /// <param name="propertyName">Name of the property changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
