namespace SchoolManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class school4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Aluno", "Etnia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Aluno", "Etnia", c => c.Int());
        }
    }
}
