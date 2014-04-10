using CalculateFreight.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace CalculateFreight.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _australiaPostBaseUrl;
        private readonly string _australiaPostUrl;
        private readonly string _australiaPostKey;

        private readonly string _fastWayPostBaseUrl;
        private readonly string _fastWayPostUrl;
        private readonly string _fastWayPostKey;

        public HomeController()
        {
            _australiaPostBaseUrl = ConfigurationManager.AppSettings["AustraliaPostBaseUrl"];
            _australiaPostUrl = ConfigurationManager.AppSettings["AustraliaPostUrl"];
            _australiaPostKey = ConfigurationManager.AppSettings["AustraliaPostKey"];

            _fastWayPostBaseUrl = ConfigurationManager.AppSettings["FastWayPostBaseUrl"];
            _fastWayPostUrl = ConfigurationManager.AppSettings["FastWayPostUrl"];
            _fastWayPostKey = ConfigurationManager.AppSettings["FastWayPostKey"];
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AustraliaPost()
        {
            return View();
        }

        public ActionResult FastWayCouriers()
        {
            return View();
        }

        public ActionResult AustraliaPostResult(AustraliaPostModel model)
        {
            var client = new RestClient(_australiaPostBaseUrl);
            var request = new RestRequest(GetAustraliaPostUrl(_australiaPostUrl, model), Method.GET);

            request.AddHeader("AUTH-KEY", _australiaPostKey);
            request.RequestFormat = DataFormat.Json;

            var queryResult = client.Execute<PostResult>(request).Data;

            if (queryResult.postage_result==null)
            {
                return View(new Result {TotalCost = "Please enter a valid From postcode"});
            }

            var totalPrice = queryResult.postage_result.First().TotalCost;
            var result = new Result { TotalCost = totalPrice };
            return View(result);
        }

        public ActionResult FastWayResult(FastWayPostModel model)
        {
            var client = new RestClient(_fastWayPostBaseUrl);
            var request = new RestRequest(String.Format("{0}/{1}/{2}/{3}/{4}?api_key={5}", _fastWayPostUrl, model.RFCode, model.Suburb, model.DestPostCode, model.Weight, _fastWayPostKey), Method.GET);
            request.RequestFormat = DataFormat.Json;

            var queryResult = client.Execute<List<FastWayResult>>(request).Data.Select(x=>x.result).First().ToList();

            return View(new Result { TotalCost = queryResult[0].services.Select(x=>x.totalprice_normal).First()});
        }

        private string GetAustraliaPostUrl(string url, AustraliaPostModel model)
        {
            var queryUrl = String.Format("{6}?from_postcode={0}&to_postcode={1}&length={2}&height={3}&width={4}&weight={5}&service_code=AUS_PARCEL_REGULAR&option_code=AUS_SERVICE_OPTION_REGISTERED_POST&suboption_code=AUS_SERVICE_OPTION_EXTRA_COVER&extra_cover=3000",
                model.FromPostCode, model.ToPostCode, model.Length, model.Height, model.Width, model.Weight, url);

            return queryUrl;
        }
    }
}