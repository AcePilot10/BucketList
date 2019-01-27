namespace BucketList.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _100 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BucketListItems", "Completed", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BucketListItems", "Completed");
        }
    }
}
