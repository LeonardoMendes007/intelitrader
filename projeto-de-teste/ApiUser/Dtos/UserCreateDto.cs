using System;
using System.ComponentModel.DataAnnotations;

namespace ApiUser.Dtos
{

    public class UserCreateDto
    {
        public UserCreateDto( string name, string surname, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
    }
}