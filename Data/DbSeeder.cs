using Boutique.Models;
namespace Boutique.Data;
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Categories.Any())
        {
            db.Categories.AddRange(
            new Categorie { Nom = "Informatique" },
            new Categorie { Nom = "Maison" }
            );
            await db.SaveChangesAsync();
        }
    }
}