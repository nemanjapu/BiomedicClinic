namespace BiomedicClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isHiddenAndImageFieldsInWebsitePage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebsitePages", "isHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.WebsitePages", "ImageName", c => c.String());
            AddColumn("dbo.WebsitePages", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebsitePages", "ImagePath");
            DropColumn("dbo.WebsitePages", "ImageName");
            DropColumn("dbo.WebsitePages", "isHidden");
        }
    }
}
