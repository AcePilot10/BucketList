namespace BucketList.Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FollowedUsersJson", c => c.String());
            AddColumn("dbo.Users", "UserEventsJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserEventsJson");
            DropColumn("dbo.Users", "FollowedUsersJson");
        }
    }
}
