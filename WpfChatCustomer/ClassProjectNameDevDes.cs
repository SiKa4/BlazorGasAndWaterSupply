using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChatCustomer
{
    class ClassProjectNameDevDes
    {
        public ClassProjectNameDevDes(string name, string departament, string projectName, ObjectId _Id)
        {
            Name = name;
            Departament = departament;
            ProjectName = projectName;
            _id = _Id;
        }

        [BsonId]
        ObjectId _id;

        public string Name { get; set; }
        public string Departament { get; set; }
        public string ProjectName { get; set; }
    }
}
