using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace diplom
{
    public class SaveUrlController
    {
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
                    Console.WriteLine($"→ New request: {request.HttpMethod} {request.Url}");

                    using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                    {
                        string body = await reader.ReadToEndAsync();
                        Console.WriteLine("Отримано POST: " + body);
                        JsonProcessing.SaveUrlToJson(body); // або зроби це теж async, якщо треба
                    }

                    var response = context.Response;
                    // Дозволяємо всім origin’ам
                    response.AddHeader("Access-Control-Allow-Origin", "*");
                    // Preflight  
                    response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
                    // Заголовки, які ми використовуємо
                    response.AddHeader("Access-Control-Allow-Headers", "Content-Type");

                    // Якщо це preflight-запит
                    if (request.HttpMethod == "OPTIONS")
                    {
                        response.StatusCode = 200;
                        response.OutputStream.Close();
                        continue;
                    }
                    string responseString = "OK";
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                }
            }
        }
    }
}



