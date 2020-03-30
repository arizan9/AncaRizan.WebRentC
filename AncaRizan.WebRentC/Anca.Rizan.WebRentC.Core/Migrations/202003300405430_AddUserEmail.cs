namespace Anca.Rizan.WebRentC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserEmail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarID = c.Int(nullable: false, identity: true),
                        Plate = c.String(nullable: false, maxLength: 10, unicode: false),
                        Manufacturer = c.String(nullable: false, maxLength: 30, unicode: false),
                        Model = c.String(nullable: false, maxLength: 50, unicode: false),
                        PricePerDay = c.Decimal(nullable: false, storeType: "money"),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        CarID = c.Int(nullable: false),
                        CostumerID = c.Int(nullable: false),
                        ReservStatsID = c.Byte(nullable: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        LocationID = c.Int(nullable: false),
                        CuponID = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Coupons", t => t.CuponID)
                .ForeignKey("dbo.Customers", t => t.CostumerID)
                .ForeignKey("dbo.ReservationStatuses", t => t.ReservStatsID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.Cars", t => t.CarID)
                .Index(t => t.CarID)
                .Index(t => t.CostumerID)
                .Index(t => t.ReservStatsID)
                .Index(t => t.LocationID)
                .Index(t => t.CuponID);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        CuponID = c.Int(nullable: false, identity: true),
                        CouponCode = c.String(nullable: false, maxLength: 10, unicode: false),
                        Description = c.String(nullable: false, storeType: "ntext"),
                        Discount = c.Decimal(nullable: false, precision: 4, scale: 2),
                    })
                .PrimaryKey(t => t.CuponID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CostumerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50, fixedLength: true),
                        LastName = c.String(maxLength: 50, fixedLength: true),
                        BirthDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.CostumerID);
            
            CreateTable(
                "dbo.ReservationStatuses",
                c => new
                    {
                        ReservStatsID = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        Description = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ReservStatsID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10, unicode: false),
                        Description = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.PermissionID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(nullable: false, maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(nullable: false, maxLength: 100, unicode: false),
                        Enabled = c.Boolean(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.RolesPermissions",
                c => new
                    {
                        PermissionID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionID, t.RoleID })
                .ForeignKey("dbo.Permissions", t => t.PermissionID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.PermissionID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolesPermissions", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RolesPermissions", "PermissionID", "dbo.Permissions");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Reservations", "CarID", "dbo.Cars");
            DropForeignKey("dbo.Reservations", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Reservations", "ReservStatsID", "dbo.ReservationStatuses");
            DropForeignKey("dbo.Reservations", "CostumerID", "dbo.Customers");
            DropForeignKey("dbo.Reservations", "CuponID", "dbo.Coupons");
            DropForeignKey("dbo.Cars", "LocationID", "dbo.Locations");
            DropIndex("dbo.RolesPermissions", new[] { "RoleID" });
            DropIndex("dbo.RolesPermissions", new[] { "PermissionID" });
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Reservations", new[] { "CuponID" });
            DropIndex("dbo.Reservations", new[] { "LocationID" });
            DropIndex("dbo.Reservations", new[] { "ReservStatsID" });
            DropIndex("dbo.Reservations", new[] { "CostumerID" });
            DropIndex("dbo.Reservations", new[] { "CarID" });
            DropIndex("dbo.Cars", new[] { "LocationID" });
            DropTable("dbo.RolesPermissions");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.ReservationStatuses");
            DropTable("dbo.Customers");
            DropTable("dbo.Coupons");
            DropTable("dbo.Reservations");
            DropTable("dbo.Locations");
            DropTable("dbo.Cars");
        }
    }
}
