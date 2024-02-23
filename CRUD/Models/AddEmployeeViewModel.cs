using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class AddEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Mobile { get; set; }

       
        public string EmailId { get; set; }

        public string Address { get; set; }

        
        public DateTime DateOfBirth { get; set; }

        public DateTime City { get; set; }
    }
}
