using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Principal;

namespace Shop.Service
{
    public class UploadImage
    {
       public string Upload(string url)
        {
            var acount = new Account("dodaeo6wb", "551373531286584", "pAe31pfi7f3wqsInGQ9EqxbKXxg");
            var cloudinary = new Cloudinary(acount);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(url),
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            string imageUrl = uploadResult.SecureUrl.ToString();

            return imageUrl;
        }
    }
}
