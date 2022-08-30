using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppListaMercado.View;
using AppListaMercado.Helper;
using System.IO;

namespace AppListaMercado
{
    public partial class App : Application
    {

        static SQLiteDatabaseHelper database;


        /**
         * Propriedade que define a forma de acesso a instância de SQLiteDatabaseHelper. A propriedade
         * é somente leitura, isto é, não é possível atribuir um valor a este campo. No comento que
         * o campo é chamado uma instância da classe SQLiteDatabaseHelper é criada (implementação get).
         */
        public static SQLiteDatabaseHelper Database
        {
            get
            {
                /**
                 * Se o campo database for nulo, significa que ainda não foi atribuída uma instância de
                 * SQLiteDatabaseHelper a ele, então uma nova instância será criada e esta mesma será usada
                 * em todo tempo de execução do arquivo.
                 */
                if (database == null)
                {
                    /**
                     * Para criar uma instância de SQLiteDatabaseHelper devemos o caminho do arquivo db3
                     * (arquivo que contém as definições "DDL" e os dados propriamente ditos) no SQLite).
                     * Devemos notar que essa abstração é necessária pois estamos em uma ferramenta multiplataforma
                     * e isso significa que há um caminho diferente no Windows, Android e iOS e com o uso das
                     * classes do System.IO podemos abstrair esse caminho.
                     */
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "arquivo.db3"
                    );


                    /**
                     * Criando uma instância de SQLiteDatabaBaseHelper como caminho até o arquivo db3 mencionado acima.
                     */
                    database = new SQLiteDatabaseHelper(path);
                }

                return database;
            }
        }




        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.ListaProdutos());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
