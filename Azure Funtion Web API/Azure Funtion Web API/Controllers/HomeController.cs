using Azure_Funtion_Web_API.DataAccess;
using Azure_Funtion_Web_API.Models;
using Azure_Funtion_Web_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Azure_Funtion_Web_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {

        IInfoFileDal _infoFileDal;
        public static IWebHostEnvironment _environment;

        public HomeController(IInfoFileDal infoFileDal, IWebHostEnvironment environment)
        {
            _infoFileDal = infoFileDal;
            _environment= environment;
        }


        [HttpGet("GetFile")]
        public IActionResult GetFile()
        {
            return Ok(_infoFileDal.GetList());
        }


        [HttpGet("GetFile/{id?}")]
        public IActionResult GetFile(int id)
        {
            var file = _infoFileDal.Get(f => f.IdFile == id);

            try
            {
                if (file == null)
                {

                    return StatusCode(StatusCodes.Status404NotFound);
                }

                else
                {
                    return Ok(_infoFileDal.GetList().Where(f => f.IdFile == id));
                }
            }
            catch (Exception)
            {

            }
            return BadRequest();

        }

        [HttpGet("ViewFile")]
        public async Task<IActionResult> ViewFile( string filename, string filetype)
        {
           string path =_environment.WebRootPath+ "\\Upload\\";
            var filepath=path+filename+ $".{filetype}";
            if(System.IO.File.Exists(filepath))
            {
                //byte[]b=System.IO.File.ReadAllBytes("1.jpg");

                FileStream stream = System.IO.File.OpenRead(filepath);

                string type = string.Empty;

                if (filetype == "pdf")
                {

                    type = $"application/{filetype}";
                }
                if (filetype == "json")
                {
                    type = $"application/json";
                }
                if(filetype == "mp4"|| filetype == "webm")
                {
                    type = $"video/{filetype}";
                }
                if (filetype == "png" || filetype == "gif")
                {
                    type = $"image/{filetype}";
                }
                if (filetype == "jpg" || filetype == "jpeg")
                {
                    type = $"image/jpeg";
                }

                return File(stream, contentType: type, fileDownloadName: filename, enableRangeProcessing: true);
            }
            else
            {
                return BadRequest();
            }
    
        }


        [HttpPost("PostFile")]
        public async Task <string> PostFile([FromForm] UploadFile uploadFile)
        {

            try
            {
                if (Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                {
                    DirectoryInfo d = new DirectoryInfo(_environment.WebRootPath + "\\Upload\\"); //Assuming Test is your Folder

                    FileInfo[] Files = d.GetFiles(); //Getting Text files
                    string str = "";

                    foreach (FileInfo file in Files)
                    {
                        str = str + ", " + file.Name;

                        Debug.WriteLine(str);
                    }


                }

                if (uploadFile.files.Length>0)
                {
                    if(!Directory.Exists(_environment.WebRootPath+"\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");


                    }

                    using(FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\"+uploadFile.files.FileName))
                    {
                        uploadFile.files.CopyTo(fileStream);

                        //return Ok("\\Upload\\" + uploadFile.files.FileName);




                        InfoFiles infoFile = new InfoFiles();


                        infoFile.FileName = uploadFile.files.FileName;
                        infoFile.FilePath = _environment.WebRootPath + "\\Upload\\";
                        infoFile.FileSize = Math.Round((uploadFile.files.Length* 0.00000095367432/1),2).ToString()+"MB";



                        if (!_infoFileDal.GetList().ToList().Exists(i=>i.FileName== infoFile.FileName))
                        {

                            _infoFileDal.Add(infoFile);

                        }

                     

                        fileStream.Flush();

                        return "Upload Done.";
                    }

                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }

            return  "";
        }





     
    }
}
