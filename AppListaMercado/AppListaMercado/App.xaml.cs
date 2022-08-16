using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppListaMercado.View;



namespace AppListaMercado
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.ListaProdutos());
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
