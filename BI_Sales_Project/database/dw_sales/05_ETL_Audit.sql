USE AdventureWorksDW_Sales;
GO

CREATE TABLE dbo.ETL_Audit (
    AuditId bigint IDENTITY(1,1) PRIMARY KEY,
    PackageName nvarchar(200) NOT NULL,
    StartTime datetime2 NOT NULL,
    EndTime datetime2 NULL,
    RowsInserted int NULL,
    RowsUpdated int NULL,
    Status nvarchar(20) NOT NULL,
    ErrorMessage nvarchar(max) NULL
);
