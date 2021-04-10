using MicroserviceTraining.Framework.Data;

namespace Player.Data.Entities
{
    public class Player : Entity
    {
        private Player()
        {

        }

        public Player(string name, string surname, string phoneNumber, int rating)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Rating = rating;
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public int Rating { get; set; }
    }
}
