﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using BlazorContolWork.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorGasAndWaterSupply.Data
{
    [BsonKnownTypes(typeof(Project))]
    public class Project
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        public string Name { get; set; }

        public string TypeProject { get; set; }

        [BsonIgnoreIfDefault]
        public ObjectId _idCustomer;

        [BsonIgnoreIfDefault]
        public ObjectId _idDeveloper;

        [BsonIgnoreIfDefault]
        public ObjectId _idDesigner;

        [BsonIgnoreIfDefault]
        public List<DocumentDeveloper> DocumentsDeveloper { get; set; }

        [BsonIgnoreIfDefault]
        public List<DocumentDesigner> DocumentsDesigner { get; set; }

        public Project(string Name, ObjectId _idDeveloper, ObjectId _idDesigner, ObjectId _idCustomer)
        {
            this.Name = Name;
            TypeProject = MongoExamples.FindId(_idCustomer).TypeProject;
            this._idDeveloper = _idDeveloper;
            this._idDesigner = _idDesigner;
            this._idCustomer = _idCustomer;
            DocumentsDeveloper = new List<DocumentDeveloper>();
            DocumentsDesigner = new List<DocumentDesigner>();
        }
    }
    public class ListAddFiles
    {
        public string Name;
        public IBrowserFile File;

        public ListAddFiles(string name, IBrowserFile file)
        {
            Name = name;
            File = file;
        }
    }
    public class DocumentDeveloper
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        [BsonIgnoreIfDefault]
        public ObjectId _project;

        public bool IsOk { get; set; }
        public string Name { get; set; }

        public DocumentDeveloper(ObjectId project,  string name)
        {
            _project = project;
            IsOk = false;
            Name = name;
        }
    }

    public class DocumentDesigner
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public string Name { get; set; }
        public string DiameterAndLength { get; set; }

        [BsonIgnoreIfDefault]
        public string BOS { get; set; }

        public string KNS { get; set; }

        public string CostOfWork { get; set; }

        public string DevelopmentPeriod { get; set; }

        public DocumentDesigner(ObjectId id, string diameterAndLength, string bOS, string kNS, string costOfWork, string developmentPeriod)
        {
            _id = id;
            DiameterAndLength = diameterAndLength;
            BOS = bOS;
            KNS = kNS;
            CostOfWork = costOfWork;
            DevelopmentPeriod = developmentPeriod;
        }

        public DocumentDesigner(ObjectId id, string diameterAndLength, string kNS, string costOfWork, string developmentPeriod)
        {
            _id = id;
            DiameterAndLength = diameterAndLength;
            KNS = kNS;
            CostOfWork = costOfWork;
            DevelopmentPeriod = developmentPeriod;
        }
    }
}

