using ShopSmartDevice.Data;
using ShopSmartDevice.Models;
using ShopSmartDevice.Views;
using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopSmartDevice
{
    public partial class App : Application
    {
        //la déclaration du panier dans App pour faciliter l'appel dans les autres modèles de vue
        public static Panier _panier;

        // la declaration de dbContext comme variable statique pour faciliter l'appel dans les autres modèles de vue
        private static DeviceDbContext deviceDatabase;
        private static FactureDbContext factureDatabase;


        //Methodes d'acces
        public static Panier Panier
        {
            get { return _panier; }
        }
        public static DeviceDbContext DeviceDatabase
        {
            get { return deviceDatabase; }
        }
        public static FactureDbContext FactureDatabase
        {
            get { return factureDatabase; }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            // initialiser du SmartDevice dbcontext
            string databaseName1 = "DbSmartDevice.db3";
            deviceDatabase = new DeviceDbContext(databaseName1);

            // initialiser de la facture dbcontext
            string databaseName2 = "DbFacture.db3";
            factureDatabase = new FactureDbContext(databaseName2);

            _panier = new Panier();
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
