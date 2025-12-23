# EComproj (ASP.NET Web Forms + SQL Server)

A simple e-commerce web application built with ASP.NET Web Forms (not MVC), Entity Framework 6, ASP.NET Identity, SQL Server 2022, and MSTest unit tests.

## Features

- Roles: Admin, Seller, Customer
- Registration with role selection; Customer can select interests (categories)
- Product lifecycle:
  - Seller adds products (Pending)
  - Admin approves/rejects (Approved visible to customers; Reject hard-deletes)
  - Seller can edit (core changes require re-approval) and delete (non-approved only)
- Catalog:
  - Approved products only
  - Search, filter by category, paging
  - Product details
- Cart and Checkout:
  - Session-based cart
  - Dummy payment, creates Order + OrderItems
  - Stock validation and stock reduction
  - My Orders page
- Recommendations:
  - Based on Customer interests and product click popularity
- Unit tests (MSTest):
  - Recommendation engine
  - Cart total
  - Product approval transitions
  - Validation and checkout stock
- GitHub Actions CI: builds solution and runs tests on push/PR

## Prerequisites

- Windows 10/11
- Visual Studio 2022 (ASP.NET and web development workload)
- .NET Framework 4.8 targeting pack
- SQL Server 2022 (e.g., .\\SQLEXPRESS)
- SSMS (optional but recommended)

## Getting Started

1. Clone the repo and open `EComproj.sln` in Visual Studio.
2. Update `Web.config` connection string `DefaultConnection` to your SQL Server instance (e.g., `Server=.\\SQLEXPRESS;Database=EComprojDb;Trusted_Connection=True;MultipleActiveResultSets=true`).
3. Run EF migrations:
   - Tools > NuGet Package Manager > Package Manager Console:
     - `Enable-Migrations` (once)
     - `Add-Migration InitialIdentity` (once)
     - `Update-Database`
     - `Add-Migration DomainModels`
     - `Update-Database`
4. Run (F5). Login with seeded admin:
   - Email: `daniyalawan12317@gmail.com`
   - Password: `12345` (password policy relaxed for demo)
5. Register as Seller to add products. Register as Customer to browse, get recommendations, and checkout.

## Tests

- Project: `EComproj.Tests`
- Run: Test > Run All Tests in Visual Studio
- CI: GitHub Actions workflow `.github/workflows/dotnet-framework-tests.yml` runs on push/PR.

## Notes

- Password policy relaxed in `App_Start/IdentityConfig.cs` and seeding for demo simplicity.
- Rejected products are hard-deleted along with images (per requirements).
- Images saved under `~/Uploads/`. Ensure the folder exists and app has write permissions.

## Roadmap / Ideas

- Pagination UI improvements (page numbers)
- Address fields at checkout
- Order details view
- Email notifications
- Soft delete toggle
