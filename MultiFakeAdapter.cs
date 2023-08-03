using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Microsoft.IdentityServer.Web.Authentication.External;
using Claim = System.Security.Claims.Claim;

namespace MultiFakeAuth
{
    class Adapter : IAuthenticationAdapter
    {
        static bool ValidateProofData(IProofData proofData, IAuthenticationContext authContext)
        {
            if (proofData == null || proofData.Properties == null || !proofData.Properties.ContainsKey("ChallengeQuestionAnswer"))
            {
                throw new ExternalAuthenticationException("Error - no answer found", authContext);
            }

            if ((string)proofData.Properties["ChallengeQuestionAnswer"] == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IAuthenticationAdapterMetadata Metadata
        {
            get { return new Metadata(); }
        }

        public IAdapterPresentation BeginAuthentication(Claim identityClaim, HttpListenerRequest request, IAuthenticationContext authContext)
        {
            return new PresentationForm();
        }

        public bool IsAvailableForUser(Claim identityClaim, IAuthenticationContext authContext)
        {
            return true; //its all available for now
        }

        public void OnAuthenticationPipelineLoad(IAuthenticationMethodConfigData configData)
        {
            //this is where AD FS passes us the config data, if such data was supplied at registration of the adapter
        }

        public void OnAuthenticationPipelineUnload()
        {

        }

        public IAdapterPresentation OnError(HttpListenerRequest request, ExternalAuthenticationException ex)
        {
            return new PresentationForm();
        }

        public IAdapterPresentation TryEndAuthentication(IAuthenticationContext authContext, IProofData proofData, HttpListenerRequest request, out Claim[] outgoingClaims)
        {
            outgoingClaims = new Claim[0];
            if (ValidateProofData(proofData, authContext))
            {
                //authn complete - return authn method
                outgoingClaims = new[]
                {
            // Return the required authentication method claim, indicating the particulate authentication method used.
            new Claim( "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/hardwaretoken" )
        };
                return null;
            }
            else
            {
                //authentication not complete - return new instance of IAdapterPresentationForm derived class
                return new PresentationForm();
            }
        }

    }
}
