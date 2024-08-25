using BeautyCenter_.Net_Angular.Models;

namespace BeautyCenter_.Net_Angular.DTO
{
    public class PackageUserDTO
    {
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public DateTime Date { get; set; }
        public string packageName { get; set; }
        public string userName { get; set; }

        //public int packageCount { get; set; }
        //public int userCount { get; set; }
        //public virtual Package Package { get; set; }
        //public virtual Userr User { get; set; }
    }
}
