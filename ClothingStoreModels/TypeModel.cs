using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothingStore.Models
{
    public class TypeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Product Type")]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
