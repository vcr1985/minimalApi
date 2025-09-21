# Projeto 2025 - API Minimal com Testes

Este projeto consiste em uma **API Minimal em .NET 8**, com autenticação JWT, CRUD de veículos, banco MySQL e testes automatizados com xUnit e InMemoryDatabase.

---

## Tecnologias Utilizadas

- .NET 8 Minimal API
- MySQL
- Entity Framework Core
- JWT Authentication
- xUnit para testes
- InMemoryDatabase para testes automatizados
- Swagger para documentação
- CORS configurado

---

## Endpoints Principais

### Administradores

| Método | Rota                    | Descrição                        |
|--------|------------------------|----------------------------------|
| POST   | /administradores/login  | Autenticação e geração de token  |
| POST   | /administradores/seed   | Seed de administrador (com JWT) |

### Veículos (Protegido por JWT)

| Método | Rota              | Descrição                    |
|--------|-----------------|-------------------------------|
| GET    | /veiculos        | Listar veículos              |
| POST   | /veiculos        | Criar veículo                |
| PUT    | /veiculos/{id}   | Atualizar veículo            |
| DELETE | /veiculos/{id}   | Remover veículo              |

---

## Diagrama de Arquitetura

```mermaid
flowchart TD
    A[Cliente] -->|Requisições HTTP| B[API Minimal]
    B --> C[Endpoints]
    C --> D[Login /administradores/login]
    C --> E[Veiculos /veiculos]
    E --> F[CRUD: GET, POST, PUT, DELETE]
    B --> G[Serviços]
    G --> H[AdministradorServico]
    G --> I[VeiculoServico]
    B --> J[Banco de Dados]
    J --> K["MySQL ou InMemory (Testes)"]
