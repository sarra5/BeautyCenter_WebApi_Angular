using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeautyCenter_.Net_Angular.Models
{
    public class PackageService
    {
        [ForeignKey("ServiceResponse")]
        public int ServiceId { get; set; }

        [ForeignKey("Package")]
        public int PackageId { get; set; }

        public virtual Package Package { get; set; }

        public virtual ServiceResponse Service { get; set; }

    }
}