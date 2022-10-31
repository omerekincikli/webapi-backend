﻿using System;
using Microsoft.AspNetCore.Mvc;
using BaseBackend.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BaseBackend.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;

        public EmployeeController(IWebHostEnvironment env,
            IEmployeeRepository employee,
            IDepartmentRepository department)
        {
            _env = env;
            _employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _department = department ?? throw new ArgumentNullException(nameof(department));
        }

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employee.GetEmployees());
        }


        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {

            var result = await _employee.InsertEmployee(emp);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp)
        {
            var result = await _employee.UpdateEmployee(emp);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return new JsonResult("Updated Successfully");
        }


        [HttpDelete]
        [Route("DeleteEmployee")]
        public JsonResult Delete(int id)
        {
            if (_employee.DeleteEmployee(id))
            {
                return new JsonResult("Deleted Successfully");
            }
            else
            {
                return new JsonResult("Not Found");
            }
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "\\Photos\\" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception e)
            {
                return new JsonResult("Fail: " + e.Message);
            }
        }


        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _department.GetDepartment());
        }
    }
}
