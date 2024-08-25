using BeautyCenter_.Net_Angular.DTO;
using BeautyCenter_.Net_Angular.Models;
using BeautyCenter_.Net_Angular.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyCenter_.Net_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        UnitWork unit;
        public UserServiceController(UnitWork unit)
        {
            this.unit = unit;
        }
        //Adding new UserService:
        [HttpPost]
        public ActionResult addUserService(Uservice newUserService)
        {
            ServiceResponse service = unit.ServiceRepository.selectbyid(newUserService.ServiceId);
            Userr user = unit.UserRepository.selectbyid(newUserService.UserId);

            if (service == null)
            {
                return BadRequest("error from  1");
            }

            if (user == null)
            {
                return BadRequest("error from  2");
            }
            if (newUserService == null)
            {
                return BadRequest("error from  3");
            }

            else
            {
                UserService UserService = new UserService()
                {
                    ServiceId = newUserService.ServiceId,
                    UserId = newUserService.UserId,
                    Date = newUserService.Date
                };
                unit.UserServiceRepository.add(UserService);
                unit.UserServiceRepository.save();
                return Ok(newUserService);
            }
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            List<UserService> usrModelServices = unit.UserServiceRepository.selectall(); //db
            List<Uservice> userServicesDTO = new List<Uservice>(); //properties dto


            foreach (UserService usrModelService in usrModelServices)

            {
                Uservice userServiceDTO = new Uservice()
                {


                    UserId = usrModelService.UserId,
                    ServiceId = usrModelService.ServiceId,
                    Date = usrModelService.Date,

                };
                userServicesDTO.Add(userServiceDTO);

            }
            return Ok(userServicesDTO);
        }

        [HttpGet("{userId}/{serviceId}")]
        public ActionResult GetById(int userId, int serviceId)
        {
            UserService usrS = unit.UserServiceRepository.SelectByCompositeKey(userId, serviceId);
            Userr usserr = unit.UserRepository.selectbyid(userId);
            var service = unit.ServiceRepository.selectbyid(serviceId);

            if (usserr == null || service == null || usrS == null)
                return NotFound();
            else
            {
                Uservice userServiceDTO = new Uservice()
                {
                    UserId = usrS.UserId,
                    ServiceId = usrS.ServiceId,
                    Date = usrS.Date,
                };

                var userobj = new
                {
                    Name = usserr.Name
                };

                var serviceObj = new
                {
                    Name = service.Name
                };

                var responseObject = new
                {
                    User = userobj,
                    Service = serviceObj,
                    UserService = userServiceDTO
                };

                return Ok(responseObject);
            }
        }

        [HttpGet("userservices/{date}")]
        public ActionResult GetUserServicesByDate(DateTime date)
        {
            // Query the UserService table to retrieve records for the specified date
            var userServices = unit.UserServiceRepository.GetUserServicesByDate(date);

            // Check if any user services are found for the specified date
            if (userServices == null || !userServices.Any())
                return NotFound("No user services found for the specified date");

            // Construct a list to hold the result
            var result = new List<dynamic>();

            // Iterate through the retrieved user services to extract user and service names
            foreach (var userService in userServices)
            {
                // Retrieve the user name from the User table using the UserId
                var user = unit.UserRepository.selectbyid(userService.UserId);
                var userName = user != null ? user.Name : "Unknown User";

                // Retrieve the service name from the Service table using the ServiceId
                var service = unit.ServiceRepository.selectbyid(userService.ServiceId);
                var serviceName = service != null ? service.Name : "Unknown Service";

                // Create a dynamic object with the retrieved information
                var response = new
                {
                    UserId = userService.UserId,
                    ServiceId = userService.ServiceId,
                    Date = userService.Date,
                    UserName = userName,
                    ServiceName = serviceName
                };

                // Add the dynamic object to the result list
                result.Add(response);
            }

            // Return the list of dynamic objects as the response
            return Ok(result);
        }


        [HttpGet("by-user/{userId}")]
        public ActionResult GetUserServicesByUserId(int userId)
        {
            // Query the UserService table to retrieve records for the specified user ID
            var userServices = unit.UserServiceRepository.getByUserIdfromUS(userId);

            // Check if any user services are found for the specified user ID
            if (userServices == null || !userServices.Any())
                return NotFound("No user services found for the specified user ID");

            // Construct a list to hold the result
            var result = new List<dynamic>();

            // Iterate through the retrieved user services to extract user and service names
            foreach (var userService in userServices)
            {
                // Retrieve the user name from the User table using the UserId
                var user = unit.UserRepository.selectbyid(userService.UserId);
                var userName = user != null ? user.Name : "Unknown User";

                // Retrieve the service name from the Service table using the ServiceId
                var service = unit.ServiceRepository.selectbyid(userService.ServiceId);
                var serviceName = service != null ? service.Name : "Unknown Service";

                // Create a dynamic object with the retrieved information
                var response = new
                {
                    UserId = userService.UserId,
                    ServiceId = userService.ServiceId,
                    Date = userService.Date,
                    UserName = userName,
                    ServiceName = serviceName
                };

                // Add the dynamic object to the result list
                result.Add(response);
            }

            // Return the list of dynamic objects as the response
            return Ok(result);
        }


        
        [HttpPut("{userId}/{serviceId}")]
        public IActionResult UpdateUserService(int userId, int serviceId, [FromBody] Uservice userServiceDTO)
        {
            if (userServiceDTO == null || userId!=userServiceDTO.UserId || serviceId!=userServiceDTO.ServiceId)
            {
                return BadRequest("Invalid user service data");
            }

            var existingUserService = unit.UserServiceRepository.SelectByCompositeKey(userId, serviceId);
            if (existingUserService == null)
            {
                return NotFound("User service not found");
            }

            // Update the entire entity with the new data
            existingUserService.UserId = userServiceDTO.UserId;
            existingUserService.ServiceId = userServiceDTO.ServiceId;
            existingUserService.Date = userServiceDTO.Date;

            // Save changes to the database
            unit.UserServiceRepository.update(existingUserService);
            unit.UserServiceRepository.save();

            return Ok("User service updated successfully");
        }





        //--------------------------------------------------------------

        //Delete by Composite key:

        [HttpDelete("{userId:int}/{ServiceId:int}")]
        public ActionResult deleteUserServiceByID(int userId, int ServiceId)
        {
            UserService UserService = unit.UserServiceRepository.getByCompositeKeyUS(userId, ServiceId);
            if (UserService == null)
            {
                return NotFound();
            }
            else
            {
                unit.UserServiceRepository.deleteByCompositeKeyG(userId, ServiceId);
                unit.UserServiceRepository.save();
                return Ok("Deleted Successfully");
            }
        }
        //-----------------------------------------------------------

        //Delete UserService by Date:
        [HttpDelete("{Date}")]
        public ActionResult deleteUserPackageByDate(DateTime Date)
        {
            List<UserService> UserServices = unit.UserServiceRepository.GetUserServicesByDate(Date);
            if (UserServices == null)
            {
                return NotFound();
            }
            else
            {
                unit.UserServiceRepository.deleteUserServiceByDate(Date);
                unit.UserServiceRepository.save();
                return Ok("UserService Deleted succ");
            }
        }

        //------------------------------------------------------------------------------
        //Delete by UserID:

        [HttpDelete("{userId:int}")]
        public ActionResult deleteAllUserUserByUserId(int userId)
        {
            List<UserService> ServiceUser = unit.PackageUserRepository.getUserServiceByCompositeUserID(userId);
            if (ServiceUser == null)
            {
                return NotFound();
            }
            else
            {
                unit.PackageUserRepository.deleteServicesUserByuserId(ServiceUser);
                unit.PackageUserRepository.save();
                return Ok("Deleted Successfully");
            }
        }
    }
}









