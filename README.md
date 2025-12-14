# ExpenseTracker

**ExpenseTracker** is a personal expense management application designed to help users track their spending, organize categories, and generate accurate expense reports effortlessly.

---

## 🎯 Project Goals
- Allow users to add, update, and delete expenses and their related categories.
- Provide clear insights into expenses through comprehensive reports.
- Showcase a professional API using modern backend technologies.
- Demonstrate the ability to build a well-structured, end-to-end project.

---

## 💡 Key Features
- Full CRUD operations for Categories and Expenses.
- Secure user registration and login using JWT authentication.
- Well-structured API, ready to integrate with any frontend.
- Comprehensive expense reports categorized by date and category.
- SQL Server database integration with Entity Framework Core.
- AutoMapper for efficient DTO handling.
- Optional Redis caching to enhance performance.

---

## 🛠 Technologies Used
- **Backend:** ASP.NET Core 8, C#
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Authentication:** JWT
- **Mapping:** AutoMapper
- **Caching:** Redis (optional)

---

## 🚀 How to Run Locally
1. Clone the repository:
   ```bash
   git clone https://github.com/Mo7sensaber/ExpenseTracker.git
Navigate to the project folder:

bash
نسخ الكود
cd ExpenseTracker
Restore dependencies:

bash
نسخ الكود
dotnet restore
Apply migrations (if using migrations):

bash
نسخ الكود
dotnet ef database update
Run the project:

bash
نسخ الكود
dotnet run
Access the API endpoints:

bash
نسخ الكود
http://localhost:8090/api/categories
📂 Project Structure
Presentation: Contains the API Controllers.

Service: Contains business logic and services.

Domain: Contains models and interfaces.

Persistence: Contains DbContext, repositories, and data seeding.

Shared: Contains DTOs and shared models.

🔑 Notes
Make sure to configure the database connection in appsettings.json.

Security: passwords and JWT secrets can be stored in appsettings.Development.json or environment variables.

📌 Why This Project Matters
This project demonstrates my ability to:

Build professional RESTful APIs.

Work with relational databases effectively.

Write clean, organized, and maintainable code (Clean Code & SOLID Principles).

Execute a full end-to-end project from design to deployment.

Mohsen Saber
Backend Developer | .NET | SQL Server | EF Core
