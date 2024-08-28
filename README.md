## EstoqueWEB🌐

EstoqueWEB é um sistema de gerenciamento de inventário desenvolvido para ajudar pequenas e médias empresas a gerenciar seus estoques de forma eficiente. Com funcionalidades como registro de entrada e saída de itens, controle de patrimônio, e uma interface amigável, este projeto é ideal para empresas que desejam otimizar seus processos de inventário.

## Índice📃

- Sobre o Projeto
- Funcionalidades
- Screenshots
- Tecnologias Utilizadas
- Instalação
- Como Usar
- Roadmap
- Contato


## Sobre o Projeto📋

EstoqueWEB é um sistema de gerenciamento de inventário desenvolvido em ASP.NET Core com Razor Pages. Este projeto visa fornecer uma solução simples e eficaz para o controle de estoque e patrimônio, permitindo o cadastro de itens, consulta por chamados e patrimônios, e gerenciamento de usuários com diferentes níveis de acesso.

## Funcionalidades✔️

🔍 Pesquisa por Chamado e Patrimônio: Busque itens no inventário utilizando campos específicos.

📝 Cadastro de Itens: Adicione novos itens ao inventário, incluindo detalhes como número de patrimônio, descrição, e localização.

📊 Gerenciamento de Usuários: Controle de usuários com permissões de administrador para gerenciamento de itens e usuários.

🖼️ Interface Amigável

📁 Exportação de Dados: Exportação de registros de inventário para análise externa.

## Screenshots📷

Figura 1: Tela inicial do sistema de gerenciamento de estoque.

![PaginaInicial](https://github.com/rbjardim/EstoqueWEB/assets/118462663/3c538da1-e8f3-477b-9e6c-838a99d231bb)

Figura 2: Tela de cadastro de novo item no inventário.

![TelaUsuario](https://github.com/rbjardim/EstoqueWEB/assets/118462663/541a0170-9ed5-4be7-bd41-3cd534bef1bc)

Figura 3: Tela de pesquisa de itens por chamado ou patrimônio.

![TelaUsuarioDev](https://github.com/rbjardim/EstoqueWEB/assets/118462663/5d7721c7-6967-4e47-8fe6-f4b409c2722c)

## Tecnologias Utilizadas👨🏽‍💻
Este projeto utiliza as seguintes tecnologias e frameworks:

- ASP.NET Core 6: Framework principal para o desenvolvimento da aplicação web.
- Razor Pages: Utilizado para a criação de páginas dinâmicas com código C#.
- Entity Framework Core: ORM utilizado para interagir com o banco de dados MySQL Server.
- MySQL Server: Banco de dados relacional para armazenamento de informações de inventário e usuários.
- Bootstrap: Framework front-end para a criação de layouts responsivos.
- jQuery: Biblioteca JavaScript para manipulação de elementos e interatividade.
- Visual Studio 2022: IDE utilizada para desenvolvimento e depuração da aplicação.

## Instalação📥
Para rodar o projeto localmente, siga os passos abaixo:

Crie uma pasta no C:
Navegue até ela via cmd com o seguinte comando:
```bash
cd c:\Nome Da sua pasta
```

Após acessar a pasta via cmd, clone o repositório:
```bash
git clone https://github.com/rbjardim/EstoqueWEB.git
```

Repositório clonado, navegue até onde foi salvo o mesmo e o execute com o Visual Studio

![image](https://github.com/user-attachments/assets/9944e0d9-5a55-4d96-9057-d3219439f63b)

Navegue até a pasta seguinte e exclua a pasta "MIGRATIONS"

![image](https://github.com/user-attachments/assets/bc60fb25-3a79-434f-8f8f-b1b5ae3321ed)

Dentro do Visual Studio, Abra o console e digite o seguinte comando para realizar a criação do banco de dados via Entity Framework

```bash
add-migration "Nome Da Sua Migration"
```

```bash
update-database
```

## Roadmap🖱
- Implementação de CRUD básico para inventário.
- Funcionalidade de pesquisa por chamado e patrimônio.
- Melhoria na interface de usuário com novas funcionalidades..

## Contato📲
<a href = "https://www.linkedin.com/in/ruan-bueno-jardim/"> <img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white">
</a>
