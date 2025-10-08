using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();

        //  AGENDA 3 — Criação da Tela de Cadastro
        // Nesta etapa, foi criada a página NovoProduto.xaml e a navegação a partir da ListaProduto.
        // O objetivo era permitir a inserção de novos registros no banco SQLite.
    }

    //  AGENDA 3 — Evento do Botão "Salvar"
    // Método assíncrono chamado quando o usuário clica no botão "Salvar" da Toolbar.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // AGENDA 3 — Criação do objeto Produto com base nos campos do formulário
            // AGENDA 6 — Inclusão do campo Categoria (novo requisito do desafio)
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,   // Agenda 3: campo original
                Categoria = txt_categoria.Text,   //  Agenda 6: campo novo adicionado
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Agenda 3
                Preco = Convert.ToDouble(txt_preco.Text)            // Agenda 3
            };

            //  AGENDA 3 — Inserção no banco de dados (SQLite)
            await App.Db.Insert(p);

            // Exibe alerta confirmando que o registro foi salvo com sucesso
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

            // Retorna para a tela de listagem após o cadastro
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Exibe mensagem de erro amigável caso algo dê errado
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
