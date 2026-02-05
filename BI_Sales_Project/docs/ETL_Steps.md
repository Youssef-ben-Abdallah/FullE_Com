# ETL Steps

1. Execute `00_PrepareDW.dtsx` to create the DW schema and procedures.
2. Execute `01_LoadDimensions.dtsx` to load all dimensions.
3. Execute `02_LoadFactSalesOrderLine.dtsx` to load the fact table.
4. Execute `99_Reconciliation.dtsx` to validate counts and totals.
