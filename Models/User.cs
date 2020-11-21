using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuestBook.Models
{
    public class User
    {
        public int UserId { get; set;}
        [Display(Name="留言人邮箱", Order = 15000)]
        public string Email { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public SystemRole SRole { get; set; }
        public virtual ICollection<Guestbook> Guestbooks { get; set; }


    }
    public enum SystemRole { 普通用户,管理员 }
}