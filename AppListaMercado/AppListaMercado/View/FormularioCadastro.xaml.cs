using AppListaMercado.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaMercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioCadastro : ContentPage
    {
        public FormularioCadastro()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                /**
                 * Preenchendo o model Produto com os dados informados na interface gráfica.
                 */
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_qtd.Text),
                    Preco = Convert.ToDouble(txt_preco.Text),
                };


                /**
                 * Chamando o método insert da SQLiteDatabaseHelper para fazer a inseração do
                 * novo registro no arquivo db3 com os dados da model preenchida acima. O await
                 * denota que o código deve esperar o insert para prosseguir.
                 */
                await App.Database.Insert(p);


                /**
                 * Avisando o usuário que deu certo.
                 */
                await DisplayAlert("Sucesso!", "Produto Cadastrado", "OK");


                /**
                 * Navegando para a tela de listagem. 
                 */
                await Navigation.PushAsync(new ListaProdutos());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }

        }
    }
}