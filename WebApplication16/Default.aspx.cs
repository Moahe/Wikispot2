using System;
using System.Web.UI;
using System.Net;
using Newtonsoft.Json;
using WebApplication16.Models;
using System.Collections.Generic;
using QuickType;
using SpotifyAPI;
using System.Text;
using System.Data.SqlClient;

namespace WebApplication16
{
    public partial class Default : Page
    {
        string token = "BQDS4T8q76OeuLJyCnoluAD3o6F0jiATIILKoyMx1JJWa3IFyfF9GprM2-2SX0iRyADQmBivizi99NLqSNvzUkJYOf8GLM53nr0Cm6_Ly7Zh29VqqKHVn5JVsxaHSr1i2R3cfcwqAIpICg";
        
        public String ConnToSpotApi(String url)
        {
            


            WebClient client = new WebClient();

            client.Headers.Add("Accept: application/json");
            client.Headers.Add("Content-Type: application/json");
            client.Headers.Add("Authorization: Bearer " + token);
            string jsonstring = "";
            try
            {
                jsonstring = client.DownloadString(url);
            }
            catch (Exception e)
            {

            }
            return jsonstring;

        }
        public void SearchForArtistTopSongs(String artistID)
        {
            String url = @"https://api.spotify.com/v1/artists/" + artistID + "/top-tracks?country=SE";
            String jsonstring = ConnToSpotApi(url);
            //var topSongs = TopSong.FromJson(jsonstring);
            QuickType.RootObject topSong = new QuickType.RootObject();

            try
            {
                topSong = JsonConvert.DeserializeObject<QuickType.RootObject>(jsonstring);
                foreach (Track gen in topSong.tracks)
                {
                    if (gen.album.name.Contains("Ã¶"))
                    {
                        gen.album.name.Replace("Ã¶", "o");
                    }
                    LblArtistTopSongs.Text += gen.name + "<br />";
                    LblArtistAlbums.Text += gen.album.name + ", ";
                    displayingDiv.Visible = true;
                }
                //|| gen.album.name.Contains("Ã¥") || gen.album.name.Contains("Ã¤")
            }
            catch (Exception e) {
                Label9.Text = "Artist not found";
                displayingDiv.Visible = false;

            }

        }


        public Item SearchForArtistID(String artistName)
        {
            
            LblArtistGenre.Text = "";
            LblArtistName.Text = "";
            string url = @"https://api.spotify.com/v1/search?q=" + artistName + "&type=artist";
            String jsonstring = ConnToSpotApi(url);
            SpotArtist art = new SpotArtist();
            Item item = new Item();

            try
            {
                art = JsonConvert.DeserializeObject<SpotArtist>(jsonstring);
                item = art.Artists.Items[0];
                Image1.ImageUrl = item.Images[0].Url;
                LblArtistName.Text += "" + item.Name;
                LblArtistFollowers.Text = "" + item.Followers.Total;
                LblArtistID.Text = "" + item.Id;
                foreach (string gen in item.Genres)
                {
                    LblArtistGenre.Text += gen + ", ";
                }
                SearchForArtistTopSongs(item.Id);
                displayingDiv.Visible = true;

            }
            catch (Exception e)
            {
                Label9.Text = "Artist not found";
                displayingDiv.Visible = false;

            }
            connectToDB();
            return item;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            getClientCredentialsAuthToken();
            displayingDiv.Visible = false;
            /*
            https://beta.developer.spotify.com/console/get-artist/?id=12Chz98pHFMPJEknJQMWvI
            https://beta.developer.spotify.com/dashboard/applications/6321e8e2733649a3b8117d9da53936d3
        
            
            String clientID = "6321e8e2733649a3b8117d9da53936d3";
            String clientIDsecret = "aa90b46e1a32454386a98c3823875b03";
            string token = "BQDFqN8GBf6Dm9qXuRvdN1aYVpH4qL7GWUMJRA45Ktg3gA7QF74Xip1ViEw2JFFP28HcCgHsXItiSjsgtS0dl3vkAM_wagS_ys";
            //string url = @"https://accounts.spotify.com/authorize?client_id=6321e8e2733649a3b8117d9da53936d3&redirect_uri=http:%%http://localhost:59233/About&response_type=token&state=123";
            string url = @"https://accounts.spotify.com/api/token?grant_type=refresh_token&refresh_token="+token;

            WebClient client = new WebClient();
            client.Headers.Add("Authorization: Basic "+clientID+":"+clientIDsecret+"");
            //client.Headers.Add("Authorization: Bearer " + token + "");



            //hämta och skriv ut jsonsträngen
            string jsonstring = client.DownloadString(url);
            */


            /*
            
            */

        }
        public string StringSpaceFormat(string s)
        {
            string ns = s;
            if (ns.Contains(" "))
            {
                ns.Replace(" ", "%20");
            }
            return ns;
        }

        public void getWikiPage()
        {

            
            displayingDiv.Visible = true;
            //string url = @"https://accounts.spotify.com/authorize?client_id=6321e8e2733649a3b8117d9da53936d3&redirect_uri=http:%%webapplication1520180522084947.azurewebsites.net%About&response_type=token&state=123";
            string url = @"https://en.wikipedia.org/api/rest_v1/page/html/"+TextBox1.Text;
            //string url = @"https://en.wikipedia.org/w/api.php?action=query&titles=avicii&prop=revisions&rvprop=content&format=json&formatversion=2";
            WebClient client = new WebClient();
            try
            {
                        string jsonstring = client.DownloadString(url);
                        wikispotsearch.Visible = false;
                        Wikidiv.InnerHtml = jsonstring;
            }
            catch
            {
                Label7.Text = "Wikipedia page not found. :(";
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LblArtistName.Text = "";
            LblArtistGenre.Text = "";
            LblArtistFollowers.Text = "";
            LblArtistID.Text = "";
            LblArtistTopSongs.Text = "";
            Label6.Text = "";
            Label7.Text = "";
            LblArtistAlbums.Text = "";
            SearchForArtistID(TextBox1.Text);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            getWikiPage();
        }

        public void getClientCredentialsAuthToken()
        {
            var spotifyClient = "secret";
            var spotifySecret = "secret";

            var webClient = new WebClient();

            var postparams = new System.Collections.Specialized.NameValueCollection();
            postparams.Add("grant_type", "client_credentials");

            var authHeader = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes($"{spotifyClient}:{spotifySecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);

            var tokenResponse = webClient.UploadValues("https://accounts.spotify.com/api/token", postparams);

            var textResponse = Encoding.UTF8.GetString(tokenResponse);
            SpotAu au = new SpotAu();

            au = JsonConvert.DeserializeObject<SpotAu>(textResponse);
            token = au.access_token;

        }
        public void connectToDB() {
            string artistInfo = "'"+LblArtistName.Text+"', "+0+", "+0+", " +0+ ", "+LblArtistFollowers.Text+", "+0 +", "+0;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "moaherm.database.windows.net";
                builder.UserID = "moaherm";
                builder.Password = "inputyourpassword";
                builder.InitialCatalog = "SpotifyArtist";
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO dbo.Tbl_Artist(art_Name,art_Genre,art_SpotId,art_ImgUrl,art_Followers,art_Albums,art_TopSongs)VALUES("+artistInfo+")");
                    String sql = sb.ToString();
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                connection.Close();
                reader.Close();

        }

    }
}
