# ETL Steps

## Package execution order
1. Execute `00_PrepareDW.dtsx` to create the DW schema and procedures.
2. Execute `01_LoadDimensions.dtsx` to load all dimensions.
3. Execute `02_LoadFactSalesOrderLine.dtsx` to load the fact table.
4. Execute `99_Reconciliation.dtsx` to validate counts and totals.

## Core dimensions (01_LoadDimensions.dtsx)
The core dimensions are loaded from AdventureWorks2019:

- **DimProduct** from `Production.Product`.
- **DimCustomer** from `Sales.Customer` joined to `Person.Person` to capture customer names.
- **DimTerritory** from `Sales.SalesTerritory`.
- **DimSalesPerson** from `Sales.SalesPerson`.
- **DimShipMethod** from `Purchasing.ShipMethod`.

Each dimension data flow uses a lookup to detect existing keys, updates matches with an OLE DB Command, and inserts new rows via an OLE DB Destination.

## Fact load (02_LoadFactSalesOrderLine.dtsx)
The fact load uses the most granular grain (1 row per sales order line):

- **Source:** `Sales.SalesOrderDetail` joined with `Sales.SalesOrderHeader`.
- **Grain:** 1 row per sales order line (one product line inside one order).
- **Main measures:** `OrderQty`, `UnitPrice`, `UnitPriceDiscount`, `LineTotal`.
- **Reason:** This is the most detailed level and enables flexible aggregation (daily/monthly totals, by product, by customer, etc.).

The data flow follows an upsert pattern:

1. **OLE DB Source (AdventureWorks2019)**
   - Reads joined order header/detail rows.
2. **Lookup (existence check)**
   - Looks up `SalesOrderLineKey` in `dbo.FactSalesOrderLine`.
3. **OLE DB Command (update)**
   - Updates the existing row when the lookup finds a match.
4. **OLE DB Destination (insert)**
   - Inserts the row when the lookup finds no match.

## How to run the SSIS packages

### Option A: Visual Studio (SSDT)
1. Open `etl_ssis/AdventureWorksDW_Sales_ETL.sln`.
2. Update connection managers in each package to match your SQL Server instance.
3. Right-click each package and choose **Execute Package** in the order listed above.

### Option B: DTExec (command line)
Use DTExec to execute each package from the command line (replace connection strings and paths as needed):

```bash
"C:\Program Files\Microsoft SQL Server\160\DTS\Binn\DTExec.exe" /F "<repo>\etl_ssis\Packages\00_PrepareDW.dtsx" /CONNECTION "CN_AdventureWorksDW_Sales";"Data Source=.;Initial Catalog=AdventureWorksDW_Sales;Provider=MSOLEDBSQL;Integrated Security=SSPI;"
"C:\Program Files\Microsoft SQL Server\160\DTS\Binn\DTExec.exe" /F "<repo>\etl_ssis\Packages\01_LoadDimensions.dtsx" /CONNECTION "CN_AdventureWorksDW_Sales";"Data Source=.;Initial Catalog=AdventureWorksDW_Sales;Provider=MSOLEDBSQL;Integrated Security=SSPI;" /CONNECTION "CN_AdventureWorks2019";"Data Source=.;Initial Catalog=AdventureWorks2019;Provider=MSOLEDBSQL;Integrated Security=SSPI;"
"C:\Program Files\Microsoft SQL Server\160\DTS\Binn\DTExec.exe" /F "<repo>\etl_ssis\Packages\02_LoadFactSalesOrderLine.dtsx" /CONNECTION "CN_AdventureWorksDW_Sales";"Data Source=.;Initial Catalog=AdventureWorksDW_Sales;Provider=MSOLEDBSQL;Integrated Security=SSPI;" /CONNECTION "CN_AdventureWorks2019";"Data Source=.;Initial Catalog=AdventureWorks2019;Provider=MSOLEDBSQL;Integrated Security=SSPI;"
"C:\Program Files\Microsoft SQL Server\160\DTS\Binn\DTExec.exe" /F "<repo>\etl_ssis\Packages\99_Reconciliation.dtsx" /CONNECTION "CN_AdventureWorksDW_Sales";"Data Source=.;Initial Catalog=AdventureWorksDW_Sales;Provider=MSOLEDBSQL;Integrated Security=SSPI;"
```

### Option C: SQL Server Agent
1. Deploy the SSIS project to the SSIS catalog (SSISDB).
2. Create a SQL Server Agent job with four steps (one per package).
3. Schedule the job for recurring loads.
