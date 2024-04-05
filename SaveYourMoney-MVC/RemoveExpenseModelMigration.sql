BEGIN TRANSACTION;

CREATE TABLE "Expenses" (
    "ExpenseId" INTEGER NOT NULL CONSTRAINT "PK_Expenses" PRIMARY KEY AUTOINCREMENT,
    "ExpenseTitle" TEXT NULL,
    "Type" INTEGER NOT NULL,
    "CustomerId" INTEGER NOT NULL,
    "CategoryId" TEXT NULL,
    "Amount" REAL NOT NULL,
    "Description" TEXT NULL,
    "Date" TEXT NOT NULL,
    "AttachmentData" BLOB NULL,
    "AttachmentFileName" TEXT NULL,
    CONSTRAINT "FK_Expenses_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("CustomerId") ON DELETE CASCADE,
    CONSTRAINT "FK_Expenses_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("CategoryId") ON DELETE RESTRICT
);

CREATE INDEX "IX_Expenses_CategoryId" ON "Expenses" ("CategoryId");

CREATE INDEX "IX_Expenses_CustomerId" ON "Expenses" ("CustomerId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240330174130_RecreateExpenseTable', '6.0.26');

COMMIT;

