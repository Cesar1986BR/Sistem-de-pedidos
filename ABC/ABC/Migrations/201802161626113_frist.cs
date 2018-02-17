namespace ABC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class frist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Armazem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Prazo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Deposito",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArmazemId = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Armazem", t => t.ArmazemId)
                .Index(t => t.ArmazemId);
            
            CreateTable(
                "dbo.Estoque",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepositoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deposito", t => t.DepositoId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.DepositoId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descrição = c.String(maxLength: 500),
                        Imagem = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        PrecoUnidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.ClienteId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Pedido", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Estoque", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Estoque", "DepositoId", "dbo.Deposito");
            DropForeignKey("dbo.Deposito", "ArmazemId", "dbo.Armazem");
            DropIndex("dbo.Pedido", new[] { "ProdutoId" });
            DropIndex("dbo.Pedido", new[] { "ClienteId" });
            DropIndex("dbo.Estoque", new[] { "ProdutoId" });
            DropIndex("dbo.Estoque", new[] { "DepositoId" });
            DropIndex("dbo.Deposito", new[] { "ArmazemId" });
            DropTable("dbo.Cliente");
            DropTable("dbo.Pedido");
            DropTable("dbo.Produto");
            DropTable("dbo.Estoque");
            DropTable("dbo.Deposito");
            DropTable("dbo.Armazem");
        }
    }
}
