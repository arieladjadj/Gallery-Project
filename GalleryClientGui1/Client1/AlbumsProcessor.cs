using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class AlbumsProcessor
    {
        public static async Task<List<AlbumModel>> GetAllAlbumsOfUser(String userId)
        {
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/albumsOfUser?userId={userId}";
            using (HttpResponseMessage responseMessage = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    String jsonStr = await responseMessage.Content.ReadAsStringAsync();
                    dynamic magic = JsonConvert.DeserializeObject(jsonStr);
                    AlbumModel tempAlbum;
                    List<AlbumModel> albums = new List<AlbumModel>();
                    foreach (var s in magic["albums"])
                    {
                        tempAlbum = JsonConvert.DeserializeObject<AlbumModel>(s.ToString());
                        tempAlbum.setText(tempAlbum.getName() + "\n" + tempAlbum.getCreationDate());
                        albums.Add(tempAlbum);
                    }
                    return albums;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        public static async Task<String> addAlbum(String albumName, String creationDate, String userId)
        {
            var values = new Dictionary<string, string>
            {
            { "name", albumName },
            { "creationDate", creationDate },
                {"userId" , userId }
            };

            var content = new FormUrlEncodedContent(values);
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/createAlbum";
            using (HttpResponseMessage responseMessage = await ApiHelper.ApiClient.PostAsync(url, content))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await responseMessage.Content.ReadAsStringAsync(); //right!
                    // return responseMessage.Content.ToString();
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        public static async Task<String> deleteAlbum(String albumId)
        {
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/removeAlbum?id={albumId}";
            WebRequest request = WebRequest.Create(url);
            request.Method = "DELETE";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            if (response.StatusCode == HttpStatusCode.OK)
            {
                var encoding = Encoding.GetEncoding(response.CharacterSet);

                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                    return reader.ReadToEnd();
            }
            else
            {
                throw new Exception("Did not delete item");
            }
        }

        public static async Task<String> updateAlbum(String albumId, String newName, String creationDate, String userId)
        {
            var values = new Dictionary<string, string>
            {{ "name", newName },
                {"creationDate", creationDate},
                { "userId", userId} };
            var content = new FormUrlEncodedContent(values);
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/updateAlbum?id={albumId}";

            using (HttpResponseMessage responseMessage = await ApiHelper.ApiClient.PostAsync(url, content))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
    
    }
}
