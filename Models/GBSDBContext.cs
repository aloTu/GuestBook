using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuestBook.Models
{
    public class GBSDBContext:DbContext
    {
        public DbSet<Guestbook> Guestbooks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}