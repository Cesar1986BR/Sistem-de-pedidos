namespace ABC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelatorioDetalhes",
                c => new
                    {
                        relatorioId = c.Int(nullable: false, identity: true),
                        ArmazemId = c.Int(nullable: false),
                        DepositoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.relatorioId)
                .ForeignKey("dbo.Armazem", t => t.ArmazemId)
                .ForeignKey("dbo.Deposito", t => t.DepositoId)
                .Index(t => t.ArmazemId)
                .Index(t => t.DepositoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RelatorioDetalhes", "DepositoId", "dbo.Deposito");
            DropForeignKey("dbo.RelatorioDetalhes", "ArmazemId", "dbo.Armazem");
            DropIndex("dbo.RelatorioDetalhes", new[] { "DepositoId" });
            DropIndex("dbo.RelatorioDetalhes", new[] { "ArmazemId" });
            DropTable("dbo.RelatorioDetalhes");
        }
    }
}
