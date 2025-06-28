using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JellyFlix_MediaHub.Utils
{
    internal class AvatarImage
    {
        private readonly string user_image_path;
        public AvatarImage(string username)
        {
            user_image_path = GetUserImageDir(username);
        }
        private string GetUserImageDir(string username)
        {
            string app_data_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string app_folder = Path.Combine(app_data_path, "JellyFlix_MediaHub");
            string img_dir = Path.Combine(app_folder, "UserImage");

            if (!Directory.Exists(img_dir))
            {
                Directory.CreateDirectory(img_dir);
            }

            return Path.Combine(img_dir, $"user_{username}_avatar.png");
        }

        public Image LoadUserAvatar()
        {
            if (File.Exists(user_image_path))
            {
                try
                {
                    using (var stream = new FileStream(user_image_path, FileMode.Open, FileAccess.Read))
                    {
                        return new Bitmap(Image.FromStream(stream));
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine($"Error loading user avatar: {ex.Message}");
                    return null;
                }
            } else
            {
                return null;
            }
        }

        public bool SaveUserAvatar(string selected_img_path)
        {
            try
            {
                const int size = 145;
                using (Image og_img = Image.FromFile(selected_img_path))
                {
                    using (Bitmap circle_img = new Bitmap(size, size))
                    {
                        using (Graphics g = Graphics.FromImage(circle_img))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            using (GraphicsPath path = new GraphicsPath())
                            {
                                path.AddEllipse(0, 0, size, size);
                                g.SetClip(path);
                                g.Clear(Color.Transparent);
                                g.DrawImage(og_img, 0, 0, size, size);
                            }
                        }

                        circle_img.Save(user_image_path, ImageFormat.Png);
                    }
                }
                return true;
            } catch (Exception e)
            {
                Console.WriteLine($"Error saving user avatar: {e.Message}");
                return false;
            }
        }
    }
}
