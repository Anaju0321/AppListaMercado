using AppListaMercado.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AppListaMercado.View

{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaProdutos : ContentPage
    {
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();


        public ListaProdutos()
        {
            InitializeComponent();

            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try { 
            Navigation.PushAsync(new FormularioCadastro());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            /**
             * Uso da LINQ do C# para fazer a soma de cada um dos itens do array de objetos.
             */
            double soma = lista_produtos.Sum(i => i.Preco * i.Quantidade);

            string msg = "O total da compra é: " + soma;

            DisplayAlert("Ops", msg, "OK");
        }

        /**
        * Método executado quando a página é exibida ao usuário.
        */
        protected override void OnAppearing()
        {
            /**
             * Se a ObservableCollection estiver vazia é executado para obter todas as linhas do db3
             */
            if (lista_produtos.Count == 0)
            {
                /**
                 * Inicializando a Thread que irá buscar o array de objetos no arquivo db3
                 * via classe SQLiteDatabaseHelper encapsulada na propriedade Database da
                 * classe App.
                 */
                System.Threading.Tasks.Task.Run(async () =>
                {
                    /**
                     * Retornando o array de objetos vindos do db3, foi usada uma variável tem do tipo
                     * List para que abaixo no foreach possamos percorrer a lista temporária e add
                     * os itens à ObservableCollection
                     */
                    List<Produto> temp = await App.Database.GetAll();

                    foreach (Produto item in temp)
                    {
                        lista_produtos.Add(item);
                    }

                    /**
                     * Após carregar os registros para a ObservableCollection removemos o loading da tela.
                     */
                    ref_carregando.IsRefreshing = false;
                });
            }
        }



        private async void MenuItem_Clicked(object sender, EventArgs e)
        {

            /**
             * Reconhecendo qual foi a linha do ListView que disparou o evento de exclusão.
             */
            MenuItem disparador = (MenuItem)sender;


            /**
             * Obtendo qual foi o produto que estava anexado no BindingContext
             */
            Produto produto_selecionado = (Produto)disparador.BindingContext;

            /**
             * Perguntando ao usuário se ele realmente deseja remover. Note o await para aguardar
             * a resposta do usuário antes de prosseguir com o código.
             */
            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");

            if (confirmacao)
            {
                /**
                 * Removendo o registro do db3 via método Delete da SQLiteDatabaseHelper
                 */
                await App.Database.Delete(produto_selecionado.Id);

                /**
                 * Removendo o item da ObservableCollection também, que é automaticamente
                 * removida da visão do usuário na ListView também.
                 */
                lista_produtos.Remove(produto_selecionado);
            }
        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new EditarProduto());
            {
                BindingContext = (Produto)e.SelectedItem;
            }
        }
    }
}