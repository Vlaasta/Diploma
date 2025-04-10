using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace diplom
{
    public class SaveUrlController
    {
        //public static string LastReceivedUrl { get; private set; }


        public static async Task StartHttpServerAsync()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();
            Console.WriteLine("HTTP сервер запущено на http://localhost:5000/");

            while (true)
            {
                // Асинхронне очікування запиту
                var context = await listener.GetContextAsync();
                var request = context.Request;

                if (request.HttpMethod == "POST")
                {
                    using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                    {
                        string body = await reader.ReadToEndAsync();
                        Console.WriteLine("Отримано POST: " + body);
                        SaveUrlToJson(body); // або зроби це теж async, якщо треба
                    }

                    var response = context.Response;
                    string responseString = "OK";
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                }
            }
        }

        static void SaveUrlToJson(string jsonBody)
        {
            var data = JsonConvert.DeserializeObject<UrlData>(jsonBody);

            string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json";

            List<UrlData> urlList;
            if (File.Exists(filePath))
            {
                var existingJson = File.ReadAllText(filePath);
                urlList = JsonConvert.DeserializeObject<List<UrlData>>(existingJson) ?? new List<UrlData>();
            }
            else
            {
                urlList = new List<UrlData>();
            }

            urlList.Add(data);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(urlList, Formatting.Indented));
        }

    }
}



