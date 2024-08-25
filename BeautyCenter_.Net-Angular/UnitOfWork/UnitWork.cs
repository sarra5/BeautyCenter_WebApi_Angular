using BeautyCenter_.Net_Angular.Controllers;
using BeautyCenter_.Net_Angular.Models;
using BeautyCenter_.Net_Angular.Repository;

namespace BeautyCenter_.Net_Angular.UnitOfWork
{
    public class UnitWork
    {
        BeautyCenterContext db;
        GenericRepository<Package> packageRepository;
        GenericRepository<PackageUser> packageUserRepository;
        GenericRepository<ServiceResponse> serviceRepository;
        GenericRepository<Userr> userRepository;
        GenericRepository<UserService> userServiceRepository;
        GenericRepository<PackageService> packageServiceRepository;



        public UnitWork(BeautyCenterContext db)
        {
            this.db = db;
        }

        public GenericRepository<Package> PackageRepository
        {
            get
            {
                if (packageRepository == null)
                {
                    packageRepository = new GenericRepository<Package>(db);
                }
                return packageRepository;
            }
        }

        public GenericRepository<PackageUser> PackageUserRepository
        {
            get
            {
                if (packageUserRepository == null)
                {
                    packageUserRepository = new GenericRepository<PackageUser>(db);
                }
                return packageUserRepository;
            }
        }

        public GenericRepository<ServiceResponse> ServiceRepository
        {
            get
            {
                if (serviceRepository == null)
                {
                    serviceRepository = new GenericRepository<ServiceResponse>(db);
                }
                return serviceRepository;
            }
        }

        public GenericRepository<Userr> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<Userr>(db);
                }
                return userRepository;
            }
        }

        public GenericRepository<UserService> UserServiceRepository
        {
            get
            {
                if (userServiceRepository == null)
                {
                    userServiceRepository = new GenericRepository<UserService>(db);
                }
                return userServiceRepository;
            }
        }
        public GenericRepository<PackageService> PackageServiceRepository
        {
            get
            {
                if (packageServiceRepository == null)
                {
                    packageServiceRepository = new GenericRepository<PackageService>(db);
                }
                return packageServiceRepository;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
