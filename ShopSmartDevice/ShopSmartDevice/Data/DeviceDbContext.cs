using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ShopSmartDevice.Models;
using static ShopSmartDevice.Models.SmartDevice;

namespace ShopSmartDevice.Data
{
    public class DeviceDbContext
    {
        //Lien de l'image par défaut
        private static string url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDNHZNiga4vZ97fE7yHMCNPoKSF06OH-XvnQ&usqp=CAU";

        private const SQLite.SQLiteOpenFlags openFlags =
           //ouverture de la base de donnees en lecture et ecriture
           SQLite.SQLiteOpenFlags.ReadWrite |
           // creation de la base de donnees s'il n,existe pas
           SQLite.SQLiteOpenFlags.Create |
           //activer acces 
           SQLite.SQLiteOpenFlags.SharedCache;

        private readonly SQLiteAsyncConnection database;

        // le constructeur de dbcontext
        public DeviceDbContext(string dbName)
        {
            // dbName représente le nom de la base de donnee
            string baseDbPath = Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            string dbPath = Path.Combine(baseDbPath, dbName);
            // creer la base de données s'il existe pas et faire une connection
            database = new SQLiteAsyncConnection(dbPath, openFlags);
            //creer la table si elle n'existe pas
            var res = database.CreateTableAsync<SmartDevice>().Result;
            if (res == CreateTableResult.Created)
                PeuplerdataBase();

        }

        private async void PeuplerdataBase()
        {
            try
            {
                SmartDevice device;
                device = new SmartDevice()
                {
                    Modele = "PadFone Infinity2",
                    Fabriquant = "Asus",
                    Type = e_type.Telephone_Intelligent,
                    Plateforme = e_platform.Android,
                    Prix = 129.00,
                    ImageUrl = url
                };
                // inserer un objet a la base de données
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Modele = "Flare S8 Deluxe",
                    Fabriquant = "Cherry Mobile",
                    Type = e_type.Telephone_Intelligent,
                    Plateforme = e_platform.Android,
                    Prix = 160.00,
                    ImageUrl = url

                };
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Modele = "Wildfire E3",
                    Fabriquant = "HTC",
                    Type = e_type.Telephone_Intelligent,
                    Plateforme = e_platform.Android,
                    Prix = 229.00,
                    ImageUrl = url


                };
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Modele = "Yoga Tab 13",
                    Fabriquant = "Lenovo",
                    Type = e_type.Tablette,
                    Plateforme = e_platform.Android,
                    Prix = 250.00,
                    ImageUrl = url

                };
                await database.InsertAsync(device);


                device = new SmartDevice()
                {
                    Modele = "iPhone 13 Pro",
                    Fabriquant = "Apple",
                    Type = e_type.Telephone_Intelligent,
                    Plateforme = e_platform.iOS,
                    Prix = 729.00,
                    ImageUrl = url


                };
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Modele = "Galaxy Watch 4",
                    Fabriquant = "Samsung",
                    Type = e_type.Montre_intelligente,
                    Plateforme = e_platform.Android,
                    Prix = 112.00,
                    ImageUrl = url

                };
                await database.InsertAsync(device);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }



        public  Task<List<SmartDevice>> GetAllAsync()
        {
            // Lire toutes les SmartDevices
            return database.Table<SmartDevice>().ToListAsync();
        }
        public Task<SmartDevice> GetByIdAsync(int id)
        {
            // Lire une SmartDevice spécifique avec son id.
            return database.Table<SmartDevice>().Where(i => i.Id == id).FirstOrDefaultAsync();

            //utiliser la methode GetAsync a la place de table.where
        }
        public Task<int> InsertAsync(SmartDevice SmartDevice)
        {
            // inserer une nouvelle SmartDevice
            return database.InsertAsync(SmartDevice);
        }
        public Task<int> UpdateAsync(SmartDevice SmartDevice)
        {
            //mettre a jour une SmartDevice existante
            return database.UpdateAsync(SmartDevice);
        }
        public Task<int> DeleteAsync(SmartDevice SmartDevice)
        {
            //supprime une SmartDevice existante
            return database.DeleteAsync(SmartDevice);
        }

        public Task<int> DeleteAsyncById(int id)
        {
            //supprime une SmartDevice existante selon son id
            return database.DeleteAsync(id);
        }
    }
}
