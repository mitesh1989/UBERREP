using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace UBERREP.Admin.WebLogic
{
    public abstract class Message : System.Web.UI.WebControls.WebControl
    {
        private string caption;
        private string text;

        public Message(string caption, string text)
            : base(HtmlTextWriterTag.Div)
        {
            this.caption = caption;
            this.text = text;

            base.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            base.BorderWidth = 1;
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.Controls.Add(new LiteralControl(this.caption + "<br/>" + this.text));
            base.OnPreRender(e);
        }
    }

    public class ErrorMessage : Message
    {
        public ErrorMessage(string caption, string text)
            : base(caption, text)
        {
            base.CssClass = "alert_error";
        }
    }
    public class WarningMessage : Message
    {
        public WarningMessage(string caption, string text)
            : base(caption, text)
        {
            base.CssClass = "alert_warning";
        }

    }
    public class InfoMessage : Message
    {
        public InfoMessage(string caption, string text)
            : base(caption, text)
        {
            base.CssClass = "alert_success";
        }
    }
}