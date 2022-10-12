# BOAS VINDAS AO REPOSITÓRIO TRYITTER

Este é um projeto realizado por: Max Rudim e Pedro Marques para a conclusão da aceleração C# na trybe.

O projeto foi realizado utilizando C#, Entity Framework, MS SQL Server, xUnit (testes), FluentAssertions (testes), esforço :sweat_drops: e paixão :green_heart:.

# ORIENTAÇÕES

Antes de iniciar o projeto, é necessário ter o [.NET SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) instalado em sua máquina.

Além disso, é necessário ter o [MS SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads).

Tenha instalado algum software para fazer requisições HTTP ao aplicativo criado. São alguns exemplos: Thunder Client (extensão do VS Code), [Insomnia](https://insomnia.rest/download) e [Postman](https://www.postman.com/).

Por fim, mas não menos importante, esperamos que sua pasta no computador tenha o git instalado para clonar o repositório e rodar o projeto localmente.

... Ufa, tudo certo por ai? Então vamos que vamos 🚀

# INSTALAÇÃO

## 1. Clone o repositório

### - Use o comando `git clone git@github.com:PedroMarquesFr/p-final-tryitter.git`

### - Entre na pasta do repositório que voce acabou de clonar:
###   - `cd p-final-tryitter`

## 2. Instale as dependências

### - Entre na pasta `src/`
### - Execute o comando `dotnet restore`

## 3. Execute o projeto

### - Entre na pasta `Tryitter.Web` e utilize o comando `dotnet run`. Se aparecer uma mensagem contendo o endereço: `https://localhost:7272`, tudo deu certo ;D

## 4. Enviando requisições às rotas

### - O projeto possui as seguintes rotas: User `https://localhost:7272/user` e Post `https://localhost:7272/post`.

### - Antes de começar a navegar por elas, primeiramente será necessário criar um token. Para isso, faça uma requisição do tipo POST para user passando os seguinte body em JSON:

{
  "Nickname": "Seu Usuário",
  "Login": "seuemail@email.com",
	"Password": "suasenhacomnominimo8digitos"
}

### - A partir disto, seu usuário foi criado no banco de dados e você poderá logar e pegar seu token, bastando fazer uma requisição do tipo POST para `https://localhost:7272/user/authentication` contendo o seguinto body em JSON:

{
  "Login": "seulogincriado@email.com",
	"Password": "suasenhacriada"
}

### - O retorno desta rota será um token que poderá ser utilizado para efetuar todas as requisições no aplicativo ;D

### - Caso queira encontrar todas as rotas e todas as tabelas utilizadas no aplicativo, entre em navegador, como o Google Chrome, por exemplo, e digite: `https://localhost:7272/swagger`.

## 5. Testando a aplicação

### - Este projeto foi realizado utilizando testes unitários e de integração. Para rodar os testes, basta entrar na pasta `Tryitter.Test` e, sem estar com o aplicativo rodando (dê um ctrl + c no terminal caso o dotnet run esteja em execução) utilize o comando: `dotnet test`.

### - Caso queira testar os arquivos individualmente, rode o comando `dotnet test --filter (nome do arquivo sem parênteses e sem a extensão .cs)`.


# Esperamos que tenha conseguido utilizar corretamente o aplicativo. Em caso de dúvidas, sinta-se livre para nos contactar.