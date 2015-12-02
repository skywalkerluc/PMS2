namespace SchoolManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class school : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aluno", "Observacoes", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Turma", "HorariosTurmaId", c => c.Int(nullable: false));
            AddColumn("dbo.ResultadosProvas", "Nota", c => c.Int(nullable: false));
            AddColumn("dbo.ResultadosProvas", "Gabarito", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultadosProvas", "Gabarito");
            DropColumn("dbo.ResultadosProvas", "Nota");
            DropColumn("dbo.Turma", "HorariosTurmaId");
            DropColumn("dbo.Aluno", "Observacoes");
        }
    }
}
