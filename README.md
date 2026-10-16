# 🥬 DailyVegetableTracker

**Track your daily vegetable purchases and manage your household spending with ease.**

DailyVegetableTracker is a web application built with ASP.NET Core MVC and MySQL to help users record vegetable purchases, manage vegetables, review purchase history, and monitor daily and monthly expenses.

🌐 **Live Demo:** [DailyVegetableTracker](https://dailyvegetabletracker.runasp.net/)

💻 **Source Code:** [GitHub Repository](https://github.com/Shanthoshg/DailyVegetableTracker)

---

## 📸 Application Screenshots

### 1. Dashboard Overview

The dashboard provides an overview of daily spending, monthly expenses, vegetables tracked, average purchase amount, recent purchases, and spending by vegetable.

![DailyVegetableTracker Dashboard](screenshots/dashboard.png)

### 2. My Vegetables

The vegetable section displays available vegetables, their categories, measurement units, and links to view their details.

![DailyVegetableTracker My Vegetables](screenshots/vegetables.png)

---

## ✨ Features

### 📊 Dashboard

- View today's total spending.
- Monitor current month's spending.
- See the number of vegetables tracked.
- View average purchase statistics.
- Review recent purchase records.
- Compare monthly spending by vegetable.

### 🥦 Vegetable Management

- View the list of available vegetables.
- Store vegetable names and categories.
- Specify measurement units such as kg, bunch, dozen, and piece.
- Access vegetable details.
- Extend the application with vegetable creation, editing, and deletion workflows.

### 🛒 Purchase Tracking

- Record vegetable purchases.
- Enter quantity, price, and purchase date.
- Calculate purchase totals.
- Store purchase records in the MySQL database.

### 📅 Purchase History

- Review previous purchases.
- View purchase dates, vegetables, quantities, and totals.
- Track daily and monthly spending.
- Support future filtering and search improvements.

_Note: Features are subject to the functionality currently implemented in the application._

---

## 🛠️ Technologies Used

| Technology            | Purpose                                 |
| --------------------- | --------------------------------------- |
| C#                    | Backend programming language            |
| ASP.NET Core MVC      | Web application framework               |
| Entity Framework Core | Database access and ORM                 |
| MySQL                 | Relational database                     |
| Razor Views           | Dynamic server-rendered pages           |
| HTML5                 | Web page structure                      |
| CSS3                  | Styling                                 |
| Bootstrap             | Responsive UI components                |
| JavaScript            | Client-side interactions                |
| MySQL Workbench       | Database management                     |
| Git and GitHub        | Version control and source code hosting |
| MonsterASP.NET        | Application hosting                     |

---

## 📁 Project Structure

```text
DailyVegetableTracker/
│
├── Controllers/
│   └── Application controllers
│
├── Data/
│   └── Database context and data access
│
├── Migrations/
│   └── Entity Framework Core migrations
│
├── Models/
│   └── Application data models
│
├── ViewModels/
│   └── View-specific data models
│
├── Views/
│   ├── Shared/
│   └── MVC Razor views
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── Other static assets
│
├── Program.cs
├── appsettings.json
├── .gitignore
└── DailyVegetableTracker.csproj
```

_This is a high-level overview. Individual files and folders may vary in your current project._

---

## ⚙️ Prerequisites

Install the following software before running the application locally:

- [.NET SDK](https://dotnet.microsoft.com/download) compatible with the project's target framework.
- [MySQL Server](https://dev.mysql.com/downloads/mysql/).
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) (optional).
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).
- [Git](https://git-scm.com/downloads).

---

## 🚀 Getting Started

### Step 1: Clone the repository

Open Git Bash or a terminal and run:

```bash
git clone https://github.com/Shanthoshg/DailyVegetableTracker.git
cd DailyVegetableTracker
```

### Step 2: Check the .NET SDK

```bash
dotnet --version
```

Verify that the installed SDK is compatible with the project's target framework.

### Step 3: Configure MySQL

Create the database if it does not already exist:

```sql
CREATE DATABASE daily_vegetable_tracker;
```

If the database already exists, do not recreate it unnecessarily.

### Step 4: Configure the database connection

Configure the MySQL connection string using .NET User Secrets for local development.

From the project directory containing `DailyVegetableTracker.csproj`, run:

```bash
dotnet user-secrets init
```

Then configure the connection string using your own local credentials:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;port=3306;database=daily_vegetable_tracker;user=YOUR_MYSQL_USERNAME;password=YOUR_MYSQL_PASSWORD;"
```

Replace the placeholder username and password with your actual local MySQL credentials. Never publish real credentials in this README or commit them to GitHub.

**Important:** This example assumes the application reads a connection string named `DefaultConnection`. Check `Program.cs` and the existing configuration before using it. If your project uses a different connection-string name or database provider, follow the existing implementation.

### Step 5: Restore project dependencies

```bash
dotnet restore
```

### Step 6: Apply database migrations

First, confirm the configured database connection and existing migrations.

If the project uses Entity Framework Core migrations, install or use the compatible EF Core command-line tool, then run:

```bash
dotnet ef database update
```

Run this command from the directory containing the project file, or specify the project path if needed.

Do not delete existing migrations or reset the database to resolve migration errors.

### Step 7: Run the application

```bash
dotnet run
```

Open the local URL shown in the terminal.

The application has previously run at:

http://localhost:5194/

The actual URL may vary according to your launch settings.

---

## 🗄️ Database

The application uses MySQL to persist vegetable and purchase information.

**Database name:** `daily_vegetable_tracker`

Entity Framework Core manages database access and migrations, according to the existing project configuration.

The database is intended to support information such as:

- Vegetable names, categories, and measurement units.
- Purchase quantities and prices.
- Purchase dates and calculated totals.
- Data used to generate dashboard statistics and purchase history.

The exact tables and relationships are defined by the project's current models and migrations.

---

## 🔐 Security

- Never commit database passwords, API keys, or other secrets.
- Use .NET User Secrets for local development.
- Use the hosting provider's supported secure configuration settings for production.
- Rotate any credentials that may have been exposed.
- Review Git history if sensitive credentials were committed previously.
- Do not share database connection strings containing real credentials.

---

## 🔮 Future Improvements

- Complete and refine vegetable CRUD operations.
- Improve purchase editing and deletion workflows.
- Add purchase history search and date filters.
- Add monthly and yearly expense reports.
- Introduce charts and visual spending analytics.
- Improve form validation and error handling.
- Enhance responsive design for mobile devices.
- Add automated tests and other reliability improvements.

---

## 🌐 Deployment

The application is hosted on MonsterASP.NET.

**Live application:** https://dailyvegetabletracker.runasp.net/

The live application's availability and features depend on the current deployment and hosting configuration.

---

## 👨‍💻 Author

**Shanthosh G**

- GitHub: [Shanthoshg](https://github.com/Shanthoshg)
- Project Repository: [DailyVegetableTracker](https://github.com/Shanthoshg/DailyVegetableTracker)

---

## 📄 License

No license has been specified here. Unless a license is added to the repository, the project should not be assumed to be open-source licensed for unrestricted reuse.

---

**DailyVegetableTracker — Make everyday vegetable spending easier to track!** 🥬
