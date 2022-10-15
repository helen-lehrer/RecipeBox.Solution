# Dr. Sillystringz's Factory

#### An MVC Web Application: Many-To-Many Database Relationship 

#### By Helen Lehrer

## Description

 A MVC application for a factory that keeps track of machine repairs. The user is able to add a list of engineers working at the factory and assign machines to that engineer. The user is also able to add a list of machines at the factory and assign engineers to that machine. 
 
The Users Can:

* See a list of all engineers and a list of all machines.
* Select an engineer, see their details, and see a list of all machines that engineer is licensed to repair. They can also select a machine, see its details, and see a list of all engineers licensed to repair it.
* Add new engineers to the system when they are hired. They can also add new machines to the system when they are installed.
* Add new machines even if no engineers are employed. They can add new engineers even if no machines are installed
* Add or remove machines that a specific engineer is licensed to repair. They can also modify this relationship from the other side, and add or remove engineers from a specific machine.
* Navigate to a splash page that lists all engineers and machines. They can click on an individual engineer or machine to see all the engineers/machines that belong to it.

Features:

* A splash page
* Many-to-Many database relationship
* Multiple controllers 
* Use of MySQL for database management
* GET and POST requests
* MVC routes follow RESTful conventions

## Technologies Used

* .NET 5.0.401
* C#
* Git Bash
* MySQL & MySQL Workbench 8.0.30
* Entity Framework Core 5.0.0
* LINQ queries

## Setup/Installation Requirements

Enter this command into your terminal to clone the project: 
```bash
$ git clone https://github.com/helen-lehrer/Factory.Solution
```

Navigate from the root directory of the repo into the production folder `Factory`:
```bash
$ cd Factory
```

Restore dependencies and tools: 
```bash
$ dotnet restore
```

Compile the code: 
```bash
$ dotnet build
```

---

#### Database Import/Configuration:

* Download, install, and configure [MySQL](https://dev.mysql.com/downloads/installer/) & [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)

* In MySQL Workbench, In the Navigator>Administration window, select "Data Import/Restore"

* In Import Options, select "Import from Self-Contained File"

* In the field next to this, type "[directory repo was cloned into]/Factory.Solution/helen_lehrer.sql". 

* Navigate to the tab called Import Progress and click Start Import at the bottom right corner of the window.

After you are finished with the above steps, reopen the Navigator > Schemas tab. Right click and select Refresh All. The database helen_lehrer will appear. Next, you will create an **appsettings.json** file.

Navigate from the root directory of the repo into the production folder `Factory`:
```bash
$ cd Factory
```

Create a file called **appsettings.json**: 
```bash
$ touch appsettings.json
```

Add the following code to the **appsettings.json** file: 
```bash
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=helen_lehrer;uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
    }
}
```

* Make sure to insert your MySQL username and password into the  **appsettings.json**  connection string

* Note that the database will change based on the database you are connecting to and that uid and pwd may vary depending on your MySql configurations.

---

#### To run the application: 

Navigate from the root directory of the repo into the folder `Factory`:
```bash
$ cd Factory
```

Run the application:
```bash
$ dotnet run
```

## Known Bugs

* The app lacks necessary error handling

## License
[![License](https://img.shields.io/badge/License-BSD_3--Clause-blue.svg)](https://opensource.org/licenses/BSD-3-Clause)

&copy; _Copyright 2022 Helen Lehrer_