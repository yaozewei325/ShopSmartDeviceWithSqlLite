using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ShopSmartDevice.Models
{
    public class SmartDevice
    {
        //definir les enums pour faciliter les contraintes

        public enum e_type { Telephone_Intelligent, Tablette, Montre_intelligente }

        public enum e_platform { Android, iOS, Autre }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Modele { get; set; }
        public string Fabriquant { get; set; }

        //utiliser un enum pour faciliter les contraintes
        public e_type Type { get; set; }
        public e_platform Plateforme { get; set; }
        public double Prix { get; set; }
        public string ImageUrl { get; set; }
       



    }
}
