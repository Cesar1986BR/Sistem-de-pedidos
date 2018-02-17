namespace ABC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class frist1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "Preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "Preco");
        }
    }
}
