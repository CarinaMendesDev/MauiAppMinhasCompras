using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

// 📘 Views (Agenda 05 – Edição de Produtos)
// Este arquivo contém a lógica da tela EditarProduto.xaml
// Aqui é tratado o evento de salvar, que atualiza os dados no banco.
public partial class EditarProduto : ContentPage
{
    // Construtor da página, inicializa os componentes visuais definidos no XAML
    public EditarProduto()
    {
        InitializeComponent();
    }

    // Evento disparado quando o usuário clica no botão "Salvar" da Toolbar
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Recupera o produto que foi passado como BindingContext
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os valores editados na tela
            Produto p = new Produto
            {
                Id = produto_anexado.Id, // mantém o mesmo Id para atualizar
                Descricao = txt_descricao.Text, // pega o texto digitado na descrição
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // converte quantidade
                Preco = Convert.ToDouble(txt_preco.Text) // converte preço
            };

            // Chama o método Update da classe de banco de dados (SQLiteDatabaseHelper)
            await App.Db.Update(p);

            // Exibe mensagem de sucesso ao usuário
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            // Volta para a tela anterior (ListaProduto)
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Captura e exibe erros de forma amigável
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
