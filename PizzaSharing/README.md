Martin Sirg

MVC code gen
------------
EF Core installida DAL

Microsoft.VisualStudio.Web.CodeGeneration.Design

1. Terminalis installida: dotnet tool install --global dotnet-aspnet-codegenerator
2. Liikuda WebApp kausta
3. dotnet aspnet-codegenerator controller -name CONTROLLER_NAME -actions -m MODEL_NAME -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


API controller
-------------
dotnet aspnet-codegenerator controller -name CONTROLLER_NAME -actions -m MODEL_NAME -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

DB connection string
---------------------
"ConnectionStrings": {
    "DefaultConnection": "server=alpha.akaver.com;database=student2018_179563_AspNetDemo2;user=student2018;password=student2018"
  }

Identity code generation
------------------------

dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f

EF core migration/update
-------------------------
dotnet ef migrations add ProductBugFix --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp

dotnet ef database drop --project DAL.App.EF --startup-project WebApp

Aurelia - Adding new routes/views
---------------------------------
1. Add routes to main-router.ts
2. Add folder to src (i.e prices). then add all view files (index.html, index.ts, ...)
(copy home.ts file, 1) replace logger tag, 2) replace class name(file name in camel case))
//TODO: ei leia router.navigationi üles main-router.html failis    34.29

PIZZA APP CONTROLLER GENERATION
-------------------------------
Pizza App Api controllers generation
------------------------------------

dotnet aspnet-codegenerator controller -name OrganizationsController -actions -m Organization -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Category -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ProductsController -actions -m Product -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ProductInCategoriesController -actions -m ProductInCategory -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name PricesController -actions -m Price -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ChangesController -actions -m Change -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ReceiptsController -actions -m Receipt -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ReceiptRowsController -actions -m ReceiptRow -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ReceiptRowChangesController -actions -m ReceiptRowChange -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ReceiptParticipantsController -actions -m ReceiptParticipant -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name LoansController -actions -m Loan -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name LoanRowsController -actions -m LoanRow -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f


UoW implemented controllers(DONT RUN THESE)
---------------------------
dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Category -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductInCategoriesController -actions -m ProductInCategory -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PricesController -actions -m Price -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


UoW not implemented
-------------------
dotnet aspnet-codegenerator controller -name OrganizationsController -actions -m Organization -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductsController -actions -m Product -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ChangesController -actions -m Change -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReceiptsController -actions -m Receipt -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReceiptRowsController -actions -m ReceiptRow -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReceiptRowChangesController -actions -m ReceiptRowChange -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReceiptParticipantsController -actions -m ReceiptParticipant -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LoansController -actions -m Loan -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LoanRowsController -actions -m LoanRow -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

Aurelia todo:
-------------
noImplicitThis
noImplicitReturns
noUnusedLocals
noUnusedParameters
noImplicitAny
strictNullChecks
strictFunctionTypes
strictPropertyInitialization

tsconfig files kõik panna true


EKSAM:  Layoutis render body!
	Partial vaates ViewData on koopiad, muutused ei esine mujal