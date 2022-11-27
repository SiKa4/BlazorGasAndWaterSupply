using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Bson;

namespace BlazorContolWork.Data
{
    static public class FileSystemService
    {
        static public async Task UploadFileToDb(string path, string fileName)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("DBUser");
            var gridFS = new GridFSBucket(database);

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                gridFS.UploadFromStream(fileName, fs);
            }
            File.Delete(path);
        }

        public static ObjectId SearchByNameId(string fileName)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("DBUser");
            var gridFS = new GridFSBucket(database);

            var filter = Builders<GridFSFileInfo>.Filter.Eq<string>(info => info.Filename, fileName);
            var fileInfos = gridFS.Find(filter).FirstOrDefault();
            return fileInfos.Id;
        }
    }
}
