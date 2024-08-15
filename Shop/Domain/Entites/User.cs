using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entites
{
    public class User: Entity
    {
        public User() { }
        public User(string? login,string? password,string fio, string email,string? phoneNumber) 
        {
            Login = login;
            Password = password;
            this.fio = fio;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string fio {  get; set; }
        public string email { get; set; }
        public string? phoneNumber { get; set; }
        public Order? order { get; set; }
    }
}
