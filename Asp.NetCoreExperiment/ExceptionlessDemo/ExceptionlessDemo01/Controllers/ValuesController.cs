﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionlessDemo01.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {

                ExceptionlessClient.Default.SubmitLog("Logging made easy",Exceptionless.Logging.LogLevel.Warn);

                var data = new Exceptionless.Models.DataDictionary();
                data.Add("data1key", "data1value");
                ExceptionlessClient.Default.SubmitEvent(new Exceptionless.Models.Event { Count = 1, Date = DateTime.Now, Data = data , Geo="geo", Message="message", ReferenceId="referencelId", Source="source", Tags=new Exceptionless.Models.TagSet() { "tags" },Type="type", Value=1.2m });
                ExceptionlessClient.Default.SubmitFeatureUsage("feature");
                ExceptionlessClient.Default.SubmitNotFound("notfound");
               

                throw new Exception("我的异常"+DateTime .Now);
            }
            catch(Exception exc)
            {
                exc.ToExceptionless().Submit();
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
