using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCadastro
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home())
            {
                BarBackgroundColor = Color.FromHex("#333333"),
                BarTextColor = Color.White
            }; //new MainPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
