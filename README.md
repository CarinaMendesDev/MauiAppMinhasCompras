# 🛒 App de Compras - .NET MAUI + SQLite

Este projeto está sendo desenvolvido na aula de DS do curso Técnico em Desenvolvimento de Sistemas Modulo III.  
É um **aplicativo de compras** em **.NET MAUI**, com persistência de dados utilizando **SQLite**.  
O objetivo é criar uma solução prática e interativa para gerenciar produtos, permitindo inserir, visualizar, atualizar e excluir informações de forma simples e eficiente.

---

## 📌 Funcionalidades
- **Persistência de dados com SQLite** garantindo que as informações sejam salvas localmente.
- **Listagem de produtos** de forma dinâmica, exibindo os itens cadastrados no banco.
- **CRUD completo (Create, Read, Update, Delete)**:
  - **Create** → Inserir novos produtos.
  - **Read** → Visualizar a lista de produtos cadastrados.
  - **Update** → Editar informações de produtos existentes.
  - **Delete** → Remover produtos.
- Interface simples e responsiva, pensada para o usuário final.

---

## 📅 Andamento do Desenvolvimento

### ✅ Agenda 1 — Fundamentos, SQLite e Estrutura do Projeto
- Estudo sobre **persistência de dados em aplicativos móveis**.  
- Introdução ao conceito de **CRUD (Create, Read, Update, Delete)**.  
- Configuração do **SQLite no .NET MAUI** com o pacote `sqlite-net-pcl`.
- Criação da base do aplicativo em **.NET MAUI**: Organização inicial do projeto em **Models, Views e Helpers**.  
- Criação do modelo `Produto` com os campos principais (descrição, quantidade e preço).

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

### 📄 **ListaProduto.xaml**
- **SearchBar para busca em tempo real**  
  - Evento `TextChanged` filtra produtos enquanto o usuário digita.  
  - `Placeholder` orienta o usuário sobre o que buscar.  
- **ListView para exibir produtos do banco**  
  - `ItemsSource` vinculado ao resultado da busca ou à lista completa.  
  - Suporte a **ContextActions** (ex.: remover item) para interação direta.  
  - Cabeçalho configurado com `Grid` para organizar ID, Descrição, Preço, Quantidade e Total.

---

### 💻 **ListaProduto.cs**
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

---

### 📌 **Resumo Geral**
- 🔎 **Busca instantânea** com `SearchBar`.  
- 📋 **Listagem dinâmica e reativa** com `ObservableCollection`.  
- 🔄 **Ciclo de vida integrado** usando `OnAppearing()` para manter os dados sempre atualizados.  
- 💰 **Cálculo automático de totais**, facilitando o controle de compras.

---

## 🚀 Próximas Etapas
- Finalizar as operações de **atualização (Update)** e **exclusão (Delete)** de produtos pela interface.  
- Implementar a **listagem com binding** (exibição em `CollectionView` ou similar).  
- Criar recursos de **busca e filtros** para os produtos cadastrados.  
- Melhorar a **experiência do usuário (UX/UI)** com validações, mensagens mais intuitivas e design refinado.  
- Criar **relatórios e cálculos automáticos**, como soma de valores totais da lista.  
- Implementar **testes básicos** para validar as principais funcionalidades.  

---

## 🛠 Tecnologias Utilizadas
- **.NET MAUI** — Framework multiplataforma para criação de aplicativos nativos.  
- **C#** — Linguagem principal para desenvolvimento.  
- **SQLite** — Banco de dados local para persistência de informações.  
- **MVVM** — Padrão de arquitetura para separação de responsabilidades.  
