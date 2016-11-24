using System.Data.Entity.Migrations;

namespace ContactsBook.Repositories.EF.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.contact_email",
                c => new
                    {
                        email = c.String(nullable: false, maxLength: 100, unicode: false),
                        contact_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.email)
                .ForeignKey("dbo.contact", t => t.contact_id, cascadeDelete: true)
                .Index(t => t.contact_id);
            
            CreateTable(
                "dbo.contact",
                c => new
                    {
                        contact_id = c.Int(nullable: false, identity: true),
                        contact_first_name = c.String(nullable: false, maxLength: 50, unicode: false),
                        contact_last_name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.contact_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.contact_email", "contact_id", "dbo.contact");
            DropIndex("dbo.contact_email", new[] { "contact_id" });
            DropTable("dbo.contact");
            DropTable("dbo.contact_email");
        }
    }
}
