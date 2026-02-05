# Architecture

## Components
- Angular frontend with modular feature areas (auth, catalog, admin, cart, orders, analytics).
- ASP.NET Core Web API with Identity, JWT authentication, and repository/service layers.
- OLTP database `OltpEcommerceDb` for transactional CRUD.
- Data warehouse `AdventureWorksDW_Sales` for analytics only.
- SSIS packages orchestrating stored procedure-driven ETL.

## Data Separation
The OLTP database never references the data warehouse. Analytics are served from DW views through a read-only context.
