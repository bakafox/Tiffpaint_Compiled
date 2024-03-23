using MaxRev.Gdal.Core;
using OSGeo.GDAL;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace Tiffpaint
{
    public partial class Form1 : Form
    {
        //изображение
        short[] red;
        short[] green;
        short[] blue;
        int width;
        int height;

        //рисование
        private Point previousPoint;
        private bool isDrawing = false;
        private Bitmap drawingBitmap;
        private Bitmap OriginalImage;
        Pen pen;
        Color CurrentColor;





        public Form1()
        {
            InitializeComponent();
            GdalBase.ConfigureAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadImage(@"C:\Users\ivan3\Desktop\subimage_1536_1536.tiff");
            PrintOriginalImage(pictureBox1);
            PrintSelectingAreasImage(pictureBox2);
            InitializePen();
            pictureBox2.MouseWheel += pictureBox_MouseWheel;
            pictureBox1.MouseWheel += pictureBox_MouseWheel;
        }
        /// <summary>
        /// Загружает в массивы значения пикселей
        /// </summary>
        /// <param name="filePath">Путь к изображению</param>
        public void LoadImage(string filePath)
        {
            Dataset dataset = Gdal.Open(filePath, Access.GA_ReadOnly);
            if (dataset == null)
            {
                Console.WriteLine("Не удалось открыть файл");
                return;
            }

            // Получение размеров изображения
            width = dataset.RasterXSize;
            height = dataset.RasterYSize;

            // Чтение данных пикселей
            short[] buffer = new short[width * height * 3]; // 3 канала (RGB) по 8 бит на канал
            dataset.ReadRaster(0, 0, width, height, buffer, width, height, 3, null, 0, 0, 0);

            // Закрытие датасета GDAL
            dataset.Dispose();
            red = new short[width * height];
            green = new short[width * height];
            blue = new short[width * height];
            for (int i = 0; i < width * height; i++)
            {
                red[i] = buffer[i];
            }
            for (int i = 0; i < width * height; i++)
            {
                green[i] = buffer[i + width * height];
            }
            for (int i = 0; i < width * height; i++)
            {
                blue[i] = buffer[i + width * height * 2];
            }
        }

        /// <summary>
        /// Выводит оригинальное изображение
        /// </summary>
        /// <param name="box">Контейнер, куда выводить</param>
        public void PrintOriginalImage(PictureBox box)
        {
            OriginalImage = new Bitmap(width, height);

            // Преобразование значений из short в byte (0-255)
            byte[] redBytes = Array.ConvertAll(red, val => (byte)(val / 7)); // делим на 256 чтобы уместить значения в диапазон 0-255
            byte[] greenBytes = Array.ConvertAll(green, val => (byte)(val / 7));
            byte[] blueBytes = Array.ConvertAll(blue, val => (byte)(val / 7));

            // Заполнение изображения данными
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x; // Индекс текущего пикселя в массивах данных
                    Color color = Color.FromArgb(redBytes[index], greenBytes[index], blueBytes[index]);
                    OriginalImage.SetPixel(x, y, color);
                }
            }
            box.Image = OriginalImage;
            //box.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// Выводит изображение с выделенными областями
        /// </summary>
        /// <param name="box">Контейнер, куда выводить</param>
        public void PrintSelectingAreasImage(PictureBox box)
        {
            //box.BackColor = Color.Transparent;



            // Создание объекта Bitmap
            drawingBitmap = new Bitmap(width, height);
            Color gray = Color.FromArgb(128, 128, 128);
            Color black = Color.Black;
            Color white = Color.White;
            // Заполнение изображения данными
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x; // Индекс текущего пикселя в массивах данных
                    int brightness = (red[index] + green[index] + blue[index]) / 3;
                    if (brightness < 300)
                        drawingBitmap.SetPixel(x, y, gray);
                    else if (brightness < 800 && brightness > 300)
                        drawingBitmap.SetPixel(x, y, white);
                    else
                        drawingBitmap.SetPixel(x, y, black);
                }
            }



            box.Image = drawingBitmap;
            //box.Image = ApplyTransparency(drawingBitmap, 0.10f);
            //box.BackColor = Color.FromArgb(200, Color.White);
        }

        private Bitmap ApplyTransparency(Bitmap bmp, float opacity)
        {
            Bitmap resultBitmap = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(resultBitmap))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity; // Устанавливаем значение прозрачности
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);
            }
            return resultBitmap;
        }


        //=================================================== Рисование

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            if (pictureBox2.SizeMode == PictureBoxSizeMode.Zoom)
            {
                previousPoint = PictureBoxToBitmapCoordinates(pictureBox2, e.Location);
            }
            else
                previousPoint = e.Location;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Преобразование координат мыши из PictureBox в координаты Bitmap
                Point bitmapLocation;
                if (pictureBox2.SizeMode == PictureBoxSizeMode.Zoom)
                {
                    bitmapLocation = PictureBoxToBitmapCoordinates(pictureBox2, e.Location);
                }
                else
                    bitmapLocation = e.Location;

                using (Graphics g = Graphics.FromImage(drawingBitmap))
                {
                    // Округление координат мыши
                    Point roundedPreviousPoint = new Point((int)Math.Round((double)previousPoint.X), (int)Math.Round((double)previousPoint.Y));
                    Point roundedBitmapLocation = new Point((int)Math.Round((double)bitmapLocation.X), (int)Math.Round((double)bitmapLocation.Y));

                    g.DrawLine(pen, roundedPreviousPoint, roundedBitmapLocation);
                }
                pictureBox2.Invalidate(); // Обновление PictureBox
                previousPoint = bitmapLocation;
            }
        }

        // Метод для преобразования координат мыши из PictureBox в координаты Bitmap
        private Point PictureBoxToBitmapCoordinates(PictureBox pictureBox, Point pictureBoxLocation)
        {
            float scaleX = (float)drawingBitmap.Width / pictureBox.ClientSize.Width;
            float scaleY = (float)drawingBitmap.Height / pictureBox.ClientSize.Height;

            // Учитываем смещение PictureBox при масштабировании
            int bitmapX = (int)(pictureBoxLocation.X * scaleX);
            int bitmapY = (int)(pictureBoxLocation.Y * scaleY);

            return new Point(bitmapX, bitmapY);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveAsGeoTiff(drawingBitmap, @"C:\Users\ivan3\Desktop\subimage_1536_1536_mask.tiff");
            //SaveBitmap(@"C:\Users\ivan3\Desktop\example.jpg", drawingBitmap, ImageFormat.Jpeg);
        }

        public static void SaveAsGeoTiff(Bitmap bitmap, string outputPath)
        {
            Gdal.AllRegister();
            Dataset ds = Gdal.GetDriverByName("GTiff").Create(outputPath, bitmap.Width, bitmap.Height, 1, DataType.GDT_UInt16, null);

            // Write bitmap data to the GeoTIFF dataset
            BitmapDataToRaster(bitmap, ds);

            // Close the dataset
            ds.Dispose();
        }

        private static void BitmapDataToRaster(Bitmap bitmap, Dataset ds)
        {
            // Блокировка изображения для получения данных в формате BitmapData
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            try
            {
                int stride = data.Stride;
                byte[] buffer = new byte[stride * bitmap.Height];
                System.Runtime.InteropServices.Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

                // Итерация по каждому пикселю изображения
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int index = y * stride + x * 3; // Индекс текущего пикселя в массиве buffer

                        // Получение значения красного канала текущего пикселя
                        byte redValue = buffer[index]; // Красный канал (индекс 2 в формате BGR)

                        // Запись значения красного канала пикселя в растр
                        ds.GetRasterBand(1).WriteRaster(x, y, 1, 1, new byte[] { redValue }, 1, 1, 1, 1);
                    }
                }
            }
            finally
            {
                // Разблокировка данных изображения
                bitmap.UnlockBits(data);
            }
        }

        public static void SaveBitmap(string filePath, Bitmap bitmap, ImageFormat format)
        {
            bitmap.Save(filePath, format);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Предотвратить ввод символа
            }
        }

        private void InitializePen()
        {
            txtSize.Text = "2";
            BlackRd.Checked = true;
        }

        private void BlackRd_CheckedChanged(object sender, EventArgs e)
        {
            if (BlackRd.Checked)
            {
                pen = new Pen(Color.Black, Convert.ToInt32(txtSize.Text));
                CurrentColor = Color.Black;
            }
        }

        private void WhiteRb_CheckedChanged(object sender, EventArgs e)
        {
            if (WhiteRb.Checked)
            {
                CurrentColor = Color.White;
                pen = new Pen(Color.White, Convert.ToInt32(txtSize.Text));
            }
        }

        private void GrayRb_CheckedChanged(object sender, EventArgs e)
        {
            if (GrayRb.Checked)
            {
                pen = new Pen(Color.Gray, Convert.ToInt32(txtSize.Text));
                CurrentColor = Color.Gray;
            }

        }

        private void txtSize_TextChanged(object sender, EventArgs e)
        {
            pen = new Pen(CurrentColor, Convert.ToInt32(txtSize.Text));
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox box)
            {
                if (e.Delta > 0)
                {
                    // Увеличение масштаба при прокрутке колесика вверх
                    if (box.SizeMode != PictureBoxSizeMode.Zoom)
                    {
                        box.SizeMode = PictureBoxSizeMode.Zoom;
                        //previousPoint = PictureBoxToBitmapCoordinates(pictureBox2, previousPoint);
                    }


                }
                else
                {
                    // Уменьшение масштаба при прокрутке колесика вниз
                    if (box.SizeMode != PictureBoxSizeMode.Normal)
                        box.SizeMode = PictureBoxSizeMode.Normal;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Обработка нажатия клавиш Z, X и C
            if (e.KeyCode == Keys.Z)
            {
                if(pictureBox2.Visible == true)
                {
                    pictureBox2.Visible = false;
                }
            }
            else if (e.KeyCode == Keys.X)
            {
                if (pictureBox2.Visible == false)
                {
                    pictureBox2.Visible = true;
                }
            }
            else if (e.KeyCode == Keys.C)
            {
                
            }
        }
    }
}
