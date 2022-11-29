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

        static public Stream DownloadToLocal(ObjectId _id)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("DBUser");
            var gridFS = new GridFSBucket(database);
            var temp = gridFS.OpenDownloadStream(_id);
            return temp;
        }

        static public string NameDownloadFile(ObjectId _id)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("DBUser");
            var gridFS = new GridFSBucket(database);

            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", _id);
            string fileNames = string.Empty;

            using (var cursor = gridFS.Find(filter))
            {
                fileNames = cursor.FirstOrDefault().Filename;
            }
            return fileNames;
        }
    }
}
