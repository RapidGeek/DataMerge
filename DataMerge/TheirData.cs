using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMerge
{
    public interface TheirData
    {
        string IndexString { get; set; }
        DateTime DateStamp { get; set; }
    }

    public abstract class TheirDataDecorator : TheirData
    {
        public string IndexString { get; set; }
        public DateTime DateStamp { get; set; }
    }

    public class TheirClickAndImpressionData : TheirDataDecorator
    {
        public int Clicks { get; set; }
        public int Impressions { get; set; }
    }
}
