namespace TrabajoAutomotriza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAutoFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Autos", "modelo", c => c.String(maxLength: 100));
            AddColumn("dbo.Autos", "ano", c => c.Int());
            AddColumn("dbo.Autos", "propietario", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Autos", "propietario");
            DropColumn("dbo.Autos", "ano");
            DropColumn("dbo.Autos", "modelo");
        }
    }
}
