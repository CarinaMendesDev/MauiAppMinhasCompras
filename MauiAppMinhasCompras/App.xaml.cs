using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Views;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // Agenda 02 – Padrão Singleton para o Banco: Campo estático que armazenará a instância única do helper do SQLite
        static SQLiteDatabaseHelper _db;

        // Agenda 02 – Propriedade Db: Garante que exista apenas UMA instância de SQLiteDatabaseHelper em toda a aplicação
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // Se ainda não foi criada, instancia o helper passando o caminho do banco
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), // Pasta de dados local
                        "banco_sqlite_compras.db3"); // Nome do arquivo do banco

                    _db = new SQLiteDatabaseHelper(path); // Cria a conexão
                }

                return _db; // Retorna a mesma instância em toda a aplicação
            }
        }

        public App()
        {
            InitializeComponent();

            // Agenda 03 – Definição da Tela Inicial: inicializa com a página de listagem de produtos
            // encapsulada em NavigationPage para permitir navegação entre telas.
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
