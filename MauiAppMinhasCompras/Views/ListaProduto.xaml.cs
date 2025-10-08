using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // AGENDA 4: ObservableCollection criada para atualizar a lista automaticamente
    private readonly ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        // AGENDA 4: Ligando ListView ao ObservableCollection
        lst_produtos.ItemsSource = lista;
    }

    // AGENDA 4: Ao reabrir a tela, carrega os produtos do banco de dados SQLite
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

    // AGENDA 3: Botão Adicionar — abre a tela de cadastro de novos produtos    
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

    // AGENDA 4 + AGENDA 6: Busca instantânea (Descrição OU Categoria)    
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = (e.NewTextValue ?? string.Empty).ToLower();

            lst_produtos.IsRefreshing = true;
            lista.Clear();

            // Busca todos os produtos
            var todos = await App.Db.GetAll();


            // AGENDA 6: Agora também filtra pela Categoria            
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

    // AGENDA 4: Função de somatório — calcula o valor total de todos os produtos
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

    // AGENDA 5: Remoção com ContextActions — confirmação e exclusão
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
    // AGENDA 5: Edição de produtos — ao selecionar um item, abre tela de edição
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

    // AGENDA 4: Pull to Refresh — recarrega manualmente os produtos do banco
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

    // AGENDA 6: Relatório de Gastos por Categoria
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
