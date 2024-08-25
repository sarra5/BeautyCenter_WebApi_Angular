using AutoMapper;
using BeautyCenter_.Net_Angular.DTO;
using BeautyCenter_.Net_Angular.Models;
using BeautyCenter_.Net_Angular.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeautyCenter_.Net_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ServicesController : ControllerBase
    {
        UnitWork unit;
        IMapper mapper;
        public ServicesController(UnitWork unit,IMapper mapper)
        {
            this.unit = unit;
            this.mapper=mapper;
        }






        [HttpGet("AllServices")]
        public IActionResult GetAllServices()
        {
            List<ServiceResponse> ServiseList = unit.ServiceRepository.selectall();
            //List<serviceD> ServiseListDTO = new List<serviceD>();
            //foreach (ServiceResponse serv in ServiseList)
            //{
            //  serviceD servD = new serviceD()
            //  {
            //    Id = serv.Id,
            //    Name = serv.Name,
            //    Price = serv.Price,
            //    Category = serv.Category,
            //  };
            //ServiseListDTO.Add(servD);
            //}
            List<serviceD> ServiseListDTO = mapper.Map<List<serviceD>>(ServiseList);

            if (ServiseListDTO.Count > 0)
            {
                return Ok(ServiseListDTO);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetServiceById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            ServiceResponse service = unit.ServiceRepository.selectbyid(id);
            if (service == null)
            {
                return NotFound();
            }
            else
            {
                //serviceD serviceDTO = new serviceD()
                //{
                //    Id = service.Id,
                //    Name = service.Name,
                //    Price = service.Price,
                //    Category = service.Category,
                //};
                serviceD serviceDTO=mapper.Map<serviceD>(service);
                return Ok(serviceDTO);

            }
        }



        [HttpGet("ServiceByName/{name:alpha}")]
        public IActionResult GetServiceByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            else
            {
                //List<ServiceResponse> ListOfServices = unit.ServiceRepository.GetServicesByName(name);
                ServiceResponse service = unit.ServiceRepository.GetServicesByName(name);
                if (service == null)
                {
                    return NotFound();
                }
                else
                {
                    serviceD ServiceDTO = mapper.Map<serviceD>(service);  
                    
                    return Ok(ServiceDTO);
                }
                
            }
        }



        [HttpGet("ServicesByCategory/{categ:alpha}")]
        public IActionResult GetServiceByCategory(string categ)
        {
            if (categ == null)
            {
                return BadRequest();
            }
            else
            {
                List<ServiceResponse> ListOfServices = unit.ServiceRepository.GetServicesByCategory(categ);
                if (ListOfServices.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    List<serviceD> ListOfServicesDTO = mapper.Map<List<serviceD>>(ListOfServices);
                    return Ok(ListOfServicesDTO);
                }
                
            }
        }

        [HttpGet("AllCategories")]
        public IActionResult GetAllCategories()
        {
            List<string> ListOfCategories=unit.ServiceRepository.GetAllCategories();
            if(ListOfCategories == null || ListOfCategories.Count<0)
            {
                return NotFound();
            }
            return Ok(ListOfCategories);
        }


        [HttpGet("AllServicesName")]
        public IActionResult GetAllServicesName()
        {
            List<string> ListOfServicesNames = unit.ServiceRepository.GetAllServicesName();
            if (ListOfServicesNames == null || ListOfServicesNames.Count < 0)
            {
                return NotFound();
            }
            return Ok(ListOfServicesNames);
        }



        [HttpPost]
        public IActionResult AddNewService(serviceD service)
        {
            if (service == null)
            {
                return BadRequest();
            }
            else
            {
                //ServiceResponse NewService = new ServiceResponse()
                //{
                //    Id = service.Id,
                //    Name= service.Name,
                //    Price = service.Price,
                //    Category= service.Category,
                //};

                ServiceResponse NewService=mapper.Map<ServiceResponse>(service);
                unit.ServiceRepository.add(NewService);
                unit.ServiceRepository.save();
                return Ok(service);
            }
        }

        //here a problem


        [HttpPut]
        public IActionResult AddServiceById(serviceD service)
        {
            if (service == null)
            {
                return BadRequest();
            }
            ServiceResponse serv = unit.ServiceRepository.selectbyid(service.Id);
            if (service == null)
            {
                return NotFound();
            }
            else
            {
                //serv.Name = service.Name;
                //serv.Price = service.Price;
                //serv.Category = service.Category;
                ServiceResponse serv2 = mapper.Map<ServiceResponse>(serv);

                unit.ServiceRepository.update(serv2);
                unit.ServiceRepository.save();
                return Ok(service);
            }    
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteServiceById(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            ServiceResponse ser=unit.ServiceRepository.selectbyid(id);
            if(ser ==null)
            {
                return NotFound();
            }
            else
            {
                unit.ServiceRepository.delete(id);
                unit.ServiceRepository.save();
                return Ok();
            }
        }


    }
}
