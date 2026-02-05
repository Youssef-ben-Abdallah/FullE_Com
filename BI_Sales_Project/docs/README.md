# BI Sales Project

## Prerequisites
- Windows + Visual Studio 2022
- SQL Server 2022 (server=.)
- .NET 8 SDK
- Node.js 18+ and Angular CLI 17+
- SSIS (SQL Server Data Tools)

## Setup OLTP Database
1. Open the backend solution.
2. Apply migrations to create `OltpEcommerceDb`.
3. Run the seed endpoint in development:
   - `POST /api/auth/seed`

## Setup Data Warehouse
1. Execute scripts in order from `/database/dw_sales`:
   - `00_Create_AdventureWorksDW_Sales.sql`
   - `01_DW_Tables.sql`
   - `02_DW_LoadProcs_Merge.sql`
   - `03_DW_AnalyticsViews.sql`
   - `05_ETL_Audit.sql`

## Execute SSIS ETL
Run packages in order:
1. `00_PrepareDW.dtsx`
2. `01_LoadDimensions.dtsx`
3. `02_LoadFactSalesOrderLine.dtsx`
4. `99_Reconciliation.dtsx`

## Run API
1. Update connection strings in `appsettings.json`.
2. Start the API.
3. Browse Swagger at `/swagger` and authenticate with JWT.

## Run Angular
1. Install dependencies: `npm install`
2. Run: `ng serve`
3. Log in with:
   - admin@local / P@ssw0rd!
   - user@local / P@ssw0rd!

## Validation Checklist
- CRUD categories, subcategories, products.
- Cart add/update/remove and checkout.
- Orders list and details.
- Dashboard charts load from DW analytics endpoints.
