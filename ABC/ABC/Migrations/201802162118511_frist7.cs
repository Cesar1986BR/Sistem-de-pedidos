namespace ABC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class frist7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedido", "Quantidade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "Quantidade", c => c.String());
        }
    }
}
