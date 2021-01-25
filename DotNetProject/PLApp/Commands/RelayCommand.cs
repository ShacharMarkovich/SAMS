using System;
using System.Windows.Input;

class RelayCommand : ICommand
{
    private Action<object> _action;

    public RelayCommand(Action<object> action)
    {
        _action = action;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (parameter != null)
        {
            _action(parameter);
        }
        else
        {
            _action("Hello world");
        }
    }

    public event EventHandler CanExecuteChanged;
}