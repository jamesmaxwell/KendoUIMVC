using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KendoUIMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public double Weight { get; set; }

        public string Email { get; set; }
    }
}