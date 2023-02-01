using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Borowik.Database.Sqlite.Migrations;

internal class M001_InitialMigration : IMigration
{
    public int Version => 1;

    public async Task MigrateAsync(SqliteConnection connection, CancellationToken cancellationToken)
    {
        await connection.ExecuteAsync("""
            CREATE TABLE BOOKSHELVES(
                ID TEXT PRIMARY KEY,
                NAME TEXT NOT NULL,
                DESCRIPTION TEXT NULL,
                CREATED_AT INT NOT NULL);

            CREATE TABLE BOOKS(
                ID TEXT PRIMARY KEY,
                BOOKSHELF_ID TEXT NOT NULL,
                NAME TEXT NOT NULL,
                CONTENT TEXT NOT NULL,
                AUTHOR TEXT NULL,
                CREATED_AT INT NOT NULL,
                LAST_OPENED_AT INT NULL);
            """);
    }
}