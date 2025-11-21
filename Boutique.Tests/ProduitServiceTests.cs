using Boutique.Data;
using Boutique.Data;
using Boutique.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Threading.Tasks;
using Xunit; // Nécessite l'installation du package Xunit

namespace Boutique.Tests // Assurez-vous d'utiliser le bon namespace
{
    public class ProduitServiceTests
    {
        // Le test vérifie qu'un produit peut être créé et ajouté à la base de données
        [Fact] // Annotation fournie par Xunit pour marquer une méthode comme un test
        public async Task CreateAsync_AjouteProduit()
        {
            // 1. Configuration de la base de données en mémoire
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "test_db") // Nécessite Microsoft.EntityFrameworkCore.InMemory
                .Options;

            // 2. Utilisation du contexte (similaire à l'image que vous avez fournie)
            using (var db = new AppDbContext(options))
            {
                // 3. Action : Ajouter un nouveau produit
                db.Produits.Add(new Produit
                {
                    Nom = "Test",
                    Prix = 10,
                    EnStock = true,
                    // Si CategorieId est obligatoire, ajoutez-le :
                    CategorieId = 1
                });

                // Sauvegarder les changements
                await db.SaveChangesAsync();

                // 4. Assertion : Vérifier qu'il y a bien 1 produit dans la base de données
                // Le test réussit si le nombre d'éléments dans la table Produits est égal à 1
                Assert.Equal(1, await db.Produits.CountAsync());
            }
        }
    }
}