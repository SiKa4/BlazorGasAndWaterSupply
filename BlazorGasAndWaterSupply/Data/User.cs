using BlazorGasAndWaterSupply.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
        public string Email { get; set; }
        [BsonIgnoreIfDefault]
        public string Department { get; set; }
        [BsonIgnoreIfDefault]
        public string PhoneNumber { get; set; }
        [BsonIgnoreIfDefault]
        public string OGRN { get; set; }
        [BsonIgnoreIfDefault]
        public string INN { get; set; }
        [BsonIgnoreIfDefault]
        public string KPP { get; set; }
        [BsonIgnoreIfDefault]
        public string Adress { get; set; }
        [BsonIgnoreIfDefault]
        public string Organization { get; set; }

        public bool FilledIn { get; set; }



        public User(string name, string surName, string password, string login, string department)
        {
            Name = name;
            Surname = surName;
            Password = password;
            Login = login;
            Department = department;
            FilledIn = false;
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
