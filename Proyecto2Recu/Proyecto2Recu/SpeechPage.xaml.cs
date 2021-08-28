using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto2Recu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeechPage : ContentPage
    {
        public SpeechPage()
        {
            InitializeComponent();
        }

        private void BtnSpeed_Clicked(object sender, EventArgs e)
        {
            TextToSpeech.SpeakAsync(txtNota.Text);
        }

        private async void BtnSpeech1_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(txtNota.Text, null, (float)sldTono.Value, (float)sldSpeed.Value, (float)sldVolumen.Value);
        }
    }
}