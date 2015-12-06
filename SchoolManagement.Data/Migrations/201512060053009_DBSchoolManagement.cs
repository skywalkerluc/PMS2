namespace SchoolManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBSchoolManagement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Aluno", "Etnia", c => c.Int());
            AlterColumn("dbo.Usuario", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Foto", c => c.Byte());
            AlterColumn("dbo.Usuario", "Endereco_Numero", c => c.Int());
            AlterColumn("dbo.Usuario", "UserLogin", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.LojasRecomendadas", "Endereco_Numero", c => c.Int());
            AlterColumn("dbo.Notificacao", "Assunto", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("dbo.Notificacao", "Descricao", c => c.String(maxLength: 1000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notificacao", "Descricao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Notificacao", "Assunto", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.LojasRecomendadas", "Endereco_Numero", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "UserLogin", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Endereco_Numero", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuario", "Foto", c => c.Byte(nullable: false));
            AlterColumn("dbo.Usuario", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Aluno", "Etnia", c => c.Int(nullable: false));
        }
    }
}
