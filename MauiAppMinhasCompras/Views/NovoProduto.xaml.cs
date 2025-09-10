using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
        // Agenda 03 – Tela de Cadastro        
    }

    // Evento do Botão Salvar: Método assíncrono chamado quando o usuário clica no botão "Salvar" da Toolbar
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Criação do Objeto Produto: Instancia a model Produto e preenche com os valores digitados pelo usuário
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere o novo registro na tabela Produto utilizando o método Insert()
            await App.Db.Insert(p);

            // Exibe alerta confirmando que o registro foi salvo com sucesso
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
                       
        }
        catch (Exception ex)
        {
            // Exibe mensagem de erro amigável caso algo dê errado
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
