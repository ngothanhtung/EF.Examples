using EF.CodeFirst.Examples.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF.CodeFirst.Examples.Data
{
    public class OnlineShopDb : DbContext
    {
        public OnlineShopDb() : base("DefaultConnection")
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}