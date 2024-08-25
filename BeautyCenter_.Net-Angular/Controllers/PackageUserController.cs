using BeautyCenter_.Net_Angular.DTO;
using BeautyCenter_.Net_Angular.Models;
using BeautyCenter_.Net_Angular.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace BeautyCenter_.Net_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageUserController : ControllerBase
    {
        UnitWork unit;
        IMapper mapper;

        public PackageUserController(UnitWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        //Get all users with packages and date:
        [HttpGet]
        public ActionResult getAllPackageUser()
        {
            List<PackageUser> allPackageUser = unit.PackageUserRepository.selectAllFromPU();

            List<PackageUserDTO> userPackageListDTO = mapper.Map<List<PackageUserDTO>>(allPackageUser);


            //foreach (PackageUser PackageUser in allPackageUser)
            //{
            //    PackageUserDTO pakcageUserDTO = new PackageUserDTO()
            //    {
            //        PackageId = PackageUser.PackageId,
            //        UserId = PackageUser.UserId,
            //        Date = PackageUser.Date,
            //        packageName = PackageUser.Package.Name,
            //        userName = PackageUser.User.Name,
            //    };
            //    userPackageListDTO.Add(pakcageUserDTO);
            //}
            return Ok(userPackageListDTO);
        }

        //-------------------------------------------------------------
        //Get by specific composite key:

        [HttpGet("{userId:int}/{packageId:int}")]
        public ActionResult GetPackageUser(int packageId, int userId)
        {
            PackageUser selectedPackageUser = unit.PackageUserRepository.getByCompositeKeyPU(packageId, userId);
            if (selectedPackageUser == null)
            {
                return NotFound();
            }
            else
            {
                PackageUserDTO packageUserDTO = mapper.Map<PackageUserDTO>(selectedPackageUser);
                //{
                //    Date = selectedPackageUser.Date,
                //    packageName = selectedPackageUser.Package.Name,
                //    userName = selectedPackageUser.User.Name,
                //};

                return Ok(packageUserDTO);
            }
        }
        //---------------------------------------------------

        //Get packages and it's reservation date by user name:
        [HttpGet("api/packages-by-user/{userId}")]
        public ActionResult getPackagesOfUser(int userId)
        {
            List<PackageUser> packagesOfUser = unit.PackageUserRepository.getByUserIdfromPU(userId);
            if (packagesOfUser == null)
            {
                return NotFound();
            }
            else
            {
                List<PackageUserDTO> packagesOfUserDTO = mapper.Map<List<PackageUserDTO>>(packagesOfUser);

                //foreach (PackageUser PackageUser in packagesOfUser)
                //{
                //    PackageUserDTO pakcageUserDTO = new PackageUserDTO()
                //    {
                //        PackageId = PackageUser.PackageId,
                //        UserId = PackageUser.UserId,
                //        Date = PackageUser.Date,
                //        packageName = PackageUser.Package.Name,
                //        userName = PackageUser.User.Name,
                //    };
                //    packagesOfUserDTO.Add(pakcageUserDTO);
                return Ok(packagesOfUserDTO);
            }
        }
  
        //------------------------------------------------------------------

        //Get users and it's reservation date by package name:
        [HttpGet("api/users-by-package/{PackageId}")] 
        public ActionResult getUsersOfPackage(int PackageId)
        {
            List<PackageUser> usersOfPackage = unit.PackageUserRepository.getByPackageIdfromPU(PackageId);
            if (usersOfPackage == null)
            {
                return NotFound();
            }
            else
            {
                List<PackageUserDTO> usersOfPackageDTO = mapper.Map<List<PackageUserDTO>>(usersOfPackage);            //foreach (PackageUser PackageUser in usersOfPackage)

                //{
                //    PackageUserDTO pakcageUserDTO = new PackageUserDTO()
                //    {
                //        PackageId = PackageUser.PackageId,
                //        UserId = PackageUser.UserId,
                //        Date = PackageUser.Date,
                //        packageName = PackageUser.Package.Name,
                //        userName = PackageUser.User.Name,
                //    };
                //    usersOfPackageDTO.Add(pakcageUserDTO);
                //}
                return Ok(usersOfPackageDTO);

            }
        }
        //---------------------------------------------------------------------

        //Get packages and users by specific date:
        [HttpGet("{Date}")] 
        public ActionResult getUserPackageByDate(DateTime Date)
        {
            List<PackageUser> packageUsers = unit.PackageUserRepository.getPackageUserByDate(Date);
            if (packageUsers == null)
            {
                return NotFound();
            }
            else
            {
                List<PackageUserDTO> packageUsersDTOList = mapper.Map<List<PackageUserDTO>>(packageUsers);     //foreach (PackageUser PackageUser in packageUsers)

                //{
                //    PackageUserDTO pakcageUserDTO = new PackageUserDTO()
                //    {
                //        PackageId = PackageUser.PackageId,
                //        UserId = PackageUser.UserId,
                //        Date = PackageUser.Date,
                //        packageName = PackageUser.Package.Name,
                //        userName = PackageUser.User.Name,
                //    };
                //    packageUsersDTOList.Add(pakcageUserDTO);
                //}
                return Ok(packageUsersDTOList);

            }
        }
        //------------------------------------------------------

        //Adding new PackageUser:
        [HttpPost]
        public ActionResult addPackageUser(PackageUserDTO newPackageUser)
        {
            Package package = unit.PackageRepository.selectbyid(newPackageUser.PackageId);
            Userr user = unit.UserRepository.selectbyid(newPackageUser.UserId);

            if (package == null)
            {
                return BadRequest();
            }
            
            if (user == null)
            {
                return BadRequest();
            }
            if (newPackageUser == null)
            {
                return BadRequest();
            }
            
            else
            {
                PackageUser packageUser = mapper.Map <PackageUser> (newPackageUser);
                //{
                //    PackageId = newPackageUser.PackageId,
                //    UserId = newPackageUser.UserId,
                //    Date = newPackageUser.Date
                //};
                unit.PackageUserRepository.add(packageUser);
                unit.PackageUserRepository.save();
                return Ok(newPackageUser);
            }
        }
        //--------------------------------------------------------------

        //Update:
        [HttpPut]
        public ActionResult updatePackageUser(int userId, int packageId, DateTime date)
        {
            // Fetch the existing PackageUser
            PackageUser existingPU = unit.PackageUserRepository.getByCompositeKeyPU(packageId, userId);
            if (existingPU == null)
            {
                return NotFound();
            }
            else
            {
                // Directly update the fetched entity's properties
                existingPU.Date = date;

                // The entity is already being tracked, so just call update to set its state to Modified
                unit.PackageUserRepository.update(existingPU);

                // Save the changes
                unit.PackageUserRepository.save();
                return Ok("Done");
            }
        }


        //--------------------------------------------------------------
        //Delete by Composite key:

        [HttpDelete("{userId:int}/{packageId:int}")]
        public ActionResult deletePackageUserByID(int userId,int packageId)
        {
            PackageUser packageUser= unit.PackageUserRepository.getByCompositeKeyPU(userId,packageId);
            if (packageUser == null)
            {
                return NotFound();
            }
            else
            {
                unit.PackageUserRepository.deleteByCompositeKeyG(userId, packageId);
                unit.PackageUserRepository.save();
                return Ok("Deleted Successfully");
            }       
        }
        //--------------------------------------------------------------
        //Delete by UserID:

        [HttpDelete("{userId:int}")]
        public ActionResult deleteAllPckageUserByUserId(int userId)
        {
            List<PackageUser> packageUser = unit.PackageUserRepository.getUserPackageByCompositeUserID(userId);
            if (packageUser == null)
            {
                return NotFound();
            }
            else
            {
                unit.PackageUserRepository.deletepackagesUserByuserId(packageUser);
                unit.PackageUserRepository.save();
                return Ok("Deleted Successfully");
            }
        }
        //-----------------------------------------------------------

        //Delete PackageUser by Date:
        [HttpDelete("{Date}")] 
        public ActionResult deletePackageUserByDate(DateTime Date)
        {
            List<PackageUser> packageUsers = unit.PackageUserRepository.getPackageUserByDate(Date);
            if (packageUsers == null)
            {
                return NotFound();
            }
            else
            {
                unit.PackageUserRepository.deletePackageUserByDate(Date);
                unit.PackageUserRepository.save();
                return Ok("Done");
            }
        }

        //-----------------------------------------------------------------------------------------------
        //Delete By UserID



    }
}
