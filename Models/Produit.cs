using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boutique.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Nom { get; set; } = string.Empty;

        [Range(0, 999999)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Prix { get; set; }

        public bool EnStock { get; set; } = true;

        [Display(Name = "Catégorie")]
        public int CategorieId { get; set; }
        public Categorie? Categorie { get; set; }
    }
}
