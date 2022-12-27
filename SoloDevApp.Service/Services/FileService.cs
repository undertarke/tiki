using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Helpers;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface IFileService 
    {
        Task<string> SaveFileAsync(IFormFile file, string folder);
    }

    public class FileService: IFileService
    {
        
        public FileService()
        {
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            var folderName = Path.Combine("wwwroot", folder);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            // Tạo folder nếu chưa tồn tại
            if (!Directory.Exists(pathToSave))
                Directory.CreateDirectory(pathToSave);

            string newName = FuncUtilities.BestLower(file.FileName, false);
            // Lấy tên file
            string fileName = $"{DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")}-{newName}";

            // Tạo đường dẫn tới file
            string path = Path.Combine(pathToSave, fileName);

            // Kiểm tra xem file bị trùng không
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //using (var stream = new FileStream(path, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}

            //toi uu hoa hinh anh > 500Kb
          
            Image myImage = Image.FromStream(file.OpenReadStream(), true, true);
            // Save the image with a quality of 50% 
            SaveJpeg(path, myImage, 20);
          

            return $"/{folder}/{fileName}";
        }

        // toi uu hinh
        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);

        }

        // toi uu hinh
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

    }
}