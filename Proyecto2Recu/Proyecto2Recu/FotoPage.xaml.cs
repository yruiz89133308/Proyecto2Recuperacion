using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto2Recu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FotoPage : ContentPage
    {

        public string nombre, descripcion;
        private bool wasPhotoTaked;
        byte[] arrayImagen = null;
        public FotoPage()
        {
            InitializeComponent();
            wasPhotoTaked = false;
        }


        private async void toma_Clicked(object sender, EventArgs e)
        {
            var fotoTomada = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (fotoTomada != null)
            {
                arrayImagen = null;
                BtnGuardar.IsVisible = true;
                MemoryStream memoryStream = new MemoryStream();

                fotoTomada.GetStream().CopyTo(memoryStream);
                arrayImagen = memoryStream.ToArray();

                this.foto.Source = ImageSource.FromStream(() => { return fotoTomada.GetStream(); });
            }


        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            //condicional para asegurarse de que se ingreso un nombre y una descripcion
            if (!string.IsNullOrEmpty(foto_nombre.Text))
            {
                Picture photoToSave = new Picture
                {
                    Name = foto_nombre.Text,
                    Desc = foto_desc.Text,
                    Imagen = arrayImagen
                };

                try
                {
                    int res = await App.InstanciaBD.InsertPicture(photoToSave);
                    if (res > 0)
                    {
                        await DisplayAlert("Guardado Exitosamente", "Imagen guardada exitosamente", "Ok");

                        //Reseteamos a los valores iniciales 
                        BtnGuardar.IsVisible = false;
                        photoToSave = null;
                        foto.Source = ImageSource.FromFile("imagen.png");
                        foto_desc.Text = "";
                        foto_nombre.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Lo sentimos ocurrio un error" + ex.Message, "Ok");
                }


            }
            else
                await DisplayAlert("Campos vacios", "Debe asignar al menos un nombre a la foto", "Ok");
        }

        private async void Ver_Lista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaFotos());
        }
    }
}