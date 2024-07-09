# MicroShop

MicroShop é uma aplicação web desenvolvida em ASP.NET Core utilizando arquitetura de microsserviços. O projeto visa criar uma plataforma de comércio eletrônico escalável e modularizada, oferecendo funcionalidades de gerenciamento de produtos, autenticação JWT para usuários e controle de acesso baseado em papéis (roles).

## Register
![RegisterPage](https://github.com/leonardolucusi/MicroShop/assets/61367434/aaae20e8-416f-4178-a8b7-6deeaf4cd419)
## Login
![LoginPage](https://github.com/leonardolucusi/MicroShop/assets/61367434/ee23f91c-c5cc-4eab-acce-104da495599d)
## Edit User
![EditUserInfo](https://github.com/leonardolucusi/MicroShop/assets/61367434/43086630-7546-433d-b2a9-586799e5a3d8)
## User Product View
![UserProductView](https://github.com/leonardolucusi/MicroShop/assets/61367434/a86397c8-de7e-4c33-8bc8-85b51d42aeb9)
![ProductAddedToCart](https://github.com/leonardolucusi/MicroShop/assets/61367434/fbbbaa39-aa32-4cd8-a1a9-3f12b4f7c801)
![ProductRemovedFromCart](https://github.com/leonardolucusi/MicroShop/assets/61367434/cc51b98b-2f92-4d27-9274-421691d9f23f)
![ProductAPI](https://github.com/leonardolucusi/MicroShop/assets/61367434/96042733-a5d2-4ff7-bfbc-e44dca74619e)
## Admin Product View
![AdminProductView](https://github.com/leonardolucusi/MicroShop/assets/61367434/eda08dac-6062-432b-be5c-a7919cfe3a56)
![AdminViewCreateProduct](https://github.com/leonardolucusi/MicroShop/assets/61367434/61067597-44f8-460f-b134-88efbf252837)
![EditProductAdminView](https://github.com/leonardolucusi/MicroShop/assets/61367434/8ff59c98-24f7-4a2e-aafa-a899191f7f9e)
![AdminViewDeleteProduct](https://github.com/leonardolucusi/MicroShop/assets/61367434/89f5167e-17e2-4f88-a6cb-0fa3c269ba7b)
## Cart
![EstoqueNoMaximo](https://github.com/leonardolucusi/MicroShop/assets/61367434/caf2d259-c47d-4449-9e8b-21bedb331a97)
![cartAPI](https://github.com/leonardolucusi/MicroShop/assets/61367434/9772559e-9fbe-44e9-9542-697ce52704a1)

## Arquitetura de Microsserviços

O MicroShop adota uma arquitetura de microsserviços para promover a separação de preocupações e a escalabilidade. Cada microsserviço é responsável por uma parte específica da funcionalidade da aplicação, permitindo um desenvolvimento e implantação independentes.

## Funcionalidades Principais

- **Gerenciamento de Produtos**: Capacidade de adicionar, visualizar, atualizar e excluir produtos.
- **Autenticação JWT**: Login seguro para usuários com tokens JWT para autenticação e autorização.
- **Controle de Acesso Baseado em Papéis**: Definição de papéis (roles) como 'ADMIN' e 'CLIENT' para acesso diferenciado a recursos da aplicação.
- 
## Tecnologias Utilizadas

- **ASP.NET Core**: Framework utilizado para o desenvolvimento backend da aplicação.
- **JWT**: Utilizado para autenticação segura entre microsserviços e com os clientes da aplicação.
- **Entity Framework Core**: Framework ORM para interação com banco de dados.
- **Swagger**: Documentação e teste de APIs.
- **AutoMapper**: Mapeamento de objetos entre camadas da aplicação.
- 
## Pré-requisitos

- .NET Core 8 SDK
- SQL Server
- Visual Studio ou qualquer IDE compatível com .NET

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
