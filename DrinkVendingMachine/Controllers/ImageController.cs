﻿using DrinkVendingMachine.DataService.Interface;
using System.Web.Mvc;

namespace DrinkVendingMachine.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public ActionResult GetImageFile(long drinkId)
        {
            var imageBody = imageService.GetImageBody(drinkId);
            return new FileStreamResult(new System.IO.MemoryStream(imageBody), "image/jpeg");
        }
    }
}