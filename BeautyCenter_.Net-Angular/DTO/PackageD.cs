using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BeautyCenter_.Net_Angular.Models;

namespace BeautyCenter_.Net_Angular.DTO
{
    public class PackageD
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public List<string> Services { get; set; }
    }
}
