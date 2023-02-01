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
        public ClassProjectNameDevDes(string name, string departament, string projectName, ObjectId _Id, ObjectId _idProject)
        {
            Name = name;
            Departament = departament;
            ProjectName = projectName;
            _id = _Id;
            this._idProject = _idProject;
        }

        [BsonId]
        public ObjectId _id;

        [BsonId]
        public ObjectId _idProject;

        public string Name { get; set; }
        public string Departament { get; set; }
        public string ProjectName { get; set; }
    }
}
