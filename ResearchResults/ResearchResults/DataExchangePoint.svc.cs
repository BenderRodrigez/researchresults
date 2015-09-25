using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using ResearchResults.Models;

namespace ResearchResults
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DataExchangePoint" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы DataExchangePoint.svc или DataExchangePoint.svc.cs в обозревателе решений и начните отладку.
    public class DataExchangePoint : IDataExchangePoint
    {
        public string SaveData(double energyMeasure, string trainDictorName, string testDictorName, bool isSOM)
        {
            try
            {
                var con = ConfigurationManager.AppSettings["MONGOLAB_URI"];
                var client = new MongoClient(con);
                var db = client.GetDatabase("DistortionMeasures");
                var collection = db.GetCollection<BsonDocument>("Test");

                var doc = new BsonDocument();
                doc["Energy"] = energyMeasure;
                doc["TrainDictor"] = trainDictorName;
                doc["TestDictor"] = testDictorName;
                doc["IsSOM"] = isSOM;

                collection.InsertOneAsync(doc);
                return "OK";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string GetDataCount()
        {
            try
            {
                var con = ConfigurationManager.AppSettings["MONGOLAB_URI"];
                var client = new MongoClient(con);
                var db = client.GetDatabase("DistortionMeasures");
                var collection = db.GetCollection<BsonDocument>("Test");
                var dbResp= collection.CountAsync(new BsonDocument());
                dbResp.Wait();
                return dbResp.Result.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<DataModel> GetData(int from, int limit)
        {
            var con = ConfigurationManager.AppSettings["MONGOLAB_URI"];
            var client = new MongoClient(con);
            var db = client.GetDatabase("DistortionMeasures");
            var collection = db.GetCollection<BsonDocument>("Test");
            var filter = new BsonDocument();
            var result =
                collection.Find(filter)
                    .Skip(from)
                    .Limit(limit)
                    .ToListAsync()
                    .Result.Select(
                        x =>
                            new DataModel
                            {
                                Energy = x["Energy"].ToDouble(),
                                IsSOM = x["IsSOM"].ToBoolean(),
                                TestDictorName = x["TestDictorName"].ToString(),
                                TrainDictorName = x["TrainDictorName"].ToString()
                            });
            return result.ToList();
        }
    }
}
