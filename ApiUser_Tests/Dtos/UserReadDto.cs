using System;

namespace ApiUser.Dtos
{

    public class UserReadDto
    {

        public UserReadDto(int id, string name, string surname, int age, DateTime creationDate)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
            this.CreationDate = creationDate;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
    }
}