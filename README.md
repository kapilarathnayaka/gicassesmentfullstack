# gicassesmentfullstack

## How to Compile and Run the Code

### Backend Setup

1. **Install .NET 8.x SDK**: Ensure you have the .NET 8.x SDK installed on your machine. You can download it from the official [.NET website](https://dotnet.microsoft.com/download).

2. **Clone the Repository**: Clone this repository to your local machine using the following command:
   ```bash
   git clone https://github.com/kapilarathnayaka/gicassesmentfullstack.git
   ```

3. **Navigate to Backend Directory**: Change your directory to the backend folder:
   ```bash
   cd gicassesmentfullstack/backend
   ```

4. **Restore Dependencies**: Restore the required dependencies using the following command:
   ```bash
   dotnet restore
   ```

5. **Update Database Connection String**: Update the database connection string in the `appsettings.json` file to point to your RDMS database (MSSQL, Postgres, MySQL, etc.).

6. **Apply Migrations**: Apply the database migrations to set up the database schema:
   ```bash
   dotnet ef database update
   ```

7. **Run the Backend**: Start the backend server using the following command:
   ```bash
   dotnet run
   ```

### Frontend Setup

1. **Install Node.js**: Ensure you have Node.js installed on your machine. You can download it from the official [Node.js website](https://nodejs.org/).

2. **Navigate to Frontend Directory**: Change your directory to the frontend folder:
   ```bash
   cd gicassesmentfullstack/frontend
   ```

3. **Install Dependencies**: Install the required dependencies using the following command:
   ```bash
   npm install
   ```

4. **Run the Frontend**: Start the frontend development server using the following command:
   ```bash
   npm start
   ```

### Database Setup and Seed Data

1. **Database Design**: The database is designed to handle the following data:
   - Employee data
   - Café data
   - Employee-Café relationship

2. **Seed Data**: The seed data for the database is provided in the `backend/Data/SeedData.cs` file. This file includes sample data for employees and cafes.

3. **Applying Seed Data**: The seed data will be automatically applied when you run the backend server.

## Endpoints

### Café Endpoints

- **GET /cafes?location=<location>**: Retrieve a list of cafes, optionally filtered by location.
- **POST /cafe**: Create a new café.
- **PUT /cafe**: Update an existing café.
- **DELETE /cafe**: Delete an existing café and all employees under it.

### Employee Endpoints

- **GET /employees?cafe=<café>**: Retrieve a list of employees, optionally filtered by café.
- **POST /employee**: Create a new employee and assign them to a café.
- **PUT /employee**: Update an existing employee and their café assignment.
- **DELETE /employee**: Delete an existing employee.

## Frontend Pages

### Café Page

- Displays a list of cafes in a table.
- Allows filtering by location.
- Provides options to add, edit, and delete cafes.
- Clicking on the employees count navigates to the Employee page.

### Employee Page

- Displays a list of employees in a table.
- Allows filtering by café.
- Provides options to add, edit, and delete employees.

### Add/Edit Café Page

- Form to add or edit a café.
- Includes validation for form fields.
- Warns user of unsaved changes before navigating away.

### Add/Edit Employee Page

- Form to add or edit an employee.
- Includes validation for form fields.
- Warns user of unsaved changes before navigating away.
