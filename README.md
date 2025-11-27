# 🛒 App de Compras - .NET MAUI + SQLite

Este projeto está sendo desenvolvido nas aulas de **Desenvolvimento de Sistemas (DS) - Módulo III**.  
Trata-se de um **aplicativo de compras** desenvolvido em **.NET MAUI**, com persistência de dados utilizando **SQLite**.  
📌 **Objetivo:** Criar uma solução prática e interativa para gerenciar produtos, permitindo **inserir, visualizar, atualizar e excluir** informações de forma simples e eficiente.  

---

## 📌 Funcionalidades
- **Persistência de dados com SQLite** garantindo que as informações sejam salvas localmente.  
- **Listagem de produtos dinâmica** vinculada ao banco de dados.  
- **CRUD completo (Create, Read, Update, Delete):**
  - **Create** → Inserir novos produtos através de formulário.  
  - **Read** → Visualizar a lista de produtos cadastrados.  
  - **Update** → Editar informações de produtos existentes.  
  - **Delete** → Remover produtos com confirmação.  
- **Busca instantânea com SearchBar** para localizar produtos em tempo real.  
- **Navegação entre telas com BindingContext**, permitindo visualizar e editar itens selecionados.  
- **Menus de Contexto (ContextActions)** na listagem, possibilitando editar ou excluir produtos rapidamente.  
- **Pull to Refresh** com `RefreshView` para atualizar a lista de produtos puxando para baixo.  
- **Regionalização** com `CurrentCulture` e `CurrentUICulture`, exibindo moeda no padrão **R$** e datas no formato **dd/MM/yyyy**.  
- **Splash Screen e Ícone personalizados** para melhor identidade visual do app.  
- Interface **simples, responsiva e intuitiva**, pensada para o usuário final.  

---

## 📅 Andamento do Desenvolvimento

### ✅ Agenda 1 — Fundamentos, SQLite e Estrutura do Projeto
- Estudo sobre **persistência de dados em aplicativos móveis**.  
- Introdução ao conceito de **CRUD (Create, Read, Update, Delete)**.  
- Configuração do **SQLite no .NET MAUI** com o pacote `sqlite-net-pcl`.
- Criação da base do aplicativo em **.NET MAUI**: Organização inicial do projeto em **Models, Views e Helpers**.  
- Criação do modelo `Produto` com os campos principais (descrição, quantidade e preço).

📌 **Resumo:**  
- Introdução ao CRUD e persistência de dados em aplicativos móveis.  
- Criação do modelo Produto e organização inicial do projeto.
  
---

### ✅ Agenda 2 — Classe de Acesso ao Banco de Dados (Helpers/SQLiteDatabaseHelper)
- Estruturação da pasta **Helpers** e criação da classe `SQLiteDatabaseHelper`.
- Implementação da **conexão assíncrona** com SQLite (`SQLiteAsyncConnection`), garantindo a criação da tabela `Produto`.  
- Desenvolvimento dos métodos principais do **CRUD**:
  - `Insert` → insere novos produtos.  
  - `GetAll` → retorna todos os registros da tabela.  
  - `Update` → atualiza os dados de um produto existente.  
  - `Delete` → exclui um produto pelo Id.  
  - `Search` → busca produtos pela descrição utilizando **SQL LIKE**.  
- Organização do código para garantir **reaproveitamento, centralização e segurança** na manipulação do banco.

📌 **Resumo:**  
- Conexão assíncrona com SQLite.  
- Métodos principais do CRUD implementados.  
- Código organizado para reaproveitamento e segurança.
  
---

### ✅ Agenda 3 — Estrutura Principal do App, Navegação e Cadastro de Produtos

- **App.xaml.cs**  
  - Implementação do **padrão Singleton** para o `SQLiteDatabaseHelper`, garantindo apenas uma instância do banco durante toda a execução do app.  
  - Uso de `Path.Combine` + `Environment.GetFolderPath` para definir corretamente o caminho do banco no dispositivo.  
  - Alteração da tela inicial: agora o app abre em **ListaProduto**, encapsulada em um `NavigationPage` (necessário para permitir navegação entre telas).  

- **ListaProduto.xaml / ListaProduto.cs**  
  - Página configurada com **ToolbarItems**:  
    - **Somar** (ainda sem lógica).  
    - **Adicionar** → abre a página de cadastro (`NovoProduto`) via `Navigation.PushAsync`.  
  - Estrutura inicial da tela de listagem de produtos.  

- **NovoProduto.xaml / NovoProduto.cs**  
  - Criação de formulário simples para cadastro:  
    - Campos de **Descrição**, **Quantidade** e **Preço Unitário**.  
    - Teclado numérico configurado para entrada de valores.  
  - Toolbar com botão **Salvar**, que:  
    - Cria um objeto `Produto` com os valores digitados.  
    - Insere no banco de dados via `App.Db.Insert(p)`.  
    - Exibe mensagens de sucesso ou erro com `DisplayAlert`.  

📌 **Resumo:**  
Na Agenda 3 consolidamos o núcleo do app:  
- Implementação do **padrão Singleton** para garantir uma única instância do banco de dados em toda a aplicação.  
- Definição da **página inicial com navegação** entre telas. 
- Criação da tela de **lista de produtos** e da tela de **cadastro de novo produto**. 
- Implementação do **formulário de inserção** e da lógica para salvar produtos no banco.  

---

### ✅ Agenda 4 — Recuperação de Dados, Busca Instantânea e Listagem Dinâmica

- **ListaProduto.xaml**
- **SearchBar para busca em tempo real**
  - Evento `TextChanged` filtra produtos enquanto o usuário digita.  
  - `Placeholder` orienta o usuário sobre o que buscar.  
- **ListView para exibir produtos do banco**
  - `ItemsSource` vinculado ao resultado da busca ou à lista completa.  
  - Suporte a **ContextActions** (ex.: remover item) para interação direta.  
  - Cabeçalho configurado com `Grid` para organizar ID, Descrição, Preço, Quantidade e Total.

- **ListaProduto.cs**
- **Uso de `ObservableCollection<Produto>`**  
  - Atualiza a interface automaticamente sem precisar recarregar o `ItemsSource`.  
  - Reage às alterações (inserção, exclusão, busca) em tempo real.  
- **Método `OnAppearing()` sobrescrito**  
  - Recarrega os produtos sempre que a tela volta a ser exibida.  
  - Garante consistência após adição ou edição de itens.  
- **Implementação da busca**  
  - `txt_search_TextChanged` limpa e repopula a lista conforme o texto digitado.  
- **Função de somatório**  
  - Botão na Toolbar que calcula e exibe o valor total de todos os produtos.

📌 **Resumo:**
- 🔎 **Busca instantânea** com `SearchBar`.  
- 📋 **Listagem dinâmica e reativa** com `ObservableCollection`.  
- 🔄 **Ciclo de vida integrado** usando `OnAppearing()` para manter os dados sempre atualizados.  
- 💰 **Cálculo automático de totais**, facilitando o controle de compras.

---

### ✅ Agenda 5 — Navegação, ContextActions e Edição de Produtos  

**Navegação a partir de itens da ListView**  
- Uso do evento `ItemSelected` para detectar seleção de produtos.  
- Passagem de dados entre telas via **BindingContext**.  
- Exibição e edição de detalhes do produto selecionado.  

**Menu de Contexto (ContextActions)**  
- Inclusão de **menus de contexto** na ListView (`MenuItem`).  
- Ações como **Editar** e **Excluir** diretamente na lista.  
- Uso de `IsDestructive="True"` para destacar ações críticas (ex: excluir).  

**Confirmações com DisplayAlert**  
- Mensagens de confirmação antes de exclusões.  
- Caixas de diálogo assíncronas com `await`.  

**Tratamento de Exceções (try-catch)**  
- Evita falhas em operações com o banco.  
- Exibe alertas amigáveis ao usuário em caso de erro.  

**Método EditarProduto**  
- Acessado via menu de contexto ou botão.  
- Atualiza os dados do produto no SQLite com `Update(p)`.  
- Exibe mensagem de sucesso após a atualização.  
- Retorna à listagem com `Navigation.PopAsync()`.  
- Encapsulado em `try-catch` para maior robustez.  

📌 **Resumo:**  
- Navegação entre telas com passagem de dados.  
- Menus de contexto adicionados à ListView.  
- Edição e exclusão de produtos implementadas.  
- Uso de tratamento de erros e confirmações.  

---

### ✅ Agenda 6 — Regionalização, Pull to Refresh e Ajustes Visuais

**Regionalização do App**
- Configuração de `CultureInfo.DefaultThreadCurrentCulture` e `CultureInfo.DefaultThreadCurrentUICulture` para "pt-BR".  
- Exibição de valores monetários no padrão brasileiro (ex.: **R$ 1.234,56**).  
- Datas formatadas no padrão `dd/MM/yyyy`.  

**Pull to Refresh**
- Substituição da `ListView` para estar dentro de um `RefreshView`.  
- Implementação do evento `Refreshing` no code-behind para recarregar os produtos do SQLite.  
- Uso de `IsRefreshing = false` ao final do processo para parar a animação de atualização.  

**Ajustes Visuais**
- Configuração do **ícone do aplicativo** via `<MauiIcon Include="Resources/appicon.svg" />`.  
- Configuração da **Splash Screen** no `.csproj` ou via `MauiProgram.cs`.  
- Melhorias no layout e na apresentação dos valores  

📌 **Resumo:**  
- Regionalização implementada para exibição correta de moeda e datas.  
- Funcionalidade de atualização manual da lista adicionada com `RefreshView`.  
- Ícone e splash screen configurados, deixando o app com aparência mais profissional.
    
---

- **Item 1:** Implementado campo categoria e botão relatório.
<div align="center">
  <h1><img width="385" height="290" alt="image" src="https://github.com/user-attachments/assets/69282deb-5f98-476d-a70e-e144cd4572dc" /></h1>
</div>

- **Item 2:** Implementado filtro de pesquisa onde o usuário pode visualizar somente os produtos de uma categoria específica
<div align="center">
  <h1><img width="383" height="287" alt="image" src="https://github.com/user-attachments/assets/31c0cf99-2c25-4470-899b-cee57e051031" /></h1>
</div>

- **Item 3:** Implementado relatório gastos por categoria ao clicar no botão relatório.
<div align="center">
  <h1><img width="385" height="290" alt="image" src="https://github.com/user-attachments/assets/2d706639-a5e4-4847-a556-9cb4424220c2" /></h1>
</div>

## 🛠 Tecnologias Utilizadas
- **.NET MAUI** — Framework multiplataforma para criação de aplicativos nativos.  
- **C#** — Linguagem principal para desenvolvimento.  
- **SQLite** — Banco de dados local para persistência de informações.  
- **MVVM** — Padrão de arquitetura para separação de responsabilidades.  
