using System;
using OpenWeather;

namespace OpenICAO
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["lat"] != null) && (Request.QueryString["lngt"] != null))
                {
                    var lat = Convert.ToDouble(Request.QueryString["lat"]);
                    var lngt = Convert.ToDouble(Request.QueryString["lngt"]);

                    var station = MetarStationLookup.Instance.Lookup(lat, lngt);

                    Response.Write(station.GetStationInfo.ICAO);
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