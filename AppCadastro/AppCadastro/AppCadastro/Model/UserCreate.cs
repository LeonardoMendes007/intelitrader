using System;
using System.Collections.Generic;
using System.Text;

namespace AppCadastro.Model
{
    class UserCreate
    {
        public UserCreate(string name, string surname, int age)
        {

            this.Name = name;
            this.Surname = surname;
            this.Age = age;
        }



        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }
    }
    

}
