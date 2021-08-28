using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto2Recu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaFotos : ContentPage
    {
        private int ItemID;
        private string ItemRoute;
        private string ItemName;
        private string ItemDesc;


        public ListaFotos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            var listafotos = await App.InstanciaBD.ObtenerSitios();
            ObservableCollection<Picture> observableCollectionFotos = new ObservableCollection<Picture>();
            ListaFotosBD.ItemsSource = observableCollectionFotos;
            foreach (Picture imagen in listafotos)
            {
                observableCollectionFotos.Add(imagen);
            }

        }

        private async void ListaFotosBD_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          


        }

        private async void MtmEliminarFoto_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var PictureSelected = menuItem.CommandParameter as Picture;
            //Se pregunta al usuario si desea elminar la ubicación
            bool confirmacion = await DisplayAlert("¿Desea borrar la ubicación?", "Los datos de borrarán de forma permanente", "Aceptar", "Cancelar ");
            if (confirmacion)
            {
                await App.InstanciaBD.EliminarFoto(PictureSelected);
            }
        }

        private void btnborrar_Clicked(object sender, EventArgs e)
        {

        }

        private async void ListaFotosBD_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var PictureSelected = e.Item as Picture;
            var verImagen = new VerImagen();
            verImagen.BindingContext = PictureSelected;
            await Navigation.PushAsync(verImagen);
        }
    }
}