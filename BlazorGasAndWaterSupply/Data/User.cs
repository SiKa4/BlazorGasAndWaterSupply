using BlazorGasAndWaterSupply.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlazorContolWork.Data
{
    [BsonKnownTypes(typeof(User))]
    public class User
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        [BsonIgnoreIfDefault]
        public ObjectId _idPhoto;

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Wrong Email.")]
        public string Email { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        public string Department { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        [RegularExpression(@"^[+][0-9][(][0-9]{3}[)][-][0-9]{3}[-][0-9]{2}[-][0-9]{2}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "Invalid OGRN.")]
        public string OGRN { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid INN.")]
        public string INN { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        [RegularExpression(@"^[0-9]{9}", ErrorMessage = "Invalid KPP.")]
        public string KPP { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        public string Adress { get; set; }

        [BsonIgnoreIfDefault]
        [Required]
        public string Organization { get; set; }

        public bool FilledIn { get; set; }
        [BsonIgnoreIfDefault]
        public string TypeProject { get; set; }

        public User(string name, string surName, string password, string login, string department)
        {
            Name = name;
            Surname = surName;
            Password = password;
            Login = login;
            Department = department;
            FilledIn = false;
        }

        public User(string name, string surName, string password, string login, string department, string typeProject)
        {
            Name = name;
            Surname = surName;
            Password = password;
            Login = login;
            Department = department;
            FilledIn = false;
            TypeProject = typeProject;
        }

        public void TopUp(string Email, string PhoneNumber)
        {
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            FilledIn = true;
        }

        public void TopUp(string Email, string PhoneNumber, string OGRN, string INN, string KPP, string Adress, string Organization)
        {
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.OGRN = OGRN;
            this.INN = INN;
            this.KPP = KPP;
            this.Adress = Adress;
            this.Organization = Organization;
            FilledIn = true;
        }
    }
}
