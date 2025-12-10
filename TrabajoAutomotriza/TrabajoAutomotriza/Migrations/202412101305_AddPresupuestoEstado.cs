namespace TrabajoAutomotriza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPresupuestoEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presupuesto", "estado", c => c.String(maxLength: 50));
            Sql("UPDATE [dbo].[Presupuesto] SET [estado] = 'Pendiente' WHERE [estado] IS NULL");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presupuesto", "estado");
        }
    }
}
