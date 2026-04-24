# 🌿 Ezra3 — Plant Sales & agricultural services API

A RESTful Web API built with **ASP.NET Core (.NET 10)** for a plant e-commerce platform. The system connects customers with farmers, supports plant listings with image uploads, order management, and farmer ratings.

---

## 🚀 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10 Web API |
| ORM | Entity Framework Core 10 (SQL Server) |
| Auth | ASP.NET Core Identity (JWT-based) |
| Image Storage | Cloudinary |
| Mapping | Mapster |
| Validation | FluentValidation |
| API Docs | Scalar (OpenAPI) |

---

## 📁 Project Structure

```
Graduation/
├── Controllers/         # API endpoints
├── Services/            # Business logic
├── Entities/            # Database models
├── Contracts/           # Request/Response DTOs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Migrations/
├── Enums/               # Shared enumerations
├── Mapping/             # Mapster configuration
├── Dependancies.cs      # Service registration
└── Program.cs           # App entry point
```

---

## 🌱 Domain Overview

### Entities

- **Plant** — A product listing with name, description, category, climate type, suitable location, care tips, price, and optional planting service price. Supports multiple photos.
- **FarmerProfile** — A farmer account linked to an ASP.NET Identity user. Farmers can manage plants and fulfill orders.
- **Order** — A customer purchase that includes delivery address, optional planting service, scheduling, and status tracking.
- **OrderItem** — Individual items within an order, referencing a plant and an assigned farmer.
- **FarmerRating** — Customer ratings submitted for farmers.
- **UserProfile** — Extended profile for customer users.
- **PlantPhoto** — Cloudinary-hosted images associated with a plant.


### Identity (built-in)
Standard ASP.NET Core Identity endpoints are mapped at the root for registration, login, token refresh, etc.

---

## ⚙️ Configuration

### `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=...;database=...;Trusted_Connection=true;TrustServerCertificate=true"
  },
  "Cloudinary": {
    "CloudName": "YOUR_CLOUD_NAME",
    "ApiKey": "YOUR_API_KEY",
    "ApiSecret": "YOUR_API_SECRET"
  }
}
```

> ⚠️ **Do not commit real credentials.** Move secrets to `appsettings.Development.json` or use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets).

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (or LocalDB for development)
- A [Cloudinary](https://cloudinary.com/) account

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/Muhammed-Gamall/Ezra3.git
   cd Graduation
   ```

2. **Configure your settings**
   Update `appsettings.json` (or use User Secrets) with your SQL Server connection string and Cloudinary credentials.

3. **Apply database migrations**
   ```bash
   cd Graduation
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Explore the API**
   Open your browser and navigate to:
   ```
   https://localhost:{port}/scalar
   ```
   This opens the interactive Scalar API documentation.

---

## 📦 Dependencies

| Package | Version | Purpose |
|---|---|---|
| `CloudinaryDotNet` | 1.28.0 | Image upload & management |
| `FluentValidation.AspNetCore` | 11.3.1 | Input validation |
| `Mapster` | 10.0.5 | Object-to-object mapping |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | 10.0.5 | Authentication & authorization |
| `Microsoft.EntityFrameworkCore.SqlServer` | 10.0.5 | SQL Server database access |
| `Scalar.AspNetCore` | 2.13.1 | API documentation UI |
| `Microsoft.AspNetCore.OpenApi` | 10.0.3 | OpenAPI schema generation |
