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
using System.Net.Http;
using System.Text.Json;
using SpotifyAPI.Web;
using Newtonsoft.Json;
using System.DirectoryServices;
using System.IO;
using System.Threading.Channels;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;

namespace PMPP
{
    internal class connector
    {
        Settings sett = Settings.Instance;
     
        // spotify
        string code = string.Empty;
        string spotTokenURL = "https://accounts.spotify.com/api/token";
        string spotRedirectUrl = "https://open.spotify.com/intl-de?";
        string spotAuthorizeUrl = "https://accounts.spotify.com/authorize?client_id={0}&response_type=code&redirect_uri={1}&scope=user-read-private%20user-read-email";
        string spotPlayerPlayUrl = "https://api.spotify.com/v1/me/player/play";
        HttpClient httpClient = null;
        private string spotToken =null;

        // youtube
       
        string myYtChannel = "UC-TR_ysJP7PqYZ5rFjPYnGw";
        public string SpotRedirectUrl { get => spotRedirectUrl; set => spotRedirectUrl = value; }

       public async void connectToSpot()
        {

             spotToken = await establishConn();
            if (!string.IsNullOrEmpty(spotToken))
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

        async Task<string> establishConn()
        {
            httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, spotToken);
           
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",Convert.ToBase64String(Encoding.UTF8.GetBytes($"{sett.SpotClientId}:{sett.SpotClientSecret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"grant_type","client_credentials" }
            });

            var response = await httpClient.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await System.Text.Json.JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

        
            return authResult.access_token;
        }

        public async void connectYT()
        {
            getSongsFromYT();
        }
        private async Task getSongsFromYT()
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;

            List<SearchResult> res = new List<SearchResult>();

            string nextpagetoken = " ";

            var _youtubeService = new YouTubeService(new BaseClientService.Initializer() { ApiKey = sett.YoutubeApiKey });

            //   var playlists = new Playlist();



            while (nextpagetoken != null)
            {
                var searchListRequest = _youtubeService.Search.List("id,snippet");
                searchListRequest.MaxResults = 50;
                searchListRequest.ChannelId = myYtChannel;
                searchListRequest.PageToken = nextpagetoken;
                searchListRequest.Type = "playlist";

                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = searchListRequest.Execute();



                // Process  the video responses 
                res.AddRange(searchListResponse.Items);

                nextpagetoken = searchListResponse.NextPageToken;

            }
            //Console.WriteLine($"{res?.pageInfo?.totalResults} videos in playlist.");

            Console.WriteLine("-----------------------------------------------------\n");

            string docFolder = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents");

            if (!Directory.Exists(docFolder + "/YTPlayList"))
            {
                Directory.CreateDirectory(docFolder + "/YTPlayList");
            }

            List<string> videosNotFound = new List<string>();

            var i = 0;
            foreach (var result in res)
            {

                //  var resultj = JsonConvert.DeserializeObject(result.Snippet.ToString());
                //foreach (var video in resultj)
                Playlist newPl = new Playlist();
               // newPl.id = result .Id.PlaylistId;
               // newPl.title = result.Snippet.Title;
              
                //  Console.WriteLine("------ PLAYLIST " + newPl.Name + "----------------");

                int testCnt = 0;

                //get all videos 
            //    var url = string.Format($"https://www.googleapis.com/youtube/v3/playlistItems?part=contentDetails,id,snippet,status&playlistId={newPl.Id}&maxResults=50&key={apiKey}");

                //dynamic playListVideo = await GetResult(url, newPl.Id);

                //List<string> ytResults = new List<string>();
                //foreach (var video in playListVideo)
                //{
                //    ytResults.Add(video);
                //}

               
            }

        }


        static async Task<dynamic> GetYTPlaylistResult(string url, string playlistId, string apiKey)
        {
            dynamic result = null;

            List<string> resultStrings = new List<string>();

            try
            {
                var client = new HttpClient();

                string nextPageToken = "firstRun";

                while (nextPageToken != null)
                {
                    var json = await client.GetStringAsync(url);

                    result = JsonConvert.DeserializeObject(json);
                    try
                    {
                       
                        nextPageToken = result["nextPageToken"].Value;
                        url = string.Format($"https://www.googleapis.com/youtube/v3/playlistItems?part=contentDetails,id,snippet,status&playlistId={playlistId}&pageToken={nextPageToken}&maxResults=50&key={apiKey}");

                    }
                    catch (Exception)
                    {
                        nextPageToken = null;

                    }

                    foreach (var video in result.items)
                    {
                        string tmp = video.snippet.title;
                        resultStrings.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Console.WriteLine(ex.Message);
            }

            return resultStrings;
        }

    }
}
