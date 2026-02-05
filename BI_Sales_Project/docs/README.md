# BI Sales Project

## Prerequisites
- Windows 11 / Windows Server
- Visual Studio 2022
- SQL Server 2022 (local instance `.`)
- SQL Server Data Tools (SSDT) + SSIS
- .NET 8 SDK
- Node.js 18+ and Angular CLI 17+

## Setup Databases
### OLTP (OltpEcommerceDb)
1. Open `backend_api/BI.Sales.sln` in Visual Studio.
2. Update `appsettings.json` connection string if needed.
3. Run EF migrations (create as needed):
   ```bash
   dotnet ef database update --project backend_api/BI.Sales.Api/src/BI.Sales.Api
   ```

### Data Warehouse (AdventureWorksDW_Sales)
1. Ensure `AdventureWorks2019` is installed on the same SQL Server instance.
2. Run the scripts in order:
   - `database/dw_sales/00_Create_AdventureWorksDW_Sales.sql`
   - `database/dw_sales/01_DW_Tables.sql`
   - `database/dw_sales/02_DW_LoadProcs_Merge.sql`
   - `database/dw_sales/03_DW_AnalyticsViews.sql`
   - `database/dw_sales/05_ETL_Audit.sql`

## Execute SSIS ETL
1. Open `etl_ssis/AdventureWorksDW_Sales_ETL.sln`.
2. Update connection managers per `etl_ssis/Config/README_Connections.txt`.
3. Execute packages in order:
   1. `00_PrepareDW.dtsx`
   2. `01_LoadDimensions.dtsx`
   3. `02_LoadFactSalesOrderLine.dtsx`
   4. `99_Reconciliation.dtsx`

## Run Backend API
1. Start the API:
   ```bash
   dotnet run --project backend_api/BI.Sales.Api/src/BI.Sales.Api
   ```
2. Seed users and roles:
   ```bash
   POST /api/auth/seed
   ```
3. Test Swagger at `https://localhost:5001/swagger`.

## Run Angular Frontend
1. Install dependencies:
   ```bash
   cd frontend_angular
   npm install
   ```
2. Start the UI:
   ```bash
   npm start
   ```

## Validation Checklist
- CRUD Categories/Subcategories/Products (Admin)
- Cart add/update/remove + checkout
- Orders listing and details
- Dashboard KPI and charts
