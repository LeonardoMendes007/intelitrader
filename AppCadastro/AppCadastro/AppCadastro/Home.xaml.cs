using AppCadastro.Model;
using AppCadastro.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCadastro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private IRestService _restService = new RestService();
        private List<UserRead> users = new List<UserRead>();

        public Home()
        {
            InitializeComponent();
            initAtributos();
            initEvents();
            buscarUsers();
            
            
        }

        private void buscarUsers()
        {
            try
            {
                users = _restService.getAllUsersAsync().Result.ToList();
                lvUsers.ItemsSource = users;
            }
            catch
            {

            }
            
        }

        private void initEvents()
        {
            btCadastrar.Clicked += BtCadastrar_ClickedAsync;
            lvUsers.ItemSelected += LvUsers_ItemSelected;
            etBusca.TextChanged += EtBusca_TextChanged;
        }

        private void initAtributos()
        {
            etBusca.BackgroundColor = Color.FromHex("#333333");
            etBusca.Margin = new Thickness(30, 10, 30, 0);
            gdHeader.Margin = new Thickness(30, 0, 30, 0);
            btCadastrar.Margin = new Thickness(0, 10, 0, 20);
        }

        private void EtBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<UserRead> usersTemp = users;

            lvUsers.ItemsSource = usersTemp.Where(x => x.Name.ToLower().Contains(etBusca.Text.ToLower()));

        }

        private void LvUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        private async void BtCadastrar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro());
        }

        void img_reload(object sender, EventArgs args)
        {
            imgReload.RelRotateTo(720, 2000, Easing.Linear);
            lvUsers.ItemsSource = _restService.getAllUsersAsync().Result;
        }

        private void lvUsers_Scrolled(object sender, ScrolledEventArgs e)
        {

        }
    }
}