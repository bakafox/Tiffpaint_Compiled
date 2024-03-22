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
        short[] red;
        short[] green;
        short[] blue;
        int width;
        int height;

        private Point previousPoint;
        private bool isDrawing = false;
        private Bitmap drawingBitmap;



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
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            //drawingBitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            //pictureBox2.Image = drawingBitmap;
            //ClearDrawing();
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
            Bitmap bitmap = new Bitmap(width, height);

            // Преобразование значений из short в byte (0-255)
            byte[] redBytes = Array.ConvertAll(red, val => (byte)(val / 10)); // делим на 256 чтобы уместить значения в диапазон 0-255
            byte[] greenBytes = Array.ConvertAll(green, val => (byte)(val / 10));
            byte[] blueBytes = Array.ConvertAll(blue, val => (byte)(val / 10));

            // Заполнение изображения данными
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x; // Индекс текущего пикселя в массивах данных
                    Color color = Color.FromArgb(redBytes[index], greenBytes[index], blueBytes[index]);
                    bitmap.SetPixel(x, y, color);
                }
            }

            box.Image = bitmap;
        }

        /// <summary>
        /// Выводит изображение с выделенными областями
        /// </summary>
        /// <param name="box">Контейнер, куда выводить</param>
        public void PrintSelectingAreasImage(PictureBox box)
        {
            // Создание объекта Bitmap
            Bitmap bitmap2 = new Bitmap(width, height);

            Color gray = Color.FromArgb(128, 128, 128);

            // Черный цвет
            Color black = Color.Black;

            // Белый цвет
            Color white = Color.White;
            // Заполнение изображения данными
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x; // Индекс текущего пикселя в массивах данных
                    int brightness = (red[index] + green[index] + blue[index]) / 3;
                    if (brightness < 300)
                        bitmap2.SetPixel(x, y, gray);
                    else if (brightness < 800 && brightness > 300)
                        bitmap2.SetPixel(x, y, white);
                    else
                        bitmap2.SetPixel(x, y, black);
                }
            }

            box.Image = bitmap2;
            drawingBitmap = bitmap2;
        }




        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            previousPoint = e.Location;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(drawingBitmap))
                {
                    Pen pen = new Pen(Color.Black, 2); // Указывает цвет и толщину линии
                    g.DrawLine(pen, previousPoint, e.Location);
                }
                pictureBox2.Invalidate(); // Обновление PictureBox
                previousPoint = e.Location;
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void ClearDrawing()
        {
            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                g.Clear(Color.White);
            }
            pictureBox2.Invalidate();
        }
    }
}
