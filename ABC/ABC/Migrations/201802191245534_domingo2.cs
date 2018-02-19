namespace ABC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class domingo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RelatorioDetalhes", "valorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RelatorioDetalhes", "Nome", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RelatorioDetalhes", "Nome");
            DropColumn("dbo.RelatorioDetalhes", "valorTotal");
        }
    }
}
