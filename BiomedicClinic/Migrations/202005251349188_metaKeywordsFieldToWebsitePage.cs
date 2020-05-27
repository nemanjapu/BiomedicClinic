namespace BiomedicClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class metaKeywordsFieldToWebsitePage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebsitePages", "MetaKeywords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebsitePages", "MetaKeywords");
        }
    }
}
