﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBERREP.Admin
{
    public partial class Contact : WebLogic.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;



            return !string.IsNullOrEmpty(errorMessage);
        }
    }
}