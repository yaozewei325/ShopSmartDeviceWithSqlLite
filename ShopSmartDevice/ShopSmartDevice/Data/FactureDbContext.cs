using ShopSmartDevice.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static ShopSmartDevice.Models.SmartDevice;
namespace ShopSmartDevice.Data
{
    public class FactureDbContext
    {
        //Créer une base de données des factures pour conserver les factures générées par l'application.

        private const SQLite.SQLiteOpenFlags openFlags =
           //ouverture de la base de donnees en lecture et ecriture
           SQLite.SQLiteOpenFlags.ReadWrite |
           // creation de la base de donnees s'il n,existe pas
           SQLite.SQLiteOpenFlags.Create |
           //activer acces 
           SQLite.SQLiteOpenFlags.SharedCache;

        private readonly SQLiteAsyncConnection database;

        // le constructeur de dbcontext
        public FactureDbContext(string dbName)
        {
            // dbName représente le nom de la base de donnee
            string baseDbPath = Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            string dbPath = Path.Combine(baseDbPath, dbName);
            // creer la base de données s'il existe pas et faire une connection
            database = new SQLiteAsyncConnection(dbPath, openFlags);
            //creer la table si elle n'existe pas
            var res = database.CreateTableAsync<Facture>().Result;


        }

        
        public Task<List<Facture>> GetAllAsync()
        {
            // Lire toutes les Factures
            return database.Table<Facture>().ToListAsync();
        }
        public Task<Facture> GetByIdAsync(int id)
        {
            // Lire une Facture spécifique avec son id.
            return database.Table<Facture>().Where(i => i.Id == id).FirstOrDefaultAsync();

            //utiliser la methode GetAsync a la place de table.where
        }
        public Task<int> InsertAsync(Facture facture)
        {
            // inserer une nouvelle Facture
            return database.InsertAsync(facture);
        }
        public Task<int> UpdateAsync(Facture facture)
        {
            //mettre a jour une Facture existante
            return database.UpdateAsync(facture);
        }
        public Task<int> DeleteAsync(Facture facture)
        {
            //supprime une Facture existante
            return database.DeleteAsync(facture);
        }

        public Task<int> DeleteAsyncById(int id)
        {
            //supprime une Facture existante selon son id
            return database.DeleteAsync(id);
        }


    }
}
