using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Net.Mail;
using TestRegister.Business.Models;
using TestRegister.DAL.Entity;

namespace TestRegister.Business
{
    public class Tools
    {
        public static string uploadPath = "uploads";
        public static string noPhoto = "/" + Tools.uploadPath + "/noPhoto.gif";
        public static void SendEmail(string Email, string subject, string body)
        {
            try
            {
                var threadSendMails = new Thread(delegate ()
                {
                    //string messageBody = "<a href=\"" + message + ">Click to confirm<a>";
                    string messageBody = body;
                    const string fromAddress = "imagesharing.sigma@gmail.com";
                    const string mailPassword = "3176771aA";
                    var client = new SmtpClient
                    {
                        Port = 587,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Timeout = 10000,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress, mailPassword)
                    };
                    var sendMail = new MailMessage { IsBodyHtml = true, From = new MailAddress(fromAddress) };
                    sendMail.To.Add(new MailAddress(Email));
                    sendMail.Subject = "Email Confirmation";
                    sendMail.Body = messageBody;
                    sendMail.IsBodyHtml = true;
                    client.Send(sendMail);

                })
                { IsBackground = true };
                threadSendMails.Start();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static string saveImg(Stream img, string path)
        {
            Bitmap bigImg = new Bitmap(img);
            Image smallImg = resizeImage(bigImg, 200.0f);

            var name = getRandomName(path);

            bigImg.Save(Path.Combine(path, name + "_big.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            smallImg.Save(Path.Combine(path, name + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            // "/uploads/123.jpg"  -- сразу
            // "/uploads/123_big.jpg" -- при клике. replace(".jpg", "_big.jpg") на стороне клиента
            return string.Format("/{0}/{1}.jpg", uploadPath, name);

        }

        private static Image resizeImage(Image srcImg, float widthNew, float heightNew = -1)
        {
            if (heightNew == -1)
            {
                heightNew = (widthNew / srcImg.Width) * srcImg.Height;
            }
            Bitmap newBitmap = new Bitmap((int)widthNew, (int)heightNew);
            Graphics g = Graphics.FromImage(newBitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(srcImg, 0, 0, widthNew, heightNew);

            return newBitmap;
        }

        private static string getRandomName(string basePath)
        {
            string name;
            string path;
            Random rand = new Random();
            do
            {
                name = rand.Next().ToString();
                path = Path.Combine(basePath, name + ".jpg"); // сделать имя более случайным
            } while (File.Exists(path));
            return name;
        }

        public static void setUpLocale(string lang)
        {
            var ci = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }

        public static string getName(User user)
        {
            return user.Email;
        }

        public static List<CommentModel> commentsToModel(IEnumerable<Comment> comments, int userId)
        {
            var commentsModel = new List<CommentModel>();
            foreach (var comment in comments)
            {
                var c = new CommentModel();
                c.id = comment.Id;
                c.isOwner = comment.User.Id == userId;
                c.msg = comment.Message;
                c.ownerId = comment.User.Id;
                c.ownerName = Tools.getName(comment.User);
                c.time = comment.Time;
                commentsModel.Add(c);
            }
            return commentsModel;

        }
    }
}
