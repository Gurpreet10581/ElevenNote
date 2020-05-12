namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Note", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Note", "Content", c => c.String(nullable: false));
        }
    }
}
