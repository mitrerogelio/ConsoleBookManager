using ConsoleBookManager.model;
using ConsoleBookManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleBookManager.data;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    private string DbPath { get; }

    public BookContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "books.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}