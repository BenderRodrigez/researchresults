using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ResearchResults.Models;

namespace ResearchResults
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IDataExchangePoint" в коде и файле конфигурации.
    [ServiceContract]
    public interface IDataExchangePoint
    {
        [OperationContract]
        string SaveData(double energyMeasure, string trainDictorName, string testDictorName, bool isSOM);

        [OperationContract]
        long GetDataCount();

        [OperationContract]
        List<DataModel> GetData(int from, int limit);
    }
}
