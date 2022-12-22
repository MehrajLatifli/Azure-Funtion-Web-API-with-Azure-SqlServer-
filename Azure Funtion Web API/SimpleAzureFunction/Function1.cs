using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Web.Http.Results;

namespace SimpleAzureFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

         

            using (var httpClient = new HttpClient())
            {

                string requestBody = await httpClient.GetStringAsync($"https://localhost:44345/Home/GetFile");
                var data = JsonConvert.DeserializeObject<List<dynamic>>(requestBody);

                List<string> list = new List<string>();

                string responseMessage=string.Empty;

                string name = req.Query["idFile"];

                    

                string combinedString = string.Empty;   

                if (string.IsNullOrWhiteSpace(req.Query["idFile"]))
                {


                    foreach (var item in data)
                    {

                        string fileName = item.fileName;
                        string filePath = item.filePath;
                        string fileSize = item.fileSize;


                        if (fileName.Length > 5)
                        {
                            //shorten string
                            fileName = fileName.Substring(0, 5) + "... ." + fileName.Substring(fileName.LastIndexOf('.') + 1);
                        }
                        else
                        {
                            fileName = fileName + "    ";
                        }


                        responseMessage = $"\n {fileName}\t\t {filePath}  \t  {fileSize}";
                        list.Add(responseMessage);
                    }



                     combinedString = " File name: \t \t File path: \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t  File size: \t \n" + "\n ==================================================================================================================================================================================================== \n" + string.Join(" \n \n", list.ToArray());

                }

                else
                {

                    foreach (var item in data)
                    {
                        string idFile = item.idFile.ToString();

                        string fileName = item.fileName;

                        if (idFile == name)
                        {

                            name = item.fileName;

                        }


                    }



                    
                    combinedString = name;
                }
                    return new OkObjectResult(combinedString.ToString());
            }
        }
    }
}
