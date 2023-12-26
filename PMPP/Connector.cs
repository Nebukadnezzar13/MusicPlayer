using RestSharp;
using System.Collections.Specialized;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SpotifyAPI.Web.Http;

namespace PMPP
{
    internal class connector
    {
        Settings sett = Settings.Instance;
        string code = string.Empty;
        string spotRedirectUrl = "https://open.spotify.com/intl-de?";
        string spotAuthorizeUrl = "https://accounts.spotify.com/authorize?client_id={0}&response_type=code&redirect_uri={1}&scope=user-read-private%20user-read-email";

        public string SpotRedirectUrl { get => spotRedirectUrl; set => spotRedirectUrl = value; }

       public void connectToSpot()
        {
              

            if (!(establishConn() == System.Net.HttpStatusCode.OK.ToString()))
            {
                string spotRedir = WebUtility.UrlEncode(spotRedirectUrl);
                string spotclientID = sett.SpotClientId;
               string website= string.Format(spotAuthorizeUrl, spotclientID, spotRedir);
                Process.Start(new ProcessStartInfo
                {
                    FileName = website,
                    UseShellExecute = true
                });
                string code = Console.ReadLine();
            }
            else
            {
                int i;
                
            }
        }

       string  establishConn()
        {
            var client = new RestClient("https://accounts.spotify.com/api/token");
            var request = new RestRequest(Method.Post.ToString());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", sett.SpotClientId);
            request.AddParameter("client_secret", sett.SpotClientSecret);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", spotRedirectUrl);

            RestResponse response = client.Execute(request);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);

            // Hier hast du den Access Token
            string accessToken = responseData.access_token;
            return accessToken;
        }

    }
}
