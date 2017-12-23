using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMerge
{
    public interface OurData
    {
        int ID { get; set; }
    }

    public abstract class OurDataDecorator : OurData
    {
        public int ID { get; set; }
    }

    public class OurNameAndTrackingIdData : OurDataDecorator
    {
        public string Name { get; set; }
        public string TrackingID { get; set; }
    }
}
