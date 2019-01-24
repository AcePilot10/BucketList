namespace BucketList.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _014 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserEvents", "ItemId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserEvents", "ItemId", c => c.String());
        }
    }
}
