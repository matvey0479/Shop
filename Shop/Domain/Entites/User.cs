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
            Fio = fio;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string Fio {  get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Order? Order { get; set; }
    }
}
