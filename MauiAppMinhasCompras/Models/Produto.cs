using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppMinhasCompras.Models
{
    //  AGENDA 1 — Criação do modelo Produto
    // Nesta etapa, foi definida a estrutura básica da entidade que representa
    // um produto dentro do aplicativo. Ela é mapeada diretamente no banco SQLite.
    public class Produto
    {
        //  AGENDA 1 — Campo usado para armazenar a descrição do produto
        string _descricao;

        //  AGENDA 1 — Campo de identificação do produto (chave primária com autoincremento)
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //  AGENDA 3 — Implementação de propriedades com validação (getter/setter)
        // Aqui foi incluída a validação para impedir cadastro de produtos sem descrição.
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                    throw new Exception("Por favor, preencher a descrição");

                _descricao = value;
            }
        }

        //  AGENDA 6 — Campo Categoria (Nova funcionalidade do projeto)
        // Adicionado na finalização do projeto para atender ao desafio "Relatório por Categoria".
        // Este campo classifica o produto por tipo (Ex: Alimentos, Higiene, Limpeza).
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

        //  AGENDA 1 — Campos principais do modelo Produto
        // Representam a quantidade e o preço unitário do item.
        public double Quantidade { get; set; }
        public double Preco { get; set; }

        //  AGENDA 4 — Propriedade calculada (Total = Quantidade x Preço)
        // Usada para exibir o total de cada item e somatórios na listagem.
        public double Total { get => Quantidade * Preco; }
    }
}
