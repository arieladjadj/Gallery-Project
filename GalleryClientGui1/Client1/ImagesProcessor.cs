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
    class ImagesProcessor
    {
        public static async Task<List<ImageModel>> GetAllImagesOfAlbum(String albumId)
        {
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/PicturesOfAlbum?albumId={albumId}";
            using (HttpResponseMessage responseMessage = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    String jsonStr = await responseMessage.Content.ReadAsStringAsync();
                    dynamic magic = JsonConvert.DeserializeObject(jsonStr);
                    ImageModel tempImage;
                    List<ImageModel> Images = new List<ImageModel>();
                    foreach (var s in magic["Pictures"])
                    {
                        tempImage = JsonConvert.DeserializeObject<ImageModel>(s.ToString());
                        tempImage.setText(tempImage.getName() + "\n" + tempImage.getCreationDate());
                        Images.Add(tempImage);
                    }
                    return Images;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        public static async Task<String> addImage(String ImageName, String creationDate,String path,  String albumId)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            var values = new Dictionary<string, string>
            {
            { "name", ImageName },
            { "creationDate", creationDate },
                {"path", base64ImageRepresentation },
                {"albumId" , albumId }
            };

            var content = new FormUrlEncodedContent(values);
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/createPicture";
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

        public static async Task<String> deleteImage(String ImageId)
        {
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/removePicture?id={ImageId}";
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

        public static async Task<String> updateImage(String ImageId, String newName, String creationDate, String path, String albumId)
        {
            var values = new Dictionary<string, string>
            {{ "name", newName },
             {"path", path },
             {"creationDate", creationDate},
             { "albumId", albumId} };
            var content = new FormUrlEncodedContent(values);
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/updatePicture?id={ImageId}";

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

/*
        byte[] binaryData = Convert.FromBase64String(bgImage64);

        BitmapImage bi = new BitmapImage();
        bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
        bi.EndInit();*/
    }
}
