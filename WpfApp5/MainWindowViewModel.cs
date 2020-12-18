using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp5
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _text;
        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        private BitmapSource _image;
        public BitmapSource Image
        {
            get => _image;
            set => Set(ref _image, value);
        }

        public MainWindowViewModel()
        {
            WebClient client = new WebClient();

            string address = @"https://h24.mangas.rocks/auto/39/93/83/TowerOfGod_s3_ch68_p02_SIU_Gemini.jpg_res.jpg?t=1608328190&u=0&h=co4rkjO9kkoFMqRaLA1ZmA";

            var imagedata = client.DownloadData(address);

            using (MemoryStream stream = new MemoryStream(imagedata))
            {
                Image = ToBitmapSource(new Bitmap(stream));
            }
               
            using (StreamReader str = new StreamReader("d:\\text.txt", Encoding.Default))
            {
                Text = str.ReadToEnd();
            }
        }

        public BitmapSource ToBitmapSource(Bitmap source)
        {
            BitmapSource bitSrc = null;

            var hBitmap = source.GetHbitmap();

            try
            {
                bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (System.ComponentModel.Win32Exception)
            {
                bitSrc = null;
            }

            return bitSrc;
        }
    }
}
