using BlazorContolWork.Data;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorGasAndWaterSupply.Data
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string _idReceiver, string _idSending, string _idProject)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, _idReceiver, _idSending, _idProject);
            await MongoExamples.AddNewMessageInChat(new ObjectId(_idSending), new ObjectId(_idReceiver), new ObjectId(_idProject), message);
        }
    }

    public class ChatsClass
    {
        public ChatsClass(ObjectId idProject, ObjectId idReceiver, ObjectId idSending)
        {
            _idProject = idProject;
            _idFirstUser = idReceiver;
            _idLastUser = idSending;
            _messages = new List<Messages>();
        }
        [BsonId]
        ObjectId _id;
        public ObjectId _idProject { get; set; }
        public ObjectId _idFirstUser { get; set; }
        public ObjectId _idLastUser { get; set; }
        public List<Messages> _messages { get; set; }
    }

    public class Messages
    {
        public Messages(string nameSending, string message)
        {
            NameSending = nameSending;
            Message = message;
        }

        public string NameSending { get; set; }
        public string Message { get; set; }
    }
}
