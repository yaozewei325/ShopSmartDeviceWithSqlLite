using ShopSmartDevice.Models;
using ShopSmartDevice.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace ShopSmartDevice.ViewModels
{
    public class PaiementPageVM : BaseViewModel
    {

        //C'est le nom et le prénom du client qui est obligatoires et seront affichés sur la facture, mais
        //ceci est arbitraire, on peut le faire pour tous les champs si on le souhaite

        private string _ajouterNom;

        //parce que le bouton de paiement est conditionnel, chaque fois que le changement de
        //valeur dans la zone de texte du nom et du prénom doit être notifié dans les deux sens.
        public string AjouterNom
        {
            get { return _ajouterNom; }
            set
            {
                SetValue(ref _ajouterNom, value);
            }
        }

        private string _ajouterPrenom;
        public string AjouterPrenom
        {
            get { return _ajouterPrenom; }
            set { SetValue(ref _ajouterPrenom, value); }
        }
        public string AjouterAdresse { get; set; }
        public string AjouterTele { get; set; }
        public string AjouterCourriel { get; set; }
        public string AjouterNumBancaire { get; set; }
        public double Montant { get; set; }
 



        //Declaration du delegate de command
        public Command CmdAjouter { get; private set; }

        public Command CmdCancel { get; private set; }


        public PaiementPageVM()
        {
            CmdAjouter = new Command(async () => await AjouterFactureAsync(), ValidateSave);

            CmdCancel = new Command(OnCancel);

            //  a chaque fois que l'un des texbox change de valeur (execute la methode validateSave)
            this.PropertyChanged += (_, __) => CmdAjouter.ChangeCanExecute();

            //le montant est transféré du panier
            this.Montant = App.Panier.GetTotal();

        }

        private bool ValidateSave()
        {
            //le bouton de paiement ne sera activé que si le nom et le prénom sont saisis

            return !string.IsNullOrWhiteSpace(AjouterNom) && !string.IsNullOrWhiteSpace(AjouterPrenom);
        }



        //definition d'une action ajouter
        public async Task AjouterFactureAsync()
        {
            //Créer un objet client avec les valeurs de la zone de texte

            Client client = new Client()
            {
                Nom = AjouterNom,
                Prenom = AjouterPrenom,
                Adresse = AjouterAdresse,
                Telephone = AjouterTele,
                Courriel = AjouterCourriel,
                NumBancaire = AjouterNumBancaire
            };

            //Créer un objet facture avec le montant et le nom du client.
            Facture newFacture = new Facture()
            {

                Montant = this.Montant,
                NomClient = $"{client.Nom} {client.Prenom}",

            };

            //message pour que l'utilisateur confirme le paiement
            bool answer = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alerte", "Confirmez le paiement?", "Oui", "Non");

            if (answer)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Confirmation", "Paiement traité, directez-vous sur la page des factures", "OK");

                //lors de la confirmation du paiement, Ajouter la nouvelle facture au base de donée.

                await App.FactureDatabase.InsertAsync(newFacture);

                //rediriger vers la page des factures
                await Shell.Current.GoToAsync(nameof(FacturesPage));
            }
        }

        //En cliquant sur le bouton d'annulation, rediriger vers la page d'accueil.
        private async void OnCancel(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AccueilPage));

        }

    }
}
