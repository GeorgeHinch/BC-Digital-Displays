using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class display_settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void settingsRadioSingle_CheckedChanged(object sender, EventArgs e)
    {
        settingsMessageOneline.Visible = true;
        settingsMessageMultiline.Visible = false;
    }

    protected void settingsRadioMulti_CheckedChanged(object sender, EventArgs e)
    {
        settingsMessageOneline.Visible = false;
        settingsMessageMultiline.Visible = true;
    }

    protected void settingsMessageActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox sCheck = (CheckBox)sender;
        if(sCheck.Checked == true)
        {
            messagePanel.Visible = true;
        }
        else { messagePanel.Visible = false; }
    }

    protected void chkShowHideDiv_CheckedChanged(object sender, EventArgs e)
    {
        myDiv.Visible = settingsMessageActive.Checked ? false : true;
    }
}