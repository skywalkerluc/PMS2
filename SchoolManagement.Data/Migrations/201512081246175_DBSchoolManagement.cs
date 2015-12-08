namespace SchoolManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBSchoolManagement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Professor", "Turma_TurmaId", "dbo.Turma");
            DropIndex("dbo.Professor", new[] { "Turma_TurmaId" });
            CreateTable(
                "dbo.ProfessorTurma",
                c => new
                    {
                        Professor_Id = c.Int(nullable: false),
                        Turma_TurmaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_Id, t.Turma_TurmaId })
                .ForeignKey("dbo.Professor", t => t.Professor_Id)
                .ForeignKey("dbo.Turma", t => t.Turma_TurmaId)
                .Index(t => t.Professor_Id)
                .Index(t => t.Turma_TurmaId);
            
            AddColumn("dbo.Livro", "Turma_TurmaId", c => c.Int());
            AddColumn("dbo.TrabalhosExtras", "TurmaSelecionada_TurmaId", c => c.Int());
            AlterColumn("dbo.Aluno", "Etnia", c => c.Int());
            AlterColumn("dbo.Usuario", "Rg", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Cpf", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Foto", c => c.Byte());
            AlterColumn("dbo.Usuario", "Endereco_Numero", c => c.Int());
            AlterColumn("dbo.Usuario", "Contato_Celular", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "UserLogin", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.LojasRecomendadas", "Endereco_Numero", c => c.Int());
            AlterColumn("dbo.LojasRecomendadas", "Contato_Celular", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Notificacao", "Assunto", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("dbo.Notificacao", "Descricao", c => c.String(maxLength: 1000, unicode: false));
            CreateIndex("dbo.Livro", "Turma_TurmaId");
            CreateIndex("dbo.TrabalhosExtras", "TurmaSelecionada_TurmaId");
            AddForeignKey("dbo.Livro", "Turma_TurmaId", "dbo.Turma", "TurmaId");
            AddForeignKey("dbo.TrabalhosExtras", "TurmaSelecionada_TurmaId", "dbo.Turma", "TurmaId");
            DropColumn("dbo.Aluno", "NumeroMatricula");
            DropColumn("dbo.Professor", "Turma_TurmaId");
            DropColumn("dbo.Professor", "Matricula");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Professor", "Matricula", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Professor", "Turma_TurmaId", c => c.Int());
            AddColumn("dbo.Aluno", "NumeroMatricula", c => c.String(maxLength: 100, unicode: false));
            DropForeignKey("dbo.TrabalhosExtras", "TurmaSelecionada_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.Livro", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.ProfessorTurma", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.ProfessorTurma", "Professor_Id", "dbo.Professor");
            DropIndex("dbo.ProfessorTurma", new[] { "Turma_TurmaId" });
            DropIndex("dbo.ProfessorTurma", new[] { "Professor_Id" });
            DropIndex("dbo.TrabalhosExtras", new[] { "TurmaSelecionada_TurmaId" });
            DropIndex("dbo.Livro", new[] { "Turma_TurmaId" });
            AlterColumn("dbo.Notificacao", "Descricao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Notificacao", "Assunto", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.LojasRecomendadas", "Contato_Celular", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.LojasRecomendadas", "Endereco_Numero", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "UserLogin", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Contato_Celular", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Endereco_Numero", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuario", "Foto", c => c.Byte(nullable: false));
            AlterColumn("dbo.Usuario", "Cpf", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Rg", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Aluno", "Etnia", c => c.Int(nullable: false));
            DropColumn("dbo.TrabalhosExtras", "TurmaSelecionada_TurmaId");
            DropColumn("dbo.Livro", "Turma_TurmaId");
            DropTable("dbo.ProfessorTurma");
            CreateIndex("dbo.Professor", "Turma_TurmaId");
            AddForeignKey("dbo.Professor", "Turma_TurmaId", "dbo.Turma", "TurmaId");
        }
    }
}
