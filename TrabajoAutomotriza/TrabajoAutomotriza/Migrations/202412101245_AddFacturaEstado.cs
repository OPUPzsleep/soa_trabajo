namespace TrabajoAutomotriza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFacturaEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Factura", "estado", c => c.String(maxLength: 50));
            Sql("UPDATE [dbo].[Factura] SET [estado] = 'Emitida' WHERE [estado] IS NULL");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Factura", "estado");
        }
    }
}
