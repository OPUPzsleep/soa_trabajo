namespace TrabajoAutomotriza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrdenEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdenMantenimiento", "fechaFinalizacion", c => c.DateTime());
            AddColumn("dbo.OrdenMantenimiento", "estado", c => c.String(maxLength: 50));
            Sql("UPDATE [dbo].[OrdenMantenimiento] SET [estado] = 'Pendiente' WHERE [estado] IS NULL");
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdenMantenimiento", "estado");
            DropColumn("dbo.OrdenMantenimiento", "fechaFinalizacion");
        }
    }
}
