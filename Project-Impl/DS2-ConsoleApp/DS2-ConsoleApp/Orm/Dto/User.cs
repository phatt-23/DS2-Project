using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dto
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? AboutMe { get; set; }
        public long? ProfilePictureId { get; set; }
        public DateTime? LastLogin { get; set; }
        public UserStatus Status { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return "UserId: " + UserId + ", Username: " + Username + ", FirstName: " + FirstName + ", LastName: " + LastName +
                   ", Email: " + Email + ", RegistrationDate: " + RegistrationDate + ", AboutMe: " + AboutMe +
                   ", ProfilePictureId: " + ProfilePictureId + ", LastLogin: " + LastLogin +
                   ", Status: " + Status.ToReprString() + ", IsDeleted: " + IsDeleted;
        }
    }
}
