using Microsoft.AspNetCore.Mvc;
using PRN_Project.Models;

namespace PRN_Project.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManage()
        {


            return View();
        }
        public IActionResult Upload(string audioTitle, IFormFile audioFile, IFormFile audioImage, int genreId, int moodId)
        {
            String imagePath = "/audio/img/";
            AudioMarketContext audioMarketContext = new AudioMarketContext();

            try
            {
                // Check if audioFile and audioImage are not null
                if (audioFile != null && audioImage != null)
                {
                    // Save audioFile to a specified location
                    var audioFileName = Path.Combine("wwwroot/audio", audioFile.FileName);
                    using (var stream = new FileStream(audioFileName, FileMode.Create))
                    {
                        audioFile.CopyTo(stream);
                    }

                    // Save audioImage to a specified location
                    var imageFileName = Path.Combine("wwwroot/audio/img", audioImage.FileName);
                    using (var stream = new FileStream(imageFileName, FileMode.Create))
                    {
                        audioImage.CopyTo(stream);
                    }

                    Audio audio = new Audio();

                    audio.title = audioTitle;
                    audio.filename = "/audio/" + audioFile.FileName;
                    audio.image = "/audio/img/" + audioImage.FileName;
                    audio.artistId = HttpContext.Session.GetInt32("userId");
                    audio.genreId = genreId; audio.moodId = moodId;
                    audio.status = true;

                    audioMarketContext.Audios.Add(audio);
                    audioMarketContext.SaveChanges();

                    return RedirectToAction("Admin", "Audio");
                }
                else
                {
                    // Handle the case where audioFile or audioImage is not provided
                    return Redirect("./Upload?error");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as file I/O errors
                return Redirect("./Upload?error");
            }
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}

