namespace MauiAppMinhasCompras
{
    //  AGENDA 1 — Estrutura inicial do aplicativo (.NET MAUI)
    // Esta classe representa a primeira tela do projeto (MainPage).
    // Foi utilizada apenas na fase inicial para testar eventos e a execução do app no emulador.
    // A partir da Agenda 3, a tela principal passou a ser ListaProduto.xaml.
    public partial class MainPage : ContentPage
    {
        //  AGENDA 1 — Variável usada para contar cliques no botão
        int count = 0;

        //  AGENDA 1 — Construtor da página, inicializa os componentes da interface
        public MainPage()
        {
            InitializeComponent();
        }

        //  AGENDA 1 — Evento do botão de teste "Click me"
        // Cada clique incrementa o contador e altera o texto do botão.
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            // Atualiza a leitura de acessibilidade com o novo texto
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
