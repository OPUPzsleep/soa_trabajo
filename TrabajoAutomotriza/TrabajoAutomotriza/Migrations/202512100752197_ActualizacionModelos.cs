namespace TrabajoAutomotriza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionModelos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autos",
                c => new
                    {
                        idAuto = c.Int(nullable: false, identity: true),
                        placa = c.String(nullable: false, maxLength: 50),
                        marca = c.String(nullable: false, maxLength: 100),
                        modelo = c.String(nullable: false, maxLength: 100),
                        ano = c.Int(),
                        propietario = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.idAuto);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        idCliente = c.Int(nullable: false, identity: true),
                        nombres = c.String(nullable: false, maxLength: 100),
                        apellidos = c.String(nullable: false, maxLength: 100),
                        telefono = c.String(maxLength: 20),
                        idAuto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCliente)
                .ForeignKey("dbo.Autos", t => t.idAuto, cascadeDelete: true)
                .Index(t => t.idAuto);
            
            CreateTable(
                "dbo.OrdenMantenimiento",
                c => new
                    {
                        idOrden = c.Int(nullable: false, identity: true),
                        idAuto = c.Int(nullable: false),
                        idCliente = c.Int(nullable: false),
                        fechaOrden = c.DateTime(nullable: false),
                        fechaFinalizacion = c.DateTime(),
                        descripcionProblema = c.String(maxLength: 500),
                        estado = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.idOrden)
                .ForeignKey("dbo.Autos", t => t.idAuto, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.idCliente, cascadeDelete: false)
                .Index(t => t.idAuto)
                .Index(t => t.idCliente);
            
            CreateTable(
                "dbo.Presupuesto",
                c => new
                    {
                        idPresupuesto = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        fechaPresupuesto = c.DateTime(nullable: false),
                        montoEstimado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        estado = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.idPresupuesto)
                .ForeignKey("dbo.OrdenMantenimiento", t => t.idOrden, cascadeDelete: true)
                .Index(t => t.idOrden);
            
            CreateTable(
                "dbo.DetallePresupuesto",
                c => new
                    {
                        idDetalle = c.Int(nullable: false, identity: true),
                        idPresupuesto = c.Int(nullable: false),
                        concepto = c.String(maxLength: 200),
                        costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idDetalle)
                .ForeignKey("dbo.Presupuesto", t => t.idPresupuesto, cascadeDelete: true)
                .Index(t => t.idPresupuesto);
            
            CreateTable(
                "dbo.ServicioAsignado",
                c => new
                    {
                        idServicio = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        idMecanico = c.Int(nullable: false),
                        descripcionServicio = c.String(maxLength: 200),
                        costoServicio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idServicio)
                .ForeignKey("dbo.Mecanicos", t => t.idMecanico, cascadeDelete: true)
                .ForeignKey("dbo.OrdenMantenimiento", t => t.idOrden, cascadeDelete: true)
                .Index(t => t.idOrden)
                .Index(t => t.idMecanico);
            
            CreateTable(
                "dbo.Mecanicos",
                c => new
                    {
                        idMecanico = c.Int(nullable: false, identity: true),
                        nombres = c.String(nullable: false, maxLength: 100),
                        apellidos = c.String(nullable: false, maxLength: 100),
                        telefono = c.String(maxLength: 20),
                        especialidad = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.idMecanico);
            
            CreateTable(
                "dbo.ServicioRealizado",
                c => new
                    {
                        idServicio = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        descripcionServicio = c.String(maxLength: 200),
                        costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idServicio)
                .ForeignKey("dbo.OrdenMantenimiento", t => t.idOrden, cascadeDelete: true)
                .Index(t => t.idOrden);
            
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        idFactura = c.Int(nullable: false, identity: true),
                        idPresupuesto = c.Int(nullable: false),
                        fechaFactura = c.DateTime(nullable: false),
                        totalFactura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        estado = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.idFactura)
                .ForeignKey("dbo.Presupuesto", t => t.idPresupuesto, cascadeDelete: true)
                .Index(t => t.idPresupuesto);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        idPago = c.Int(nullable: false, identity: true),
                        idFactura = c.Int(nullable: false),
                        fechaPago = c.DateTime(nullable: false),
                        montoPagado = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idPago)
                .ForeignKey("dbo.Factura", t => t.idFactura, cascadeDelete: true)
                .Index(t => t.idFactura);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pago", "idFactura", "dbo.Factura");
            DropForeignKey("dbo.Factura", "idPresupuesto", "dbo.Presupuesto");
            DropForeignKey("dbo.ServicioRealizado", "idOrden", "dbo.OrdenMantenimiento");
            DropForeignKey("dbo.ServicioAsignado", "idOrden", "dbo.OrdenMantenimiento");
            DropForeignKey("dbo.ServicioAsignado", "idMecanico", "dbo.Mecanicos");
            DropForeignKey("dbo.Presupuesto", "idOrden", "dbo.OrdenMantenimiento");
            DropForeignKey("dbo.DetallePresupuesto", "idPresupuesto", "dbo.Presupuesto");
            DropForeignKey("dbo.OrdenMantenimiento", "idCliente", "dbo.Clientes");
            DropForeignKey("dbo.OrdenMantenimiento", "idAuto", "dbo.Autos");
            DropForeignKey("dbo.Clientes", "idAuto", "dbo.Autos");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Pago", new[] { "idFactura" });
            DropIndex("dbo.Factura", new[] { "idPresupuesto" });
            DropIndex("dbo.ServicioRealizado", new[] { "idOrden" });
            DropIndex("dbo.ServicioAsignado", new[] { "idMecanico" });
            DropIndex("dbo.ServicioAsignado", new[] { "idOrden" });
            DropIndex("dbo.DetallePresupuesto", new[] { "idPresupuesto" });
            DropIndex("dbo.Presupuesto", new[] { "idOrden" });
            DropIndex("dbo.OrdenMantenimiento", new[] { "idCliente" });
            DropIndex("dbo.OrdenMantenimiento", new[] { "idAuto" });
            DropIndex("dbo.Clientes", new[] { "idAuto" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Pago");
            DropTable("dbo.Factura");
            DropTable("dbo.ServicioRealizado");
            DropTable("dbo.Mecanicos");
            DropTable("dbo.ServicioAsignado");
            DropTable("dbo.DetallePresupuesto");
            DropTable("dbo.Presupuesto");
            DropTable("dbo.OrdenMantenimiento");
            DropTable("dbo.Clientes");
            DropTable("dbo.Autos");
        }
    }
}
