using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace DataAnnotationValidatorTestWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
                    ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition()  {
                        Path = "~/Scripts/jquery-2.1.0.min.js",
                        DebugPath = "~/Scripts/jquery-2.1.0.js"
                    });
        }
    }
}