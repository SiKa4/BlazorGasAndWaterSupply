using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorGasAndWaterSupply.Data
{
    public class ChatsClassBase
    {
        [BsonId]
        public ObjectId _idCustomer { get; set; }

        [BsonId]
        public ObjectId _idProject { get; set; }
        [BsonId]
        public ObjectId _idReceiver { get; set; }
        public List<Messages> _messages { get; set; }
    }
}