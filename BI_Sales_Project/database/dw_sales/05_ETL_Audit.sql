USE AdventureWorksDW_Sales;
GO

CREATE TABLE dbo.ETL_Audit (
    AuditId BIGINT IDENTITY PRIMARY KEY,
    PackageName NVARCHAR(200) NOT NULL,
    StartTime DATETIME2 NOT NULL,
    EndTime DATETIME2 NULL,
    RowsInserted INT NOT NULL DEFAULT(0),
    RowsUpdated INT NOT NULL DEFAULT(0),
    Status NVARCHAR(20) NOT NULL,
    ErrorMessage NVARCHAR(MAX) NULL
);
