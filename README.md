# Contact Manager

It's Contact Manager web app created on .Net 8 with clean architecture.
it's allows to add, modify and delete contact and addresses related to contact.


## Set Up Database

We are using the Entity Framework code-first approach. Follow these steps to create the database with initial data:

### Step 1: Add Connection String

Add a valid connection string in the `appsettings.json` file.

### Step 2: Add Initial Migration

Go to the Package Manager Console and run the following command:
Add-Migration InitialMigration -StartupProject App.Web -Project App.Data

Note: This command creates a migration folder in the App.Data project with migration scripts.

### Step 3 : Update Database 

Update-Database InitialMigration -StartupProject App.Web

Note: This command will create the database (if it does not already exist) and apply the migration scripts.


## Technology or Library Used in This Project

- .NET 8 Framework
- Entity Framework Core
- NUnit (For unit testing)
- DataTable (for UI grid)
- AutoMapper (for mapping entities to model)

## Architecture and Design Patterns Used

- Clean Architecture (with necessary layers for managing separate concerns)
- Dependency Injection (to achieve loose coupling)
- Repository Pattern (for abstract ORM like Entity Framework)