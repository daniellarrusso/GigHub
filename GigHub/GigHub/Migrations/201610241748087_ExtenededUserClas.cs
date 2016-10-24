namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtenededUserClas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ArtistName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ArtistName");
        }
    }
}
