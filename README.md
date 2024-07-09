# MicroShop

MicroShop is a web application developed in ASP.NET Core using a microservices architecture. The project aims to create a scalable and modularized e-commerce platform, offering functionalities for product management, JWT authentication for users, and role-based access control.

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

## Microservices Architecture

MicroShop adopts a microservices architecture to promote separation of concerns and scalability. Each microservice is responsible for specific parts of the application's functionality, allowing independent development and deployment.

## Key Features

- **Product Management**: Ability to add, view, update, and delete products.
- **JWT Authentication**: Secure user login with JWT tokens for authentication and authorization.
- **Role-Based Access Control**: Definition of roles such as 'ADMIN' and 'CLIENT' for differentiated access to application resources.

## Technologies Used

- **ASP.NET Core**: Backend framework for application development.
- **JWT**: Used for secure authentication between microservices and application clients.
- **Entity Framework Core**: ORM framework for interacting with databases.
- **Swagger**: API documentation and testing.
- **AutoMapper**: Object mapping between application layers.

## Prerequisites

- .NET Core 8 SDK
- SQL Server
- Visual Studio or any IDE compatible with .NET

## Installation and Usage

To run MicroShop locally, follow these steps:

1. Clone this repository.
2. Configure database connections and JWT keys in the `appsettings.json` file.
3. Run the project using Visual Studio or the .NET CLI.

```bash
dotnet run
```
4. Access the application at http://localhost:5000 (or another configured port).

## Contribution
Contributions are welcome! Feel free to open an issue or pull request with improvements or new features for MicroShop.
