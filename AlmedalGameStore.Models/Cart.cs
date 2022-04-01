using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmedalGameStore.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ValidateNever][ForeignKey("ProductId")] public int ProductId { get; set; }

        public Product Product { get; set; }

        [Range(1, 1000)] public int Count { get; set; }

        [ValidateNever] public string ApplicationUser { get; set; }

        [NotMapped] public double Price { get; set; }

        public DateTime LastEdited { get; set; }

    }
}
