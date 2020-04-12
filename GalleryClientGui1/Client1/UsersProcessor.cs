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
    public class UsersProcessor
    {
        public static async Task<List<UserModel>> GetAllUsers()
        {
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/users";
            using (HttpResponseMessage responseMessage = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    String jsonStr = await responseMessage.Content.ReadAsStringAsync();
                    dynamic magic = JsonConvert.DeserializeObject(jsonStr);
                    UserModel tempUser;
                    List<UserModel> users = new List<UserModel>();
                    foreach (var s in magic["users"])
                    {
                        tempUser = JsonConvert.DeserializeObject<UserModel>(s.ToString());
                        users.Add(tempUser);
                    }
                    return users;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        public static async Task<String> addUser(String name)
        {
            var values = new Dictionary<string, string>
            {{ "name", name }};

            var content = new FormUrlEncodedContent(values);
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/createUser";
            using (HttpResponseMessage responseMessage = await ApiHelper.ApiClient.PostAsync(url, content))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await responseMessage.Content.ReadAsStringAsync(); //right!
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        public static async Task<String> deleteUser(String userId)
        {
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/removeUser?id={userId}";
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

        public static async Task<String> updateUser(String userId, String newName)
        {
            var values = new Dictionary<string, string>
            {{ "name", newName }};
            var content = new FormUrlEncodedContent(values);
            String url = $"http://{ApiHelper.serverIp}:{ApiHelper.serverPort}/api/updateUser?id={userId}";

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
