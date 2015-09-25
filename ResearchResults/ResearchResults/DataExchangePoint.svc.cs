using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ResearchResults
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DataExchangePoint" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы DataExchangePoint.svc или DataExchangePoint.svc.cs в обозревателе решений и начните отладку.
    public class DataExchangePoint : IDataExchangePoint
    {
        public void SaveData(double energyMeasure, string trainDictorName, string testDictorName, bool isSOM)
        {
            var con = ConfigurationManager.ConnectionStrings["MONGOLAB_URI"].ConnectionString;
            if (string.IsNullOrWhiteSpace(con))
            {
                con = ConfigurationManager.AppSettings["MONGOLAB_URI"];
            }
            var client = new MongoClient(con);
            var db = client.GetDatabase("DistortionMeasures");
            var collection = db.GetCollection<BsonDocument>("Test");

            var doc = new BsonDocument();
            doc["Energy"] = energyMeasure;
            doc["TrainDictor"] = trainDictorName;
            doc["TestDictor"] = testDictorName;
            doc["IsSOM"] = isSOM;

            collection.InsertOneAsync(doc);
        }
    }
}
