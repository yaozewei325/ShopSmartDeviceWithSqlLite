using ShopSmartDevice.Data;
using ShopSmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShopSmartDevice.ViewModels
{
    public class ItemsPageVM : BaseViewModel
    {
        public ObservableCollection<SmartDevice> SmartDevices
        {
            get;
            set;
        }

        //quantité totale dans le panier
        private int _count;

        //le changement de total doit être notifié à la page
        public int Count
        {
            get { return _count; }
            set { SetValue(ref _count, value); }
        }

        //Commande pour l'action de double-clic
        public Command<SmartDevice> ItemTapped { get; }

        public Command ViderPanier { get; }

        public ItemsPageVM()
        {
            //En double-cliquant Enregistrer dans le panier.
            this.ItemTapped = new Command<SmartDevice>(OnAddSmartDevice);
            this.ViderPanier = new Command(ClearList);
            this.SmartDevices = new ObservableCollection<SmartDevice>();
        }

        public void RefreshList()
        {
            LoadSmartDevices();
        }
        private async void LoadSmartDevices()
        {
            try
            {
                this.SmartDevices.Clear();
                var items = await App.DeviceDatabase.GetAllAsync();
                foreach (var item in items)
                {
                    this.SmartDevices.Add(item);
                }

                //Définir le Count comme la quantité totale dans le panier.

                this.Count = App.Panier.CountPanier();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void OnAddSmartDevice(SmartDevice item)
        {

            App.Panier.AddProduct(item);
            this.Count = App.Panier.CountPanier();

        }
        private void ClearList()
        {
            App.Panier.ClearPanier();
            this.Count = App.Panier.CountPanier();
        }


    }
}
