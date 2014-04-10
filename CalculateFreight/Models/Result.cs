using System.Collections.Generic;

namespace CalculateFreight.Models
{
    //Australia Post
    public class PostResult
    {
        public List<Result> postage_result { get; set; }
    }

    public class Result
    {
        public string TotalCost { get; set; }
    }

    //Fast Way Post
    public class FastWayResult
    {
        public List<Services> result { get; set; }
    }

    public class Services
    {
        public List<TotalPrice> services { get; set; }
    }

    public class TotalPrice
    {
        public string totalprice_normal { get; set; }
    }
}