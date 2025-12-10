namespace TrabajoAutomotriza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        descripcionProblema = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.idOrden)
                .ForeignKey("dbo.Autos", t => t.idAuto, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.idCliente, cascadeDelete: true)
                .Index(t => t.idAuto)
                .Index(t => t.idCliente);
            
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
                "dbo.ServicioAsignado",
                c => new
                    {
                        idServicio = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        idMecanico = c.Int(nullable: false),
                        descripcionServicio = c.String(maxLength: 200),
                        costoServicio = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.idServicio)
                .ForeignKey("dbo.OrdenMantenimiento", t => t.idOrden, cascadeDelete: true)
                .ForeignKey("dbo.Mecanicos", t => t.idMecanico, cascadeDelete: true)
                .Index(t => t.idOrden)
                .Index(t => t.idMecanico);
            
            CreateTable(
                "dbo.Presupuesto",
                c => new
                    {
                        idPresupuesto = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        fechaPresupuesto = c.DateTime(nullable: false),
                        montoEstimado = c.Decimal(nullable: false, precision: 10, scale: 2),
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
                        costo = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.idDetalle)
                .ForeignKey("dbo.Presupuesto", t => t.idPresupuesto, cascadeDelete: true)
                .Index(t => t.idPresupuesto);
            
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        idFactura = c.Int(nullable: false, identity: true),
                        idPresupuesto = c.Int(nullable: false),
                        fechaFactura = c.DateTime(nullable: false),
                        totalFactura = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.idFactura)
                .ForeignKey("dbo.Presupuesto", t => t.idPresupuesto, cascadeDelete: true)
                .Index(t => t.idPresupuesto);
            
            CreateTable(
                "dbo.ServicioRealizado",
                c => new
                    {
                        idServicio = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        descripcionServicio = c.String(maxLength: 200),
                        costo = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.idServicio)
                .ForeignKey("dbo.OrdenMantenimiento", t => t.idOrden, cascadeDelete: true)
                .Index(t => t.idOrden);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        idPago = c.Int(nullable: false, identity: true),
                        idFactura = c.Int(nullable: false),
                        fechaPago = c.DateTime(nullable: false),
                        montoPagado = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.idPago)
                .ForeignKey("dbo.Factura", t => t.idFactura, cascadeDelete: true)
                .Index(t => t.idFactura);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pago", "idFactura", "dbo.Factura");
            DropForeignKey("dbo.ServicioRealizado", "idOrden", "dbo.OrdenMantenimiento");
            DropForeignKey("dbo.Factura", "idPresupuesto", "dbo.Presupuesto");
            DropForeignKey("dbo.DetallePresupuesto", "idPresupuesto", "dbo.Presupuesto");
            DropForeignKey("dbo.Presupuesto", "idOrden", "dbo.OrdenMantenimiento");
            DropForeignKey("dbo.ServicioAsignado", "idMecanico", "dbo.Mecanicos");
            DropForeignKey("dbo.ServicioAsignado", "idOrden", "dbo.OrdenMantenimiento");
            DropForeignKey("dbo.OrdenMantenimiento", "idCliente", "dbo.Clientes");
            DropForeignKey("dbo.OrdenMantenimiento", "idAuto", "dbo.Autos");
            DropForeignKey("dbo.Clientes", "idAuto", "dbo.Autos");
            DropIndex("dbo.Pago", new[] { "idFactura" });
            DropIndex("dbo.ServicioRealizado", new[] { "idOrden" });
            DropIndex("dbo.Factura", new[] { "idPresupuesto" });
            DropIndex("dbo.DetallePresupuesto", new[] { "idPresupuesto" });
            DropIndex("dbo.Presupuesto", new[] { "idOrden" });
            DropIndex("dbo.ServicioAsignado", new[] { "idMecanico" });
            DropIndex("dbo.ServicioAsignado", new[] { "idOrden" });
            DropIndex("dbo.OrdenMantenimiento", new[] { "idCliente" });
            DropIndex("dbo.OrdenMantenimiento", new[] { "idAuto" });
            DropIndex("dbo.Clientes", new[] { "idAuto" });
            DropTable("dbo.Pago");
            DropTable("dbo.ServicioRealizado");
            DropTable("dbo.Factura");
            DropTable("dbo.DetallePresupuesto");
            DropTable("dbo.Presupuesto");
            DropTable("dbo.ServicioAsignado");
            DropTable("dbo.Mecanicos");
            DropTable("dbo.OrdenMantenimiento");
            DropTable("dbo.Clientes");
            DropTable("dbo.Autos");
        }
    }
}
