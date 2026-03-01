# DotNet Business Workflow API

A layered ASP.NET Core Web API for managing core business workflows: authentication, customers, products, orders, payments, invoices, invoice PDF generation, and invoice email delivery.

## Overview

This project is organized using a clean/layered architecture:

- **Domain**: business entities, value objects, enums, and repository contracts.
- **Application**: DTOs, validators, use cases, and shared application interfaces.
- **Infrastructure**: Entity Framework Core persistence, repository implementations, JWT/auth helpers, SMTP email sender, and PDF generation.
- **API**: HTTP controllers, JWT authentication/authorization, and Swagger/OpenAPI.

## Tech Stack

- **.NET 10** (`net10.0` target framework)
- **ASP.NET Core Web API**
- **Entity Framework Core 10**
- **PostgreSQL** (`Npgsql.EntityFrameworkCore.PostgreSQL`)
- **JWT Bearer Authentication**
- **FluentValidation**
- **QuestPDF** (invoice PDF generation)
- **SMTP** (invoice email delivery)
- **Swagger / OpenAPI**

## Solution Structure

```text
DotNetBusinessWorkFlow.Api/             # Web API entry point, controllers, app config
DotNetBusinessWorkFlow.Application/     # Use cases, DTOs, validators, service interfaces
DotNetBusinessWorkFlow.Domain/          # Entities, enums, value objects, repository contracts
DotNetBusinessWorkFlow.Infrastructure/  # EF Core, repositories, auth, PDF/email services
DotNetBusinessWorkFlow.slnx             # Solution file
```

## Core Business Modules

- **Auth**: user login and JWT token issuance.
- **Customers**: create, update, deactivate, and list customer records.
- **Products**: create, update, deactivate, and list catalog products.
- **Orders**:
  - create order
  - add order items
  - confirm, pay, complete, cancel lifecycle actions
  - fetch by ID, list all, list by customer
- **Payments**: create payment and fetch payment by order.
- **Invoices**:
  - create invoice from order
  - fetch by ID / list all
  - generate and send invoice email

## Order Lifecycle

Order status flow in domain logic:

`Created -> Confirmed -> Paid -> Completed`

Alternative branch:

`Created/Confirmed -> Cancelled`

> Paid or completed orders cannot be cancelled.

## Role-Based Access

JWT roles are used for authorization:

- **Admin**: full access, including customer/product create/update/deactivate.
- **Manager**: operational access (e.g., viewing records, order confirmation/completion, payments/invoices).

Most endpoints require authentication. `POST /api/auth/login` is public.

## Seeded Users (Development)

The infrastructure seed includes two users:

- `admin@company.com` / `Admin@123` (Admin)
- `manager@company.com` / `Manager@123` (Manager)

Use these credentials to obtain a JWT in local development.

## Prerequisites

- .NET 10 SDK
- PostgreSQL 15+ (or compatible)
- A configured SMTP account (for invoice email endpoint)

## Configuration

1. Copy `DotNetBusinessWorkFlow.Api/appsettings.example.json` to `DotNetBusinessWorkFlow.Api/appsettings.json`.
2. Update the values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=your_db;Username=your_user;Password=your_password"
  },
  "Jwt": {
    "Key": "YOUR_LONG_RANDOM_SECRET",
    "Issuer": "DotNetBusinessWorkFlow",
    "Audience": "DotNetBusinessWorkFlowUsers",
    "ExpiryMinutes": 60
  },
  "Email": {
    "From": "your@email.com",
    "SmtpHost": "smtp.provider.com",
    "Port": 587,
    "Username": "smtp_username",
    "Password": "smtp_password"
  },
  "Application": {
    "SeedData": true
  }
}
```

## Running Locally

From repository root:

```bash
dotnet restore
dotnet build
```

Run the API:

```bash
dotnet run --project DotNetBusinessWorkFlow.Api
```

By default (development), Swagger UI is enabled.

## Database Migrations

Existing EF Core migrations are included in `DotNetBusinessWorkFlow.Infrastructure/Migrations`.

Apply migrations:

```bash
dotnet ef database update \
  --project DotNetBusinessWorkFlow.Infrastructure \
  --startup-project DotNetBusinessWorkFlow.Api
```

Create a new migration:

```bash
dotnet ef migrations add <MigrationName> \
  --project DotNetBusinessWorkFlow.Infrastructure \
  --startup-project DotNetBusinessWorkFlow.Api
```

## Authentication Quick Start

### 1) Login

`POST /api/auth/login`

```json
{
  "email": "admin@company.com",
  "password": "Admin@123"
}
```

Copy the JWT token from the response.

### 2) Authorize in Swagger

- Open Swagger UI.
- Click **Authorize**.
- Enter token as: `Bearer <your_token>`

## API Endpoint Summary

### Auth

- `POST /api/auth/login`

### Customers

- `POST /api/customers` (Admin)
- `PUT /api/customers/{customerId}` (Admin)
- `PATCH /api/customers/{customerId}/deactivate` (Admin)
- `GET /api/customers/{customerId}` (Admin, Manager)
- `GET /api/customers` (Admin, Manager)

### Products

- `POST /api/products` (Admin)
- `PUT /api/products/{productId}` (Admin)
- `PATCH /api/products/{productId}/deactivate` (Admin)
- `GET /api/products/{productId}` (Admin, Manager)
- `GET /api/products` (Admin, Manager)

### Orders

- `POST /api/orders`
- `POST /api/orders/{orderId}/items?productId={productId}&quantity={quantity}`
- `POST /api/orders/{orderId}/confirm` (Admin, Manager)
- `POST /api/orders/{orderId}/pay`
- `POST /api/orders/{orderId}/complete` (Admin, Manager)
- `POST /api/orders/{orderId}/cancel`
- `GET /api/orders/{orderId}`
- `GET /api/orders` (Admin, Manager)
- `GET /api/orders/customer/{customerId}`

### Payments

- `POST /api/payments` (Admin, Manager)
- `GET /api/payments/order/{orderId}` (Admin, Manager)

### Invoices

- `POST /api/invoices/{orderId}` (Admin, Manager)
- `GET /api/invoices/{invoiceId}` (Admin, Manager)
- `GET /api/invoices` (Admin, Manager)
- `POST /api/invoices/{invoiceId}/send-email` (Admin, Manager)

## Request Model Notes

- Product and payment money values use a `Money` object:

```json
{
  "amount": 1500.00,
  "currency": "INR"
}
```

- Order creation payload:

```json
{
  "customerId": "00000000-0000-0000-0000-000000000000"
}
```

## Error Handling

Controllers return an `OperationResult<T>` wrapper with HTTP status code and message payload conventions. Validation failures and business-rule violations are surfaced through use-case-level checks and exceptions.

## Notes

- Swagger is enabled only in `Development` environment.
- HTTPS redirection is enabled by default in the middleware pipeline.
- QuestPDF license is configured for community usage in infrastructure registration.

## Future Improvements

- Add automated integration and unit tests.
- Add CI pipeline for build/test/lint.
- Add refresh-token support and stricter auth hardening.
- Introduce structured logging and distributed tracing.

---

Author

Built with strong architectural discipline and production-focused design.
