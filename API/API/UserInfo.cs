using System;
using System.Collections.Generic;

namespace API
{
    public partial class UserInfo
    {
        public int IdUsers { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Jtoken { get; set; }
        public string Name { get; set; }
        public string Fam { get; set; }
        public string Patronimic { get; set; }
        public short Years { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public bool Isman { get; set; }
        public short IdHelth { get; set; }
    }
}
