using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorGasAndWaterSupply.Data
{
    public class Project
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        public string TypeProject { get; set; }

        [BsonIgnoreIfDefault]
        public ObjectId _idDeveloper;

        [BsonIgnoreIfDefault]
        public ObjectId _idDesigner;

        [BsonIgnoreIfDefault]
        public List<DocumentsDeveloper> DocumentsDevelopers { get; set; }

        [BsonIgnoreIfDefault]
        public List<DocumentsDesigner> DocumentsDesigners { get; set; }

        public Project(string TypeProject)
        {
            this.TypeProject = TypeProject;
        }

    }

    public class DocumentsDeveloper
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        [BsonIgnoreIfDefault]
        public ObjectId _project;

    }

    public class DocumentsDesigner
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        public string DiameterAndLength { get; set; }

        [BsonIgnoreIfDefault]
        public string BOS { get; set; }

        public string KNS { get; set; }

        public string CostOfWork { get; set; }

        public string DevelopmentPeriod { get; set; }

        public DocumentsDesigner(ObjectId id, string diameterAndLength, string bOS, string kNS, string costOfWork, string developmentPeriod)
        {
            _id = id;
            DiameterAndLength = diameterAndLength;
            BOS = bOS;
            KNS = kNS;
            CostOfWork = costOfWork;
            DevelopmentPeriod = developmentPeriod;
        }

        public DocumentsDesigner(ObjectId id, string diameterAndLength, string kNS, string costOfWork, string developmentPeriod)
        {
            _id = id;
            DiameterAndLength = diameterAndLength;
            KNS = kNS;
            CostOfWork = costOfWork;
            DevelopmentPeriod = developmentPeriod;
        }
    }
}

