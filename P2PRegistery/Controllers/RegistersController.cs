using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2pModels.model;

namespace P2PRegistery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private static readonly Dictionary<String, HashSet<FileEndPoint>> register = new Dictionary<string, HashSet<FileEndPoint>>();
        
        // GET: api/Registers
        [HttpGet]
        public Dictionary<String, HashSet<FileEndPoint>> Get()
        {
            return register;
        }

        // GET: api/Registers/peter.txt
        [HttpGet("{filename}")]
        public HashSet<FileEndPoint> Get(String filename)
        {
            if (!register.Keys.Contains(filename)) 
                return new HashSet<FileEndPoint>();
            
            return register[filename];
        }

        // POST: api/Registers/peter.txt
        [HttpPost("{filename}")]
        public int Post(String filename, [FromBody] FileEndPoint value)
        {
            int res = -1;

            if (!register.Keys.Contains(filename))
            {
                register.Add(filename, new HashSet<FileEndPoint>());
            }
            var fileset = register[filename];
            res = fileset.Add(value) ? 1 : 0;

            return res;
        }

        // PUT: api/ApiWithActions/5
        [HttpPut("{filename}")]
        public int Put(String filename, [FromBody] FileEndPoint value)
        {
            int res = -1;

            if (!register.ContainsKey(filename))
            {
                return 0; // do not exists
            }

            var fileset = register[filename];
            res = fileset.Remove(value) ? 1 : 0;

            if (fileset.Count == 0)
            {
                // no more entries for this filename
                register.Remove(filename);
            }

            return res;
        }
    }
}
