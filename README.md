# 👨‍💼 Employee Management System

A full-stack Employee Management web application built with **ASP.NET Core 9 MVC**.

## ✨ Features

| Feature | Description |
|---------|-------------|
| 👤 Employee CRUD | Add, Edit, Delete, View Employees |
| 🔍 Search & Filter | Search by name, email or department |
| 🖼️ Profile Image | Upload employee profile photos |
| 📄 Pagination | Handle large employee data |
| 🏢 Departments | Manage departments & assign employees |
| 📊 Dashboard | Stats & Charts with Chart.js |
| 🔐 Authentication | Login/Register with ASP.NET Core Identity |
| 👮 Authorization | Role-Based Access — Admin & Employee |
| 🗑️ AJAX Delete | Delete without page refresh |
| 🪵 Logging | Serilog logging to file & console |
| ⚠️ Exception Handling | Global exception middleware |

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| Framework | ASP.NET Core 9 MVC |
| Language | C# |
| Database | SQL Server + Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| Authorization | Role-Based (Admin / Employee) |
| Charts | Chart.js |
| UI | Bootstrap 5 + Bootstrap Icons |
| Logging | Serilog |

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server / LocalDB
- Visual Studio 2022

### Setup Steps
```bash
# 1. Clone the repo
git clone https://github.com/RohitSanjayMali/EmployeeManagementSystem.git

# 2. Run migrations
dotnet ef database update

# 3. Run the project
dotnet run
```

## 🔐 Default Admin Credentials

| Field | Value |
|-------|-------|
| Email | admin@employee.com |
| Password | Admin@123 |

## 📌 Pages

| Page | Description |
|------|-------------|
| `/Auth/Login` | Login page |
| `/Auth/Register` | Register page |
| `/Admin/Index` | Dashboard |
| `/Employee/Index` | Employee list |
| `/Employee/Create` | Add employee |
| `/Department/Index` | Departments |

## 🏗️ Architecture

## 👨‍💻 Author

**Rohit Sanjay Mali**
[GitHub](https://github.com/RohitSanjayMali)

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
