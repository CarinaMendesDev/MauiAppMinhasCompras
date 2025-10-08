using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

// AGENDA 5 — Edição de Produtos (CRUD completo)
// Este arquivo contém a lógica da tela EditarProduto.xaml
// Ele trata o evento de salvar, atualizando o produto no banco de dados SQLite.
// AGENDA 6 — Adição do campo Categoria para atualização no relatório.
public partial class EditarProduto : ContentPage
{
    //  AGENDA 5 — Construtor: inicializa os componentes definidos no XAML
    public EditarProduto()
    {
        InitializeComponent();
    }

    //  AGENDA 5 — Evento disparado quando o usuário clica no botão "Salvar" da Toolbar
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            //  AGENDA 5 — Recupera o produto atual (vindo via BindingContext)
            Produto produto_anexado = BindingContext as Produto;

            //  AGENDA 5 — Cria um novo objeto Produto com os dados atualizados
            //  AGENDA 6 — Inclui a propriedade Categoria (nova no modelo)
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text, // Agenda 5: campo original
                Categoria = txt_categoria.Text, //  Agenda 6: novo campo adicionado
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            //  AGENDA 5 — Atualiza o registro no banco de dados
            await App.Db.Update(p);

            //  AGENDA 5 — Exibe mensagem de sucesso
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            //  AGENDA 5 — Retorna para a tela anterior (ListaProduto)
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Tratamento de erros amigável
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
