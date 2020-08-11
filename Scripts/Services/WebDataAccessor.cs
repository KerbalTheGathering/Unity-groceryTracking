using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using DefaultNamespace;
using UnityEngine;
using Newtonsoft.Json;

namespace rtome.Scripts.Services
{
    public static class WebDataAccessor
    {
        public static string baseUrl = "http://192.168.1.7:4000";
        
        public static string GetPantryInventory()
        {
            var request = WebRequest.Create(baseUrl + "/api/pantry/");
            request.Credentials = CredentialCache.DefaultCredentials;
            var response = (HttpWebResponse) request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var resFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return resFromServer;
        }

        public static PantryItem[] GetAllPantryItems()
        {
            using (var client = new HttpClient())
            {
                var res
                    = client.GetAsync(baseUrl + "/api/pantry")
                        .Result;
                var readString = res.Content.ReadAsStringAsync().Result;
                return JsonUtility.FromJson<PantryItem[]>(readString);
            }
        }

        public static void PostPantryInventoryItem(PantryItem item)
        {
            var req = JsonConvert.SerializeObject(item);
            using (var client = new HttpClient())
            {
                var res
                    = client.PostAsync(baseUrl + "/api/pantry/add"
                        , new StringContent(req
                            , Encoding.UTF8
                            , "application/json")
                    ).Result;
            }
        }

        public static void UpdatePantryInventoryItem(PantryItem item)
        {
            var req = JsonConvert.SerializeObject(item);
            using (var client = new HttpClient())
            {
                var res
                    = client.PostAsync(baseUrl + "/api/pantry/update"
                        , new StringContent(req
                            , Encoding.UTF8
                            , "application/json")
                    ).Result;
            }
        }

        public static void DeletePantryInventoryItem(PantryItem item)
        {
            var req = JsonConvert.SerializeObject(item);
            using (var client = new HttpClient())
            {
                var res
                    = client.PostAsync(baseUrl + "/api/pantry/delete"
                        , new StringContent(req
                            , Encoding.UTF8
                            , "application/json")
                    ).Result;
            }
        }
        
        public static GroceryItem[] GetGroceryItems()
        {
            using (var client = new HttpClient())
            {
                var res = client.GetAsync
                        (baseUrl + "/api/groceryItems")
                    .Result;
                var readString = res.Content.ReadAsStringAsync().Result;
                var objects = JsonConvert
                    .DeserializeObject<GroceryItem[]>(readString);
                return objects;
            }
        }
        
        public static void PostGroceryItem(GroceryItem item)
        {
            var req = JsonConvert.SerializeObject(item);
            using (var client = new HttpClient())
            {
                var res
                    = client.PostAsync(baseUrl + "/api/grocery/add"
                        , new StringContent(req
                            , Encoding.UTF8
                            , "application/json")
                    ).Result;
            }
        }

        public static void RemoveGroceryItem(GroceryItem item)
        {
            var req = JsonConvert.SerializeObject(item);
            using (var client = new HttpClient())
            {
                var res
                    = client.PostAsync(baseUrl + "/api/grocery/delete"
                        , new StringContent(req
                            , Encoding.UTF8
                            , "application/json")
                    ).Result;
            }
        }
    }
}





























