using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // ObservableCollection mantém a lista atualizada automaticamente
    private readonly ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        // Ligando ListView ao ObservableCollection
        lst_produtos.ItemsSource = lista;
    }

    // Ao reabrir a tela, carrega os produtos do banco
    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Abre a tela de cadastro de novos produtos
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Busca instantânea (Descrição OU Categoria)
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = (e.NewTextValue ?? string.Empty).ToLower();

            lst_produtos.IsRefreshing = true;
            lista.Clear();

            // Busca todos os produtos
            var todos = await App.Db.GetAll();

            // Filtra por descrição OU categoria
            var filtrados = todos.Where(p =>
                (!string.IsNullOrEmpty(p.Descricao) && p.Descricao.ToLower().Contains(q)) ||
                (!string.IsNullOrEmpty(p.Categoria) && p.Categoria.ToLower().Contains(q)));

            foreach (var item in filtrados)
                lista.Add(item);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    // Função de somatório: Calcula o total dos produtos cadastrados
    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            double soma = lista.Sum(i => i.Total);
            string msg = $"O total é {soma:C}";
            await DisplayAlert("Total dos Produtos", msg, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Total dos Produtos", ex.Message, "OK");
        }
    }

    // Remoção com ContextActions: Exibe confirmação e remove o produto selecionado
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is not MenuItem selecionado)
                return;

            if (selecionado.BindingContext is not Produto p)
                return;

            bool confirm = await DisplayAlert(
                "Tem certeza?", $"Remover {p.Categoria}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Edição de produtos: Ao selecionar um item da lista, abre a tela de edição
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem is not Produto p)
                return;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Refresh manual: sempre lista todos os produtos
    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    // Relatório por Categoria
    private async void ToolbarItem_Clicked_Relatorio(object sender, EventArgs e)
    {
        try
        {
            if (!lista.Any())
            {
                await DisplayAlert("Relatório", "Nenhum produto cadastrado.", "OK");
                return;
            }

            // Agrupar produtos pela categoria e somar os totais
            var agrupado = lista
                .GroupBy(p => p.Categoria)
                .Select(g => new
                {
                    Categoria = g.Key,
                    Total = g.Sum(p => p.Total)
                })
                .ToList();

            // Montar mensagem
            StringBuilder sb = new StringBuilder();
            foreach (var item in agrupado)
            {
                sb.AppendLine($"{item.Categoria}: {item.Total:C}");
            }

            // Exibir relatório
            await DisplayAlert("Relatório por Categoria", sb.ToString(), "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
