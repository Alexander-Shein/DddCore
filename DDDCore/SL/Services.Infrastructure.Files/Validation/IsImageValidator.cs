using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Contracts.Services.Infrastructure.Files.Models.Input;
using Contracts.Services.Infrastructure.Files.Validation;

namespace Services.Infrastructure.Files.Validation
{
    public class IsImageValidator : IFileValidator
    {
        public void Validate(FileDetails fileDetails)
        {
            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var fileExtension = Path.GetExtension(fileDetails.OriginalFileName);

            if (!String.Equals(fileExtension, ".jpg", StringComparison.OrdinalIgnoreCase) &&
                !String.Equals(fileExtension, ".bmp", StringComparison.OrdinalIgnoreCase) &&
                !String.Equals(fileExtension, ".png", StringComparison.OrdinalIgnoreCase) &&
                !String.Equals(fileExtension, ".gif", StringComparison.OrdinalIgnoreCase) &&
                !String.Equals(fileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("The image extension is not valid.");
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                byte[] buffer = new byte[512];
                fileDetails.File.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    
                }
            }
            //TODO (AlexS): must not catch all exceptions
            catch (Exception)
            {
                throw new ArgumentException("File is not an image.");
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new Bitmap(fileDetails.File))
                {
                }
            }
            //TODO (AlexS): must not catch all exceptions
            catch (Exception)
            {
                throw new ArgumentException("File is not an image.");
            }
        }
    }
}