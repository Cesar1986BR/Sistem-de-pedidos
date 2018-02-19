namespace ABC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class domingo1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RelatorioDetalhes", "Cliente_Id", c => c.Int());
            AddColumn("dbo.RelatorioDetalhes", "Pedido_Id", c => c.Int());
            AddColumn("dbo.RelatorioDetalhes", "Produto_Id", c => c.Int());
            AddColumn("dbo.RelatorioDetalhes", "Estoque_Id", c => c.Int());
            CreateIndex("dbo.RelatorioDetalhes", "Cliente_Id");
            CreateIndex("dbo.RelatorioDetalhes", "Pedido_Id");
            CreateIndex("dbo.RelatorioDetalhes", "Produto_Id");
            CreateIndex("dbo.RelatorioDetalhes", "Estoque_Id");
            AddForeignKey("dbo.RelatorioDetalhes", "Cliente_Id", "dbo.Cliente", "Id");
            AddForeignKey("dbo.RelatorioDetalhes", "Pedido_Id", "dbo.Pedido", "Id");
            AddForeignKey("dbo.RelatorioDetalhes", "Produto_Id", "dbo.Produto", "Id");
            AddForeignKey("dbo.RelatorioDetalhes", "Estoque_Id", "dbo.Estoque", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RelatorioDetalhes", "Estoque_Id", "dbo.Estoque");
            DropForeignKey("dbo.RelatorioDetalhes", "Produto_Id", "dbo.Produto");
            DropForeignKey("dbo.RelatorioDetalhes", "Pedido_Id", "dbo.Pedido");
            DropForeignKey("dbo.RelatorioDetalhes", "Cliente_Id", "dbo.Cliente");
            DropIndex("dbo.RelatorioDetalhes", new[] { "Estoque_Id" });
            DropIndex("dbo.RelatorioDetalhes", new[] { "Produto_Id" });
            DropIndex("dbo.RelatorioDetalhes", new[] { "Pedido_Id" });
            DropIndex("dbo.RelatorioDetalhes", new[] { "Cliente_Id" });
            DropColumn("dbo.RelatorioDetalhes", "Estoque_Id");
            DropColumn("dbo.RelatorioDetalhes", "Produto_Id");
            DropColumn("dbo.RelatorioDetalhes", "Pedido_Id");
            DropColumn("dbo.RelatorioDetalhes", "Cliente_Id");
        }
    }
}
