using System.ComponentModel.DataAnnotations;
namespace Boutique.Models;
public class Categorie
{
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string Nom { get; set; } = string.Empty;
    [StringLength(255)]
    public string? Description { get; set; }
    public ICollection<Produit> Produits { get; set; } = new List<Produit>();
}