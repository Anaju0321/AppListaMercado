using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppListaMercado.Model;
using SQLite;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppListaMercado.Helper
{
    

    public class SQLiteDatabaseHelper 
    {
        private readonly SQLiteAsyncConnection _conn;


        public SQLiteDatabaseHelper(string path)
        {
            /**
             * Abrindo uma nova "conexão" com o arquivo db3 através do caminho recebido.
             * note a utilização da biblioteca SQLite "instalada" no projeto via pacote Nuget
             */
            _conn = new SQLiteAsyncConnection(path);

            /**
             * Criação da tabela com base no Model Produto (mais detalhes no arquivo Produto.cs na pasta Model)
             * Note que apesar do Async na criação da tabela é chamado o método Wait() que define a espera
             * da criação da tabela (se ela ainda não existir) antes de efetuar as outras operações, por exemplo,
             * insert.
             */
            _conn.CreateTableAsync<Produto>().Wait();
        }


        /**
         * Método que faz a inseração de um novo registro na tabela. Veja que o método recebe uma Model
         * preenchida com os dados a serem inseridos. Observe que o método tem um retorno do tipo int 
         * (número de linhas inseridas) sendo executado via Task (tarefa sendo executada de forma assíncrona).
         */
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        /**
         * Método implementado com uso da estratégia de escrever o código SQL. Neste método podemos
         * ver a abstração que o SQLite faz, onde podemos digitar código SQL para manipulação do 
         * arquivo db3. O método também recebe uma model preenchida para atualizar no db3 e o retorno
         * em forma de Task é uma lista de todos os registros atualizados.
         */
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE id= ? ";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }


        /**
         * Método que faz o retorno de todas as linhas contidas no arquivo db3 referentes 
         * a tabela Produto. Veja que o método executa a listagem de forma assíncrona.
         */
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }


        /**
         * Método que remove um registro do arquivo db3 de forma assíncrona. Este método recebe
         * como parâmetro a Id do registro a ser removido. Observe o uso da LINQ no processo de
         * remoção. Para entender mais sobre LINQ veja essa aula: https://www.youtube.com/watch?v=m8rR1aLjrg0
         */
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }


    }
}