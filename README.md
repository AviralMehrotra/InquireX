# InquireX
![ChatGPT Image Mar 31, 2025, 09_35_46 PM](https://github.com/user-attachments/assets/1019f69a-b746-4656-b9b2-e05d8b824f1b)

InquireX is a robust ASP.NET MVC application designed for efficient management of user queries with role-based access control. The application includes CRUD operations, role-based functionalities, and a streamlined query management system.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- **User Management**: Create, read, update, and delete users.
- **Role-Based Access**: Assign roles to users (Admin, Client, Developer, HR) with specific permissions.
- **Query Management**: Users can raise and manage queries based on their roles.
- **Session Management**: Secure session handling for user roles and email.

## Technologies Used

- **ASP.NET MVC**
- **SQL Server**
- **Bootstrap**
- **Entity Framework (optional)**
- **Visual Studio**

## Installation

### Prerequisites

- **Visual Studio 2019 or later**
- **SQL Server**
- **.NET Framework 4.6.1 or later**

### Steps

1. Clone the Repository
  
    ```sh
    git clone https://github.com/AvirarlMehrotra/InquireX.git
    cd InquireX
    ```

2. Open in Visual Studio

    - Open InquireX.sln in Visual Studio.

3. Restore NuGet Packages

    - Go to Tools > NuGet Package Manager > Package Manager Console.
      
    - Run Update-Package to restore the packages.

4. Configure the Connection String

    - Open web.config.

    - Update the connection string under <connectionStrings>:

    ```xml
        <connectionStrings>
            <add name="myConfig" connectionString="Data Source=localhost;Initial Catalog=InquireX;Integrated Security=True" providerName="System.Data.SqlClient" />
        </connectionStrings>
    ```
    
## Database Setup

### Option 1: Using Backup

   1. Restore the Database
       - Open SQL Server Management Studio (SSMS).
       - Right-click on Databases and select Restore Database.
       - Choose Device and select the database_backup.bak file located in the Database folder.
       - Follow the wizard to complete the restoration.

Option 2: Using SQL Scripts

   1. Run Setup Scripts
       - Open a new query in SSMS.
       - Run the setup.sql script located in the Database folder to create the database schema.
       - Run the initial_data.sql script to insert initial data.

## Usage
  1. Run the Application
      - Press F5 in Visual Studio to run the application.

  2. Login

      - Use the following default credentials to log in:

        ```makefile
        Email: admin@admin.com
        Password: admin
        ```
  3. Explore Features
      - Navigate through different functionalities based on the user roles.

## Contributing

Contributions are welcome! Please fork this repository and submit pull requests.

   - Fork the Project
   - Create your Feature Branch (git checkout -b feature/YourFeature)
   - Commit your Changes (git commit -m 'Add some YourFeature')
   - Push to the Branch (git push origin feature/YourFeature)
   - Open a Pull Request

## License

Distributed under the MIT License. See LICENSE for more information.

<hr>
