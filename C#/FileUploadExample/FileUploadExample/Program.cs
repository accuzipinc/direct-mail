using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadExample
{
    class Program
    {
        static void Main(string[] args) {
            string targetUrl = "https://cloud2.iaccutrace.com/ws_360_webapps/v2_0/uploadProcess.jsp?manual_submit=false";
            byte[] fileBytes = File.ReadAllBytes("C:\\path\\to\\your\\csv\\file.csv");
            string yourFileName = "file.csv";
            string apiKey = "YOUR_API_KEY";
            string callbackUrl = "YOUR_CALLBACK_URL";

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent("json"), "backOfficeOption");
            form.Add(new StringContent(apiKey), "apiKey");
            form.Add(new StringContent(callbackUrl), "callbackURL");
            form.Add(new StringContent(""), "guid");
            form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "file", yourFileName);
            HttpResponseMessage response = httpClient.PostAsync(targetUrl, form).Result;
            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string responseContent = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseContent);
            Console.Read();
        }
    }
}
