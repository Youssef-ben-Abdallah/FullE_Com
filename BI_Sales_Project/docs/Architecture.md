# Architecture

## Overview
- Angular 17+ frontend for e-commerce + analytics dashboard
- ASP.NET Core Web API (.NET 8) with JWT authentication
- OLTP database: `OltpEcommerceDb` (EF Core Code First)
- Data Warehouse: `AdventureWorksDW_Sales` populated via SSIS

## Contexts
1. **OltpEcommerceDbContext**
   - Identity tables
   - Catalog, cart, order entities
2. **DwSalesDbContext**
   - Read-only view mappings
   - Analytics endpoints only

## Data Flow
- OLTP is isolated from DW (no FK or direct dependency)
- SSIS pulls from `AdventureWorks2019` and populates the DW
- API reads from DW views for dashboard KPIs and trends
