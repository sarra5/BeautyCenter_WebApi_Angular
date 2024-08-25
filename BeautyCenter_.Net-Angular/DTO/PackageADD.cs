using BeautyCenter_.Net_Angular.Models;

namespace BeautyCenter_.Net_Angular.DTO
{
    public class PackageADD
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public List<int> ServicesId { get; set; } = new List<int>();
    }
}

