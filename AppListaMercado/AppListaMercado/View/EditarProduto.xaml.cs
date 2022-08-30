using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppListaMercado.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaMercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                /**
                 * Obtém qual foi o Produto anexado no BindingContext da página no momento que
                 * ela foi criada e enviada para navegação.
                 */
                Produto produto_anexado = BindingContext as Produto;


                /**
                 * Preenchendo a model de acordo com os valores dos Entry. Note que recuperamos a Id
                 * do BindingContext, como feito acima.
                 */
                Produto p = new Produto
                {
                    //Id = ((Produto) BindingContext).Id,
                    Id = produto_anexado.Id,
                    Descricao = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_qtd.Text),
                    Preco = Convert.ToDouble(txt_preco.Text),
                };

                /**
                 * Método para atualizar o registro no arquivo db3. Note que o método recebe um model
                 * preenchido e neste deve conter o Id para que seja feito o Where do Update.
                 */
                await App.Database.Update(p);

                await DisplayAlert("Sucesso!", "Produto Editado", "OK");

                await Navigation.PushAsync(new ListaProdutos());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}