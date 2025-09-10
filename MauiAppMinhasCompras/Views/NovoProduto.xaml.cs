using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
        // Agenda 03 � Tela de Cadastro        
    }

    // Evento do Bot�o Salvar: M�todo ass�ncrono chamado quando o usu�rio clica no bot�o "Salvar" da Toolbar
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria��o do Objeto Produto: Instancia a model Produto e preenche com os valores digitados pelo usu�rio
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere o novo registro na tabela Produto utilizando o m�todo Insert()
            await App.Db.Insert(p);

            // Exibe alerta confirmando que o registro foi salvo com sucesso
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
                       
        }
        catch (Exception ex)
        {
            // Exibe mensagem de erro amig�vel caso algo d� errado
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
