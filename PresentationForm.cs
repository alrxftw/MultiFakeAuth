using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MultiFakeAuth
{
    public class PresentationForm : IAdapterPresentationForm
    {
        /// Returns the HTML Form fragment that contains the adapter user interface. This data will be included in the web page that is presented
        /// to the cient.
        public string GetFormHtml(int lcid)
        {
            string htmlTemplate = Reources.Resources.FormArea;
            return htmlTemplate;
        }

        /// Return any external resources, ie references to libraries etc., that should be included in
        /// the HEAD section of the presentation form html.
        public string GetFormPreRenderHtml(int lcid)
        {
            return null;
        }

        //returns the title string for the web page which presents the HTML form content to the end user
        public string GetPageTitle(int lcid)
        {
            return "MultiFakeAuth Adapter";
        }
    }
}
