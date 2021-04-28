using System;
using System.ComponentModel.DataAnnotations;

namespace ApiUser.Dtos
{

    public class UserUpdateDto
    {

        public UserUpdateDto(string name, string surname, int age){
            this.Age = age;
            this.Name = name;
            this.Surname = surname;
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