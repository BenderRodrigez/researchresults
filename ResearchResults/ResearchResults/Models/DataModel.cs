using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchResults.Models
{
    public class DataModel
    {
        public double Energy { get; set; }
        public string TrainDictorName { get; set; }
        public string TestDictorName { get; set; }
        public bool IsSOM { get; set; }
    }
}