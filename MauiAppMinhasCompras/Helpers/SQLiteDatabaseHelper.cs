using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    // Classe responsável por centralizar o acesso ao banco SQLite: Agenda 2
    public class SQLiteDatabaseHelper
    {
        // Conexão assíncrona com o banco SQLite: Agenda 2
        readonly SQLiteAsyncConnection _conn;

        // Agenda 2: Construtor da classe, recebe o caminho do banco de dados
        // Inicializa a conexão e garante a criação da tabela Produto
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait(); // Cria a tabela caso ainda não exista
        }

        // Agenda 2 — CRUD (Create): inserir um novo produto no banco
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        // Agenda 2 — CRUD (Update): atualizar os dados de um produto
        // Modificado/Utilizado Agenda 5 — Edição de Produtos
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        // Agenda 2 — CRUD (Delete): excluir um produto pelo Id
        // Utilizado Agenda 5 — Menu de Contexto (Excluir)
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // Agenda 2 — CRUD (Read): retorna todos os produtos cadastrados
        // Utilizado em várias agendas para popular a lista de produtos (Agendas 3, 4 e 5)
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        // Agenda 2 — método (Search): busca produtos pelo texto digitado
        // Expandido na Agenda 4 — Busca Instantânea com SearchBar
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
