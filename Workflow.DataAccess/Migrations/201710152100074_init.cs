namespace Workflow.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 20),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "AK_Category_Name");

            CreateTable(
                "dbo.InventoryItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 15),
                    Name = c.String(nullable: false, maxLength: 80),
                    UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.Code, unique: true, name: "AK_InventoryItem_Code")
                .Index(t => t.Name, unique: true, name: "AK_InventoryItem_Name")
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.Customers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AccountNumber = c.String(nullable: false, maxLength: 8),
                    CompanyName = c.String(nullable: false, maxLength: 30),
                    Address = c.String(nullable: false, maxLength: 30),
                    City = c.String(nullable: false, maxLength: 15),
                    State = c.String(nullable: false, maxLength: 20),
                    ZipCode = c.String(nullable: false, maxLength: 10),
                    Phone = c.String(maxLength: 15),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AccountNumber, unique: true, name: "AK_Customer_AccountNumber")
                .Index(t => t.CompanyName, unique: true, name: "AK_Customer_CompanyName");

            CreateTable(
                "dbo.Labors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    WorkOrderId = c.Int(nullable: false),
                    ServiceItemCode = c.String(nullable: false, maxLength: 15),
                    ServiceItemName = c.String(nullable: false, maxLength: 80),
                    LaborHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    //ComputedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Notes = c.String(maxLength: 140),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrderId, cascadeDelete: true)
                .Index(t => new { t.WorkOrderId, t.ServiceItemCode }, unique: true, name: "AK_Labor");

            Sql("ALTER TABLE dbo.Labors ADD ComputedPrice AS CAST(LaborHours * Rate AS Decimal(18,2))");

            CreateTable(
                "dbo.WorkOrders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CustomerId = c.Int(nullable: false),
                    OrderDateTime = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    TargetDateTime = c.DateTime(),
                    DropDeadDateTime = c.DateTime(),
                    Description = c.String(maxLength: 256),
                    WorkOrderStatus = c.Int(nullable: false),
                    //Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CertificationRequirements = c.String(maxLength: 120),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);

            Sql(@"CREATE FUNCTION dbo.GetSumOfPartsAndLabor(@workOrderId INT)
                RETURNS DECIMAL(18, 2)
                AS
                BEGIN

                DECLARE @partsSum Decimal(18, 2);
                DECLARE @laborSum Decimal(18, 2);

                SELECT @partsSum = Sum(ComputedPrice)
                FROM Parts
                WHERE WorkOrderId = @workOrderId;

                SELECT @laborSum = Sum(ComputedPrice)
                FROM Labors
                WHERE WorkOrderId = @workOrderId;

                RETURN ISNULL(@partsSum, 0) + ISNULL(@laborSum, 0);
                END");

            Sql("ALTER TABLE dbo.WorkOrders ADD Total AS dbo.GetSumOfPartsAndLabor(Id)");

            CreateTable(
                "dbo.Parts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    WorkOrderId = c.Int(nullable: false),
                    InventoryItemCode = c.String(nullable: false, maxLength: 15),
                    InventoryItemName = c.String(nullable: false, maxLength: 80),
                    Quantity = c.Int(nullable: false),
                    UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    //ComputedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Notes = c.String(maxLength: 140),
                    IsInstalled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrderId, cascadeDelete: true)
                .Index(t => new { t.WorkOrderId, t.InventoryItemCode }, unique: true, name: "AK_Part");

            Sql("ALTER TABLE dbo.Parts ADD ComputedPrice AS CAST(Quantity * UnitPrice AS Decimal(18,2))");

            CreateTable(
                "dbo.ServiceItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 15),
                    Name = c.String(nullable: false, maxLength: 80),
                    Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true, name: "AK_ServiceItem_Code")
                .Index(t => t.Name, unique: true, name: "AK_ServiceItem_Name");

        }

        public override void Down()
        {
            DropForeignKey("dbo.Parts", "WorkOrderId", "dbo.WorkOrders");
            DropForeignKey("dbo.Labors", "WorkOrderId", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.InventoryItems", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ServiceItems", "AK_ServiceItem_Name");
            DropIndex("dbo.ServiceItems", "AK_ServiceItem_Code");
            DropIndex("dbo.Parts", "AK_Part");
            DropIndex("dbo.WorkOrders", new[] { "CustomerId" });
            DropIndex("dbo.Labors", "AK_Labor");
            DropIndex("dbo.Customers", "AK_Customer_CompanyName");
            DropIndex("dbo.Customers", "AK_Customer_AccountNumber");
            DropIndex("dbo.InventoryItems", new[] { "CategoryId" });
            DropIndex("dbo.InventoryItems", "AK_InventoryItem_Name");
            DropIndex("dbo.InventoryItems", "AK_InventoryItem_Code");
            DropIndex("dbo.Categories", "AK_Category_Name");
            DropTable("dbo.ServiceItems");
            DropTable("dbo.Parts");
            DropTable("dbo.WorkOrders");
            DropTable("dbo.Labors");
            DropTable("dbo.Customers");
            DropTable("dbo.InventoryItems");
            DropTable("dbo.Categories");
        }
    }
}
