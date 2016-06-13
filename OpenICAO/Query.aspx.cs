using System;
using System.Net;
using OpenWeather;

namespace OpenICAO
{
    public partial class Query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["icao"] != null)
                {
                    var url =
                        $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={Request.QueryString["icao"]}&hoursBeforeNow=24";
                    Response.Write(new WebClient().DownloadString(url));
                }
                else if ((Request.QueryString["lat"] != null) && (Request.QueryString["lngt"] != null))
                {

                    var lat = Convert.ToDouble(Request.QueryString["lat"]);
                    var lngt = Convert.ToDouble(Request.QueryString["lngt"]);
                    var station = MetarStationLookup.Instance.Lookup(lat, lngt);

                    var url =
                       $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={station.GetStationInfo.ICAO}&hoursBeforeNow=24";
                    Response.Write(new WebClient().DownloadString(url));
                }
                else
                {
                    throw new Exception("Invalid parameters");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}