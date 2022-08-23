using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace AppListaMercado.Model
{
    internal class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        
    }
}
