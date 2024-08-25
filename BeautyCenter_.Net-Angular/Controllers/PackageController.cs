using AutoMapper;
using BeautyCenter_.Net_Angular.DTO;
using BeautyCenter_.Net_Angular.Models;
using BeautyCenter_.Net_Angular.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautyCenter_.Net_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : Controller
    {
        UnitWork unit;
        BeautyCenterContext db;
        private readonly IMapper mapper;



        public PackageController(UnitWork unit , IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        //--------------------------------------------------------------------------------------------------------------------------


        [HttpGet]
        public IActionResult GetAll()
        {
            List<Package> packages = unit.PackageRepository.SelectAllPackagesWithServices();
            List<PackageD> packagesDTO = new List<PackageD>();

            foreach (Package package in packages)
            {
                PackageD PDTO = new PackageD()
                {
                    Id = package.Id,
                    Name = package.Name,
                    Price = package.Price,
                    Services = new List<string>()  // Initialize a list to hold service names
                };

                foreach (var packageService in package.PackageServices)
                {
                    PDTO.Services.Add(packageService.Service.Name); // Add each service name to the list
                }

                packagesDTO.Add(PDTO);
            }

            return Ok(packagesDTO);
        }




        //--------------------------------------------------------------------------------------------------------------------------


        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            Package package = unit.PackageRepository.SelectPackageWithServicesById(id);
            PackageD PDTO = new PackageD()
            {
                Id = package.Id,
                Name = package.Name,
                Price = package.Price,
                Services = new List<string>()  // Initialize a list to hold service names
            };

            foreach (var packageService in package.PackageServices)
            {
                PDTO.Services.Add(packageService.Service.Name); // Add each service name to the list
            }


            return Ok(PDTO);


        }


        //--------------------------------------------------------------------------------------------------------------------------


        [HttpPost("add")]
        public IActionResult AddPackage(PackageADD packageDto)
        {
            if (packageDto == null)
            {
                return BadRequest();
            }

            // Create a new Package object
            Package p = new Package()
            {
                Id=packageDto.Id,
                Name = packageDto.Name,
                Price = packageDto.Price
            };

            // Add the Package object to the database
            unit.PackageRepository.add(p);
            unit.SaveChanges(); // Save changes after adding the package

            // Loop through the services IDs and add PackageService objects
            foreach (var serviceId in packageDto.ServicesId)
            {
                // Create a new PackageService object
                PackageService ps = new PackageService()
                {
                    ServiceId = serviceId,
                    PackageId = p.Id // Assign the ID of the newly added package
                };

                // Add the PackageService object to the database
                unit.PackageServiceRepository.add(ps);
            }

            unit.SaveChanges(); // Save changes after adding all the package services

            return Ok();
        }





        //--------------------------------------------------------------------------------------------------------------------------

        [HttpPut("edit")] // Route for editing a package

        public IActionResult edit(PackageEdit packageDto)
        {

            if (packageDto == null)
            {
                return BadRequest();
            }
            Package p = new Package()
            {
                Id = packageDto.Id,
                Name = packageDto.Name,
                Price = packageDto.Price
            };
            //foreach (var serviceId in packageDto.ServicesId)
            //{
            //    PackageService ps = new PackageService()
            //    {
            //        ServiceId = serviceId,
            //        PackageId = p.Id 
            //    };

            //    unit.PackageServiceRepository.update(ps);
            //}

            unit.PackageRepository.update(p);
            unit.SaveChanges();
            return Ok();
        }




        //--------------------------------------------------------------------------------------------------------------------------

        [HttpDelete("{id}")]
        public IActionResult deleteItem(int id)
        {
   
            unit.PackageRepository.deletePackageService(id);
            unit.PackageRepository.delete(id);

            unit.SaveChanges();
            return Ok();
        }

    }
}
