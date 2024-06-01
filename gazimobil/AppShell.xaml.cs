using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace gazimobil
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public ICommand OpenWebCommand => new Command<string>(async (url) => await Launcher.OpenAsync(new Uri(url)));
    }
}
