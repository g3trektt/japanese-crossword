using System.Drawing;

namespace japanese
{
    internal abstract class BitmapTools
    {
        public static int[,] ToArray(Bitmap bitmap)
        {
            int[,] pic = new int[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    if (bitmap.GetPixel(i, j) != System.Drawing.Color.FromArgb(255, 255, 255))
                    {
                        pic[j, i] = 1;
                    }
                }
            }
            return pic;
        }
        public static int[,] GetRowStreaks(Bitmap bitmap)
        {
            return GetRowStreaks(BitmapTools.ToArray(bitmap));
        }
        public static int[,] GetRowStreaks(int[,] pic)
        {
            int sum = 0;
            int lastI = 0;
            int lastJ = 0;
            int[,] columnStreaks = new int[32, 32];
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (pic[i, j] == 1)
                    {
                        sum++;
                        if (j == 31)
                        {
                            columnStreaks[i, j] = sum;
                            sum = 0;
                        }
                    }
                    else
                    {
                        if (j != 0)
                        {
                            columnStreaks[lastI, lastJ] = sum;
                            sum = 0;
                        }
                    }

                    lastI = i;
                    lastJ = j;
                }
            }
            return columnStreaks;
        }
        public static int[,] GetColumnStreaks(Bitmap bitmap)
        {
            return GetColumnStreaks(BitmapTools.ToArray(bitmap));
        }
        public static int[,] GetColumnStreaks(int[,] pic)
        {
            int sum = 0;
            int lastI = 0;
            int lastJ = 0;
            int[,] rowStreaks = new int[32, 32];
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (pic[j, i] == 1)
                    {
                        sum++;
                        if (j == 31)
                        {
                            rowStreaks[i, j] = sum;
                            sum = 0;
                        }
                    }
                    else
                    {
                        if (j != 0)
                        {
                            rowStreaks[lastI, lastJ] = sum;
                            sum = 0;
                        }
                    }

                    lastI = i;
                    lastJ = j;
                }
            }
            return rowStreaks;
        }
    }
}
