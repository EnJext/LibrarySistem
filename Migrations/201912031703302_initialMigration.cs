namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        StartReservation = c.DateTime(nullable: false),
                        FinishReservation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreBooks",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Book_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reservations", "BookId", "dbo.Books");
            DropForeignKey("dbo.GenreBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.GenreBooks", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreBooks", new[] { "Book_Id" });
            DropIndex("dbo.GenreBooks", new[] { "Genre_Id" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "BookId" });
            DropTable("dbo.GenreBooks");
            DropTable("dbo.Users");
            DropTable("dbo.Reservations");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
        }
    }
}
