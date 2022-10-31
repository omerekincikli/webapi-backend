﻿using BaseBackend.Models;
using BaseBackend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _department;

        public DepartmentController(IDepartmentRepository department)
        {
            _department = department ?? throw new ArgumentNullException(nameof(department));
        }

        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _department.GetDepartment());
        }

        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _department.GetDepartmentByID(Id));
        }

        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(Department dep)
        {
            var result = await _department.InsertDepartment(dep);
            if (result.ID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(Department dep)
        {
            await _department.UpdateDepartment(dep);
            return new JsonResult("Updated Successfully");
        }


        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteDepartment")]
        public JsonResult Delete(int id)
        {
            if (_department.DeleteDepartment(id))
            {
                return new JsonResult("Deleted Successfully");
            }
            else
            {
                return new JsonResult("Not Found");
            }
        }
    }
}
