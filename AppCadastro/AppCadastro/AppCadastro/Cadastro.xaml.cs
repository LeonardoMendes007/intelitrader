using AppCadastro.Model;
using AppCadastro.Service;
using System;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: ExportFont("AbrilFatface-Regular.ttf", Alias = "MyAwesomeCustomFont")]

namespace AppCadastro
{
    public partial class Cadastro : ContentPage
    {
        private IRestService _service = new RestService();
        private Color errorColor = Color.OrangeRed;
        private Color sucessColor = Color.LightGreen;

        public Cadastro()
        {
            InitializeComponent();
            btCadastrar.Clicked += BtCadastrar_ClickedAsync;
            etName.TextChanged += EtName_TextChanged;
            etAge.TextChanged += EtAge_TextChanged;

            etName.BackgroundColor = Color.FromHex("#333333");
            etAge.BackgroundColor = Color.FromHex("#333333");
            etSurname.BackgroundColor = Color.FromHex("#333333");
            gdPrincipal.Margin = new Thickness(20, 5, 20, 0);

        }

        private void EtAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (etAge.BackgroundColor == Color.FromHex("#EB5757"))
            {
                etAge.BackgroundColor = Color.White;
             
            }
        }

        private void EtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (etName.BackgroundColor == Color.FromHex("#EB5757"))
            {
                etName.BackgroundColor = Color.White;
            }
        }

        private void BtCadastrar_ClickedAsync(object sender, EventArgs e)
        {
            try
            {


                var verifica = validar();

                if (verifica)
                {
                    salvarNoBanco();
                }
            }
            catch
            {
                lbMessage.TextColor = errorColor;
                if (etAge.Text != null)
                {
                    if (etAge.Text.Equals(""))
                    {
                        lbMessage.Text = "O campo Age é requerido";
                        Vibration.Vibrate(1000);
                    }
                    else
                    {

                        lbMessage.Text = "O campo Age só aceita numeros";
                        Vibration.Vibrate(1000);
           

                    }
                }
                else
                {
                    lbMessage.Text = "O campo Age é requerido";
                    Vibration.Vibrate(1000);
                }


            }

        }

        private void salvarNoBanco()
            {
                UserCreate user = new UserCreate(
                  etName.Text,
                  etSurname.Text,
                  Int32.Parse(etAge.Text)
              );

            try
            {
                HttpStatusCode statusCode = _service.postUser(user);

                switch (statusCode)
                {
                    case HttpStatusCode.Created:


                        lbMessage.TextColor = sucessColor;
                        lbMessage.Text = "Sucesso";

                        break;
                    case HttpStatusCode.BadRequest:

                        lbMessage.TextColor = errorColor;
                        lbMessage.Text = "Verifique os dados e tente novamente";
                        break;

                }
            }
            catch
            {
                lbMessage.TextColor = errorColor;
                lbMessage.Text = "Erro ao conectar com o servidor verifique sua conexão";
            }



         }

            private bool validar()
            {
                var valor = Int32.Parse(etAge.Text);

            if (valor == 0 || etName.Text == null)
            {
                if (valor == 0)
                {
                    lbMessage.TextColor = errorColor;
                    lbMessage.Text = "O campo Age não pode ser 0";
                    Vibration.Vibrate(1000);

                    return false;
                }
                else
                {
                    lbMessage.TextColor = errorColor;
                    lbMessage.Text = "O campo Name é requerido";
                    Vibration.Vibrate(1000);

                    return false;
                }
            }
            else if(etName.Text.Equals(""))
            {
                lbMessage.TextColor = errorColor;
                lbMessage.Text = "O campo Name é requerido";
                Vibration.Vibrate(1000);
                return false;
            }
            else
            {
                return true;
            }

            

        }
        
    }
}
