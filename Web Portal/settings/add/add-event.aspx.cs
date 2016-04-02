using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class settings_add_add_event : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        eventSubject.Attributes.Add("placeholder", "Event name");
        eventLocation.Attributes.Add("placeholder", "Location of event");
        eventInstructor.Attributes.Add("placeholder", "Instructor name");
        eventPrice.Attributes.Add("placeholder", "Event price (without currency symbols)");
        eventDecription.Attributes.Add("placeholder", "Description of event");
        eventFlier.Attributes.Add("placeholder", "Flier URL location");
    }
}