using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;
using System.Text;
//using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected async void OnClick(object sender, EventArgs e)
        {
            Uri WebUri = new Uri("https://api.worldbank.org/v2/en/country/all/indicator/NY.GDP.MKTP.CD?format=json&per_page=500&date=2017&MRNEV=1", UriKind.Absolute);

            System.Net.HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(WebUri);
            WebResponse webresponse = await webrequest.GetResponseAsync();
            StreamReader webstream = new StreamReader(webresponse.GetResponseStream(), Encoding.UTF8);
            string json = webstream.ReadToEnd();
            JsonConverter[] converters = { new GDPDATAConverter() };
            List<GDPDATA> gdp = JsonConvert.DeserializeObject<List<GDPDATA>>(json, new JsonSerializerSettings()
            {
                Converters = converters
            });

            GDPGridView.DataSource = gdp[1].Indicators.Where<Indicator>(x => x.Countryiso3Code.Length > 1).OrderBy(x => x.Value).Reverse<Indicator>().Take<Indicator>(Convert.ToInt32(txtTop.Text));


            GDPGridView.DataBind();
            // process the response
        }
    }
}