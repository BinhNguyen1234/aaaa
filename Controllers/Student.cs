﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using dotnet_vite_react;
using System.Linq;
using Model = dotnet_vite_react.Model;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using dotnet_vite_react.Model;

namespace dotnet_vite_react.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class Student : Controller
    {
        private IServiceProvider _services;

        public Student(IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("{name}")]
        [ActionName("info")]
        public IActionResult getInfo([FromRoute] string? name)
        {
            using (var context = _services.GetService<Context>())
            {
                if (context != null)
                {
                    Model.Student newStudent = new ()
                    {
                        FirstName = "Binh",
                        LastName = "Nguyen"
                    };
                    context.Add(newStudent);
                    context.SaveChanges();
                    return Content("name: " + name);
                }
                else return UnprocessableEntity();
            }

        }
        [HttpPost]
        [ActionName("create")]
        public async Task<IActionResult> postInfo([FromBody] object body)
        {
            Request.Body.Position = 0;
            var reader = await new StreamReader(Request.Body, Encoding.UTF8).ReadToEndAsync();
            dynamic content = JsonConvert.DeserializeObject(reader);
            return Content(content.First.name);
        }
    }
    [Route("[controller]/[action]")]
    public class Course : ControllerBase
    {
        private IServiceProvider _service;
        public Course(IServiceProvider service) {  
            _service = service; 
        }
        [HttpPost]
        [ActionName("register")]
        public IActionResult registerCourse([FromBody] RegisterForm body)
        {
            try
            {
                using (var context = _service.GetService<Context>())
                {
                    Model.Course course = new()
                    {
                        Title = body.TitleCourse,
                        Credits = body.Credits
                    };
                    Model.Enrollment enrollment = new()
                    {
                        Grade = body.Grade
                    };
                    Model.Student student = new()
                    {
                        LastName = body.LastName,
                        FirstName = body.FirstName
                    };
                    if (context != null)
                    {
                        context.Add(course);
                        context.Add(enrollment);
                        context.Add(student);
                    }
                    return Content("Sucess");

                }
            } catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }

            
        }

    }
}

public class RegisterForm
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string TitleCourse { get; set; }
    public required string Credits { get; set; }
    public required string Grade { get; set; }
}
