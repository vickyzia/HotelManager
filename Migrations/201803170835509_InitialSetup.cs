namespace CoinPaymentsDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        TxnId = c.String(),
                        Address = c.String(),
                        ConfirmationsNeeded = c.Int(nullable: false),
                        TimeOut = c.Int(nullable: false),
                        StatusUrl = c.String(),
                        QRcodeUrl = c.String(),
                        Status = c.Int(nullable: false),
                        StatusMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transactions");
        }
    }
}
