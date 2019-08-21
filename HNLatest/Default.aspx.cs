using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HNTest
{
    public partial class _Default : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {

            var hn = new HN();
            var items = await hn.Newest();
            ListView1.DataSource = items;
            ListView1.DataBind();
        }

    }
}