using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerTest.Models
{
    public class Player
    {
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
    }
}