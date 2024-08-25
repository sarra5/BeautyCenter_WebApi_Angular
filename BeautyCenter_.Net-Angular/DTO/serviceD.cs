using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeautyCenter_.Net_Angular.DTO
{
    public class serviceD
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }
        public string Category { get; set; }

    }
}
