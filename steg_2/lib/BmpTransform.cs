using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steg_2.lib
{
    
    class BmpTransform
    {
        Bitmap _newBitmap;
        Bitmap _oldBitmap;
        public BmpTransform(Bitmap source)
        {
            MemoryStream memoryStream = new MemoryStream();
            _oldBitmap = source;
            source.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _newBitmap = new Bitmap(memoryStream);
        }
        public Bitmap markedBitmap()
        {
            Bitmap result = new Bitmap(_oldBitmap);
            var list = changedPixels();
            Color nc = Color.Red;
            for (int i =0; i< list.Count; i++)
            {
                result.SetPixel(list[i].X, list[i].Y, nc);
            }
            return result;
        }
        public List<Point> changedPixels()
        {
            List<Point> changed = new List<Point>();
            for (int x = 0; x < _oldBitmap.Width; x++)
            {
                for (int y = 0; y < _oldBitmap.Height; y++)
                {
                    Color oldclr = _oldBitmap.GetPixel(x, y);
                    Color newclr = _newBitmap.GetPixel(x, y);
                    if(oldclr != newclr)
                    {
                        changed.Add(new Point(x, y));
                    }
                }
            }
            return changed;
        }
        public int nrOFPixelsChanged()
        {
            int changed = 0;
            for (int x = 0; x < _oldBitmap.Width; x++)
            {
                for (int y = 0; y < _oldBitmap.Height; y++)
                {
                    Color oldclr = _oldBitmap.GetPixel(x, y);
                    Color newclr = _newBitmap.GetPixel(x, y);
                    if (oldclr != newclr)
                    {
                        changed++;
                    }
                }
            }
            return changed;
        }
    }
}
