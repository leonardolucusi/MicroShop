# MicroShop

MicroShop é uma aplicação web desenvolvida em ASP.NET Core utilizando arquitetura de microsserviços. O projeto visa criar uma plataforma de comércio eletrônico escalável e modularizada, oferecendo funcionalidades de gerenciamento de produtos, autenticação JWT para usuários e controle de acesso baseado em papéis (roles).

## Arquitetura de Microsserviços

O MicroShop adota uma arquitetura de microsserviços para promover a separação de preocupações e a escalabilidade. Cada microsserviço é responsável por uma parte específica da funcionalidade da aplicação, permitindo um desenvolvimento e implantação independentes.

## Funcionalidades Principais

- **Gerenciamento de Produtos**: Capacidade de adicionar, visualizar, atualizar e excluir produtos.
- **Autenticação JWT**: Login seguro para usuários com tokens JWT para autenticação e autorização.
- **Controle de Acesso Baseado em Papéis**: Definição de papéis (roles) como 'ADMIN' e 'CLIENT' para acesso diferenciado a recursos da aplicação.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework utilizado para o desenvolvimento backend da aplicação.
- **JWT**: Utilizado para autenticação segura entre microsserviços e com os clientes da aplicação.
- **Entity Framework Core**: Framework ORM para interação com banco de dados.
- **Swagger**: Documentação e teste de APIs.
- **AutoMapper**: Mapeamento de objetos entre camadas da aplicação.

## Instalação e Uso

Para executar o MicroShop localmente, siga os passos abaixo:

1. Clone este repositório.
2. Configure as conexões de banco de dados e chaves JWT no arquivo `appsettings.json`.
3. Execute o projeto utilizando o Visual Studio ou o CLI do .NET.

```bash
dotnet run
```
4. Acesse a aplicação em http://localhost:5000 (ou outra porta configurada).

## Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue ou enviar um pull request com melhorias ou novas funcionalidades para o MicroShop.
