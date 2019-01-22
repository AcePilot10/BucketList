namespace BucketList.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _013 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BucketListItems", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BucketListItems", "Status", c => c.String());
        }
    }
}
