using System;
using System.ComponentModel.DataAnnotations;

namespace ApiUser.Dtos
{

    public class UserCreateDto
    {

        public UserCreateDto(string Name, string Surname, int Age)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Age = Age;
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