using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShopSmartDevice.Models
{
    public class Facture
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Montant { get; set; }
        public string NomClient { get; set; }
        public string Photo { get; set; }

        //les noms des produits qui sont transférés du panier
        public string ProduitsStr { get { return "Produits: " + String.Join(", ", App.Panier.GetProductNames()); } }


        public Facture()
        {
            //icône par défaut pour les factures

            this.Photo = "https://previews.123rf.com/images/arcady31/arcady311510/arcady31151000003/46532249-invoice-icon.jpg";

        }



    }
}
