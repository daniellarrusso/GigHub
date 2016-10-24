namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres VALUES(1, 'Pop')");
            Sql("INSERT INTO Genres VALUES(2, 'Rock')");
            Sql("INSERT INTO Genres VALUES(3, 'Jazz')");
            Sql("INSERT INTO Genres VALUES(4, 'Country')");
            Sql("INSERT INTO Genres VALUES(5, 'Folk')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE GenreId < 6");
        }
    }
}
