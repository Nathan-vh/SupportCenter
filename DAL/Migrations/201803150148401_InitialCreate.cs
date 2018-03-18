namespace SC.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketNumber = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 100),
                        DateOpened = c.DateTime(nullable: false),
                        State = c.Byte(nullable: false),
                        DeviceName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TicketNumber)
                .Index(t => t.State);
            
            CreateTable(
                "dbo.TicketResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsClientResponse = c.Boolean(nullable: false),
                        Ticket_TicketNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ticket", t => t.Ticket_TicketNumber)
                .Index(t => t.Ticket_TicketNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketResponse", "Ticket_TicketNumber", "dbo.Ticket");
            DropIndex("dbo.TicketResponse", new[] { "Ticket_TicketNumber" });
            DropIndex("dbo.Ticket", new[] { "State" });
            DropTable("dbo.TicketResponse");
            DropTable("dbo.Ticket");
        }
    }
}
