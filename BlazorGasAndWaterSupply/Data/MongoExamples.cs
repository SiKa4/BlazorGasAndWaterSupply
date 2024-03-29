﻿using BlazorContolWork.Data;
using BlazorGasAndWaterSupply.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorContolWork.Data
{
    public static class MongoExamples
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase database = client.GetDatabase("DBUser");
        static IMongoCollection<User> collection = database.GetCollection<User>("UserReg");

        public static void AddToDB(User user)
        {
            collection.InsertOne(user);
        }

        private static void UpdateBase()
        {
            collection = database.GetCollection<User>("UserReg");
        }

        public static void AddToDbProject(Project project)
        {
            IMongoCollection<Project> collectionProject = database.GetCollection<Project>("Projects");
            collectionProject.InsertOne(project);
        }

        public static void DeleteProjectInDb(Project project)
        {
            IMongoCollection<Project> collectionProject = database.GetCollection<Project>("Projects");
            collectionProject.DeleteOne(x => x._id == project._id);
        }


        public static List<Project> SearchProjectCustomer(ObjectId _id)
        {
            IMongoCollection<Project> collectionProject = database.GetCollection<Project>("Projects");
            var list = collectionProject.Find(x => x._idCustomer == _id).ToList();
            return list;
        }

        public static List<Project> SearchProjectDeveloper(ObjectId _id)
        {
            IMongoCollection<Project> collectionProject = database.GetCollection<Project>("Projects");
            var list = collectionProject.Find(x => x._idDeveloper == _id).ToList();
            return list;
        }

        public static List<Project> SearchProjectDesigner(ObjectId _id)
        {
            IMongoCollection<Project> collectionProject = database.GetCollection<Project>("Projects");
            var list = collectionProject.Find(x => x._idDesigner == _id).ToList();
            return list;
        }

        public static void ReplaceProjectById(ObjectId _id, Project project)
        {
            IMongoCollection<Project> collectionProject = database.GetCollection<Project>("Projects");
            collectionProject.ReplaceOne(x => x._id == _id, project);
        }

        public static User FindLogPass(string log, string pass)
        {
            UpdateBase();
            var one = collection.Find(x => x.Login == log && x.Password == pass).FirstOrDefault();
            return one;
        }

        public static User FindLogPassCustomer(string log, string pass)
        {
            UpdateBase();
            var one = collection.Find(x => x.Login == log && x.Password == pass && x.Department == "Customer").FirstOrDefault();
            return one;
        }

        public static List<User> FindUserDepartament(string departament)
        {
            UpdateBase();
            var one = collection.Find(x => x.Department == departament).ToList();
            return one;
        }

        public static User FindLog(string log)
        {
            UpdateBase();
            var one = collection.Find(x => x.Login == log).FirstOrDefault();
            return one;
        }

        public static User FindId(ObjectId _id)
        {
            UpdateBase();
            var one = collection.Find(x => x._id == _id).FirstOrDefault();
            return one;
        }


        public static List<User> FindAll()
        {
            UpdateBase();
            var list = collection.Find(x => true).ToList();
            return list;
        }

        public static void ReplaceById(ObjectId _id, User newUser)
        {
            collection.ReplaceOne(x => x._id == _id, newUser);
        }
    }
}
