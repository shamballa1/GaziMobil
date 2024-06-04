using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace gazimobil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }   
    }
}
