using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto2Recu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var localizacion = CrossGeolocator.Current;
            if (localizacion != null)
            {
                localizacion.PositionChanged += Localizacion_PositionChanged;
                await localizacion.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                var posicion = await localizacion.GetPositionAsync();
                var centromapa = new Position(posicion.Latitude, posicion.Longitude);
                mapas.MoveToRegion(new MapSpan(centromapa, 1, 1));
            }
        }

        private void Localizacion_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var centromapa = new Position(e.Position.Latitude, e.Position.Longitude);
            //mapas.MoveToRegion(MapSpan.FromCenterAndRadius(centromapa, Distance.FromKilometers(1)));
            mapas.MoveToRegion(new MapSpan(centromapa, 1, 1));
        }
    }
}