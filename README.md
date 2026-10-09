# Pedal & Spoke (BikeStores)

An ASP.NET MVC management system for a bicycle retail chain, built on the BikeStores sample database. It has a management dashboard, maintenance screens for staff, customers and products, and a reporting module that charts sales and exports reports to Word, Excel and PDF.

Built for INF 272 at the University of Pretoria.

## Features

- **Dashboard**: an overview of staff, customers and products, with brand and category filters and paging.
- **Maintenance**: search, add, edit and delete staff, customers and products in modal forms, with paging on each list.
- **Reporting**:
  - Charts of top-selling products and top stores (Chart.js).
  - Save a report as **Word (.docx)**, **Excel (.xlsx)** or **PDF**, with the charts embedded.
  - A report archive where you can download, delete or add a description to each saved report.

## Tech stack

- ASP.NET MVC 5 on .NET Framework 4.7.2 (C#)
- Entity Framework 6 (database-first, `.edmx`) with SQL Server LocalDB
- Razor views, Bootstrap 5, jQuery and Chart.js
- Report generation with Open XML SDK (Word), EPPlus (Excel) and iText (PDF)

## Database

The app uses the **BikeStores** sample database from [sqlservertutorial.net](https://www.sqlservertutorial.net/getting-started/sql-server-sample-database/). Its tables are `brands`, `categories`, `customers`, `orders`, `order_items`, `products`, `staffs`, `stocks` and `stores`.

## Running locally

1. Open `domBikeStores.sln` in Visual Studio 2022 with the **ASP.NET and web development** workload installed. That workload includes SQL Server LocalDB.
2. Create the database:
   1. Download the sample database scripts from the link above.
   2. Connect to `(localdb)\MSSQLLocalDB` in **SQL Server Object Explorer** (or SQL Server Management Studio) and create a database called `BikeStores`.
   3. Run the *create objects* script, then the *load data* script, against `BikeStores`.
3. Press **F5**. NuGet packages are restored automatically on the first build.

The connection string is `BikeStoresEntities` in `domBikeStores/Web.config`. Saved reports are written to `domBikeStores/Reports/`, which is created automatically.

## Project structure

```
domBikeStores/
├── Controllers/
│   ├── HomeController.cs       # Dashboard
│   ├── MaintainController.cs   # Staff, customer and product maintenance
│   ├── ReportController.cs     # Charts and Word/Excel/PDF export
│   └── Customers/Products/StaffsController.cs   # Scaffolded CRUD pages
├── Models/
│   ├── BikeStoresModel.edmx    # Entity Framework model
│   └── ViewModels/
└── Views/
```
