# About

Directory Layout
```
SportsPro
  - wwwroot
  - Controllers
  - docs
  - Migrations
  - Models
    - Data Layer
      - Configurations
    - ViewModels
  - Repositories
  - Views
  - Program.cs

SportsPro Tests
  - Controllers
  - Static
```

## Controllers
Most of the app controllers will be stored here. MVC controllers are meant to be lean, meaning they're purpose is to only pass data between the model and the view. You will see the following techniques in the controllers:
```
- Passing in a DB Context
- Constructors
- List, Add, Edit, Save, Delete methods
- GET, POST, DELETE REST API
- Routes
- ViewBags
- QueryOptions, Repository, and IRepository classes
```
## Models
Models stores the models, the Data Layer, and the ViewModels. The more robust and decoupled you can make a model classes, the better. Fat bottom models make the rockin' world go round!
```
- Check
- Country
- Customer
- Incident
- Product
- Registration
- Technician
```
## Data Layer
This where the Configurations, IRepository class, QueryOptions class, Repository class, and SportsPro Context aka DB Context lives.
```
- Configurations is responsible for seeding the database
- IRepository is the intializer of a repository pattern
- Query Options is a reusable querying logic for frequent queries, such as WHERE, ORDER BY, and INCLUDES. They are like a main query class to avoid repetitive code.
- SportsPro Context is the lifeline of the app in that it connects the data and code to perform CRUD operations
```
## ViewModels
ViewModels are joined tables that are shaped into classes. This helps help 'least privaleged' access to enhance security and organization of data.
```
Technician + Incident = The Technician assigned to an Incident
```
## Repositories
## Views
Where all the page views are stores. Each page has it's own view:
```
- List or Index for displaying data upon page load
- AddEdit for adding or editing data
- Delete for removing data
```
## Program.cs
Most of the ASP.NET setup and configuration goodies live here.
```
- AddTransient
- AddHttpContextAccessor
- Page Routing
- Memory Cache
- Sessions
- ControllerWithViews
- DBContext config
- Redirects
- Authorization
```
