using AutoMapper;
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
    public class UserController : ControllerBase
    {
        UnitWork unit;
        private readonly IMapper mapper;

        public UserController(UnitWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Userr> userrs = unit.UserRepository.selectall();
            //List<User> usersDTO = new List<User>();

            //foreach (Userr userr in userrs)
            //{
            //    User userDTO = new User() { 
            //        Id = userr.Id,
            //        Name = userr.Name,
            //        Email = userr.Email,
            //        Password = userr.Password,
            //        BankAccount = userr.BankAccount 
            //    };
            //    usersDTO.Add(userDTO);
            //}

            List<User> usersDTO = mapper.Map<List<User>>(userrs);
            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Userr usserr = unit.UserRepository.selectbyid(id);

            if (usserr == null)
                return NotFound();
            else
            {
                //User userDTO = new User()
                //{
                //    Id = usserr.Id,
                //    Name = usserr.Name,
                //    Email = usserr.Email,
                //    Password = usserr.Password,
                //    BankAccount = usserr.BankAccount
                //};
                User userDTO = mapper.Map<User>(usserr);
                return Ok(userDTO);
            }
        }

        [HttpPost]
        public ActionResult AddUser(User userDTO)
        {
            if (userDTO == null)
                return BadRequest();
            else
            {
                //Userr userr = new Userr();

                //userr.Id = userDTO.Id;
                //userr.Name = userDTO.Name;
                //userr.Email = userDTO.Email;
                //userr.Password = userDTO.Password;
                //userr.BankAccount = userDTO.BankAccount;

                Userr userr = mapper.Map<Userr>(userDTO);

                unit.UserRepository.add(userr);
                unit.UserRepository.save();

                return Ok(userDTO);
            }
        }

        [HttpPut]
        public ActionResult UpdateUser(User userDTO)
        {
            if (userDTO == null)
                return BadRequest();
            else
            {
                //Userr userr = unit.UserRepository.selectbyid(userDTO.Id);

                //userr.Name = userDTO.Name;
                //userr.Email = userDTO.Email;
                //userr.Password = userDTO.Password;
                //userr.BankAccount = userDTO.BankAccount;

                Userr userr = mapper.Map<Userr>(unit.UserRepository.selectbyid(userDTO.Id));
                
                unit.UserRepository.update(userr);
                unit.UserRepository.save();

                return Ok(userDTO);
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            if (id == null)
                return NotFound();
            Userr userr = unit.UserRepository.selectbyid(id);
            if (userr == null)
                return NotFound();
            unit.UserRepository.delete(id);
            unit.UserRepository.save();
            return Ok("Successfully deleted");
        }

    }
}
