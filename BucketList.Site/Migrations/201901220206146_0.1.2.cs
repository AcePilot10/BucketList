namespace BucketList.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _012 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "UserEventsJson");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserEventsJson", c => c.String());
        }
    }
}
