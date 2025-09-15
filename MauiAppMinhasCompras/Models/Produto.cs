using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppMinhasCompras.Models
{
    // Representa um Produto no aplicativo: Visto na Agenda 1
    public class Produto
    {
        // armazenar a descrição do produto: Visto na Agenda 1
        string _descricao;

        // Representa chave primária do banco de dados: Visto Agenda 1
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Propriedade Descricao do produto com "getter" e "setter": Visto Agenda 3 — Cadastro de Produto
        public string Descricao
        {
            get => _descricao;
            set
            {
                // Validação: se a descrição for nula, lança uma exceção
                if (value == null)
                {
                    throw new Exception("Por favor, preencher a descrição");
                }

                 _descricao = value;
            }
        }

        private string _categoria;
        public string Categoria
        {
            get => _categoria;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Por favor, preencher a categoria");
                _categoria = value;
            }
        }

        // Quantidade do produto: Agenda 1 (campos principais do modelo Produto)
        public double Quantidade { get; set; }

        // Preço unitário do produto: Agenda 1 (campos principais do modelo Produto)
        public double Preco { get; set; }

        // Propriedade calculada: retorna o valor total do produto (Quantidade x Preço)
        // Essa lógica foi aplicada na Agenda 4 — somatório e manipulação de interface
        public double Total { get => Quantidade * Preco; }
    }
}
