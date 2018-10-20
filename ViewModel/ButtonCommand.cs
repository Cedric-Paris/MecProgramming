using System;
using System.Windows.Input;

namespace MecProgramming.ViewModel
{
    /// <summary>
    /// Implementation of ICommand interface to make a button on the GUI
    /// The ICommand interface is to define a command that can be executed
    /// </summary>
    public class ButtonCommand : ICommand
    {
        /// <summary>
        /// This is the function called when we clic on the button 
        /// </summary>
        private Action actionOnExecute;

        /// <summary>
        /// Event when the state change
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Constructor, map the fonction to be executed when the button is clicked
        /// </summary>
        /// <param name="actionOnExecute"></param>
        public ButtonCommand(Action actionOnExecute)
        {
            this.actionOnExecute = actionOnExecute;
        }

        /// <summary>
        /// Indicate if the command can be executed
        /// Always returns true as we can always executed the action when the button is click
        /// </summary>
        /// <param name="parameter">Not used</param>
        /// <returns>Always returns true</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the action of the button 
        /// </summary>
        /// <param name="parameter">Not used</param>
        public void Execute(object parameter)
        {
            actionOnExecute();
        }
    }
}
