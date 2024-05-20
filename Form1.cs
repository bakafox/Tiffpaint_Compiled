using MaxRev.Gdal.Core;
using OSGeo.GDAL;
using System.Diagnostics;
using System.Drawing.Imaging;


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

        private int grey;
        public int GreyThreshold
        {
            get
            {
                return grey;
            }
            set
            {
                if (value != GreyThreshold)
                {
                    grey = value;
                    TrackGrey.Value = grey;
                    Greytxt.Text = grey.ToString();
                }
            }
        }
        private int white;
        public int WhiteThreshold
        {
            get
            {
                return white;
            }
            set
            {
                if (value != WhiteThreshold)
                {
                    white = value;
                    Whitetxt.Text = white.ToString();
                    TrackWhite.Value = white;
                }
            }
        }

        Bitmap ChunkImage;
        Pen pen;
        Color CurrentColor;
        private string imagePath; // Переменная для хранения пути к изображению
        string filePath;

        private int chunkSize = 512;
        private int currentChunkX = 0;
        private int currentChunkY = 0;


        private int ConvertingNumber = 13;                              // Число, на которое надо разделить 16 для приведения к 8


        public Form1()
        {
            InitializeComponent();
            GdalBase.ConfigureAll();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TrackInit();
            InitializePen();
            GreyThreshold = 300;
            WhiteThreshold = 800;
            pictureBox2.Visible = false;
            pictureBox2.MouseWheel += pictureBox_MouseWheel;
            pictureBox1.MouseWheel += pictureBox_MouseWheel;
            pictureBox3.MouseWheel += pictureBox_MouseWheel;

        }

        /// <summary>
        /// Инициализация ползунков
        /// </summary>
        private void TrackInit()
        {
            TrackGrey.Minimum = 100;
            TrackGrey.Maximum = 500;
            TrackGrey.TickFrequency = 10;
            TrackGrey.SmallChange = 10; // Шаг изменения значения при использовании стрелок
            TrackGrey.LargeChange = 20; // Шаг изменения значения при клике на трекбаре вне области ползунка
            TrackGrey.ValueChanged += (sender, e) =>
            {
                GreyThreshold = TrackGrey.Value;
                PrintSelectingAreasImage(pictureBox2, ChunkImage, GreyThreshold, WhiteThreshold);
            };

            TrackWhite.Minimum = 600;
            TrackWhite.Maximum = 1000;
            TrackWhite.TickFrequency = 10;
            TrackWhite.SmallChange = 10; // Шаг изменения значения при использовании стрелок
            TrackWhite.LargeChange = 20; // Шаг изменения значения при клике на трекбаре вне области ползунка
            TrackWhite.ValueChanged += (sender, e) =>
            {
                WhiteThreshold = TrackWhite.Value;
                PrintSelectingAreasImage(pictureBox2, ChunkImage, GreyThreshold, WhiteThreshold);
            };
        }

        /// <summary>
        /// Загружает в массивы значения пикселей
        /// </summary>
        public void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.tif;*.tiff;*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
            openFileDialog.Title = "Выберите изображение";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName; // Сохраняем путь к файлу
                filePath = imagePath; // Также сохраняем путь к файлу в локальную переменную, если это необходимо

                Dataset dataset = Gdal.Open(filePath, Access.GA_ReadOnly);
                if (dataset == null)
                {
                    MessageBox.Show("Не удалось открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                width = dataset.RasterXSize;
                height = dataset.RasterYSize;

                short[] buffer = new short[width * height * 3];
                dataset.ReadRaster(0, 0, width, height, buffer, width, height, 3, null, 0, 0, 0);

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
        }

        /// <summary>
        /// Выводит кусок оригинального изображения
        /// </summary>
        /// <param name="box">Контейнер для вывода</param>
        /// <param name="chunkX">координата X верхнего левого угла куска</param>
        /// <param name="chunkY">координата Y верхнего левого угла куска</param>
        /// <param name="chunkHeight">Высота чанка</param>
        /// <param name="chunkWidth">Шинина чанка</param>
        public void PrintImageChunk(PictureBox box, int chunkX, int chunkY, int chunkWidth, int chunkHeight)
        {
            Bitmap chunkImage = new Bitmap(chunkWidth, chunkHeight);
            // Преобразование значений из short в byte (0-255) и получение куска изображения
            for (int y = 0; y < chunkHeight; y++)
            {
                for (int x = 0; x < chunkWidth; x++)
                {
                    int index = (chunkY + y) * width + chunkX + x;
                    if (index >= 0 && index < red.Length)
                    {
                        byte redByte = (byte)(red[index] / ConvertingNumber);
                        byte greenByte = (byte)(green[index] / ConvertingNumber);
                        byte blueByte = (byte)(blue[index] / ConvertingNumber);
                        Color color = Color.FromArgb(redByte, greenByte, blueByte);
                        chunkImage.SetPixel(x, y, color);

                    }
                    else
                    {
                        // Если индекс находится за пределами массива, заполните этот пиксель черным цветом или любым другим по вашему выбору
                        chunkImage.SetPixel(x, y, Color.Black);
                    }
                }
            }
            box.Image = chunkImage;
            ChunkImage = chunkImage;

            string fileName = Path.GetFileNameWithoutExtension(imagePath); // Имя файла без расширения
            string chunkName = $"{fileName}_Chunk_{currentChunkX}_{currentChunkY}.tiff"; // Уникальное имя чанка
            string chunkPath = Path.Combine(Path.GetDirectoryName(imagePath), chunkName); // Полный путь к чанку
            if (LoadChunkMask(chunkPath))
            {
                PrintSelectingAreasImage(pictureBox2, ChunkImage, 300, 800);
            }
        }

        /// <summary>
        /// Выводит изображение с выделенными областями
        /// </summary>
        /// <param name="box">Контейнер, куда выводить</param>
        /// <param name="bitmap">Исходное изображение</param>
        /// <param name="grayThreshold">Порог яркости для определения серой области</param>
        /// <param name="whiteThreshold">Порог яркости для определения белой области</param>
        public void PrintSelectingAreasImage(PictureBox box, Bitmap bitmap, int grayThreshold, int whiteThreshold)
        {
            if (bitmap != null)
            {


                // Создание объекта Bitmap
                drawingBitmap = new Bitmap(bitmap.Width, bitmap.Height);
                Color gray = Color.FromArgb(128, 128, 128);
                Color black = Color.Black;
                Color white = Color.White;
                // Заполнение изображения данными
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);
                        int brightness = (int)(pixelColor.R * ConvertingNumber + pixelColor.G * ConvertingNumber + pixelColor.B * ConvertingNumber) / 3;
                        if (brightness < grayThreshold)
                            drawingBitmap.SetPixel(x, y, gray);
                        else if (brightness < whiteThreshold && brightness > grayThreshold)
                            drawingBitmap.SetPixel(x, y, white);
                        else
                            drawingBitmap.SetPixel(x, y, black);
                    }
                }
                box.Image = drawingBitmap;
                pictureBox3.Image = drawingBitmap;
            }
        }

        //=================================================== Рисование

        /// <summary>
        /// Событие нажатия мыши при рисовании
        /// </summary>
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

        /// <summary>
        /// Обрботчик события перемещенпия мыши при рисовании
        /// </summary>
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
                pictureBox3.Invalidate(); // Обновление PictureBox
                previousPoint = bitmapLocation;
            }
        }

        /// <summary>
        /// Обработчик события отпускания мыши при рисовании
        /// </summary>
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        /// <summary>
        /// Метод для преобразования координат мыши из PictureBox в координаты Bitmap
        /// </summary>
        private Point PictureBoxToBitmapCoordinates(PictureBox pictureBox, Point pictureBoxLocation)
        {
            float scaleX = (float)drawingBitmap.Width / pictureBox.ClientSize.Width;
            float scaleY = (float)drawingBitmap.Height / pictureBox.ClientSize.Height;

            // Учитываем смещение PictureBox при масштабировании
            int bitmapX = (int)(pictureBoxLocation.X * scaleX);
            int bitmapY = (int)(pictureBoxLocation.Y * scaleY);

            return new Point(bitmapX, bitmapY);
        }

        /// <summary>
        /// Функция сохранения готового изображения
        /// </summary>
        /// <param name="bitmap">Изображение</param>
        /// <param name="outputPath">Путь сохранения</param>
        public static void SaveAsGeoTiff(Bitmap bitmap, string outputPath)
        {
            Gdal.AllRegister();
            Dataset ds = Gdal.GetDriverByName("GTiff").Create(outputPath, bitmap.Width, bitmap.Height, 1, DataType.GDT_UInt16, null);

            // Write bitmap data to the GeoTIFF dataset
            BitmapDataToRaster(bitmap, ds);

            // Close the dataset
            ds.Dispose();
        }

        /// <summary>
        /// Функция копирования данных из bitmap в dataset
        /// </summary>
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

        /// <summary>
        /// Обработчик запрещающий вводить в текстовое поле что-либо кроме цифр
        /// </summary>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Предотвратить ввод символа
            }
        }

        /// <summary>
        /// Инициализация кисти для рисования
        /// </summary>
        private void InitializePen()
        {
            txtSize.Text = "2";
            BlackRd.Checked = true;
        }

        /// <summary>
        /// Выбор кнопки черного цвета
        /// </summary>
        private void BlackRd_CheckedChanged(object sender, EventArgs e)
        {
            if (BlackRd.Checked)
            {
                pen = new Pen(Color.Black, Convert.ToInt32(txtSize.Text));
                CurrentColor = Color.Black;
            }
        }

        /// <summary>
        /// Выбор кнопки белого цвета
        /// </summary>
        private void WhiteRb_CheckedChanged(object sender, EventArgs e)
        {
            if (WhiteRb.Checked)
            {
                CurrentColor = Color.White;
                pen = new Pen(Color.White, Convert.ToInt32(txtSize.Text));
            }
        }

        /// <summary>
        /// Выбор кнопки серого цвета
        /// </summary>
        private void GrayRb_CheckedChanged(object sender, EventArgs e)
        {
            if (GrayRb.Checked)
            {
                pen = new Pen(Color.Gray, Convert.ToInt32(txtSize.Text));
                CurrentColor = Color.Gray;
            }

        }

        /// <summary>
        /// Обработчик события изменения текстового поля
        /// </summary>
        private void txtSize_TextChanged(object sender, EventArgs e)
        {
            pen = new Pen(CurrentColor, Convert.ToInt32(txtSize.Text));
        }

        /// <summary>
        /// Обработчик прокрутки колеса мыши для реализация функционала zoom
        /// </summary>
        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox box)
            {
                if (e.Delta > 0)
                {
                    // Увеличение масштаба при прокрутке колесика вверх
                    if (box.SizeMode != PictureBoxSizeMode.Zoom)
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                        //previousPoint = PictureBoxToBitmapCoordinates(pictureBox2, previousPoint);
                    }


                }
                else
                {
                    // Уменьшение масштаба при прокрутке колесика вниз
                    if (box.SizeMode != PictureBoxSizeMode.Normal)
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                        pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
                        pictureBox3.SizeMode = PictureBoxSizeMode.Normal;
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопок с клавиатуры
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Обработка нажатия клавиш Z, X и C
            if (e.KeyCode == Keys.Z)
            {
                if (pictureBox2.Visible == true)
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
            else if (e.KeyCode == Keys.A)
            {
                ShowPreviousChunk();
            }
            else if (e.KeyCode == Keys.S)
            {
                ShowNextChunk();
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки загрузки изображения
        /// </summary>
        private void LoadOriginalImagebtn_Click(object sender, EventArgs e)
        {
            LoadImage();
            StatisticksWrite();
            //PrintOriginalImage(pictureBox1);
            PrintImageChunk(pictureBox1, 0, 0, 512, 512);

            ShowChunkInfo();
        }

        /// <summary>
        /// Вывод статистики
        /// </summary>
        private void StatisticksWrite()
        {

            int chunkSize = 512;

            int rows = (int)Math.Ceiling((double)height / chunkSize);
            int cols = (int)Math.Ceiling((double)width / chunkSize);

            int totalChunks = rows * cols;

            int lastRowChunks = (int)Math.Ceiling((double)(width % chunkSize) / chunkSize);
            int lastColChunks = (int)Math.Ceiling((double)(height % chunkSize) / chunkSize);

            totalChunks += (lastRowChunks * (rows - 1)) + lastColChunks;

            Debug.WriteLine("Количество кусочков размером 512x512 в изображении {0}x{1}: {2}", width, height, totalChunks);
        }

        /// <summary>
        /// Проверка пороговых значений
        /// </summary>
        private void Colortxt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Greytxt.Text) || string.IsNullOrEmpty(Whitetxt.Text))
            {
                ApplyBtn.Enabled = false;
            }
            else
            {
                ApplyBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки применения пороговых значений
        /// </summary>
        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            if (GreyThreshold > 0 && WhiteThreshold > 0 && GreyThreshold < WhiteThreshold)
            {
                PrintSelectingAreasImage(pictureBox2, ChunkImage, GreyThreshold, WhiteThreshold);
            }
        }

        /// <summary>
        /// Метод для отображения следующего чанка
        /// </summary>
        private void ShowNextChunk()
        {
            int chunkWidth = Math.Min(chunkSize, width - currentChunkX);
            int chunkHeight = Math.Min(chunkSize, height - currentChunkY);

            // Сохраняем текущий чанк под уникальным именем
            string fileName = Path.GetFileNameWithoutExtension(imagePath); // Имя файла без расширения
            string chunkName = $"{fileName}_Chunk_{currentChunkX}_{currentChunkY}.tiff"; // Уникальное имя чанка
            string chunkPath = Path.Combine(Path.GetDirectoryName(imagePath), chunkName); // Полный путь к чанку
                                                                                          // Сохраняем текущий чанк как изображение по полученному пути
            SaveChunkImage(chunkPath);
            // Если текущий чанк занимает весь ряд, переходим к следующему ряду
            if (currentChunkX + chunkWidth >= width)
            {
                currentChunkX = 0;
                currentChunkY += chunkSize;
            }
            else
            {
                currentChunkX += chunkSize;
            }

            // Отображаем чанк и обновляем информацию о чанке
            PrintImageChunk(pictureBox1, currentChunkX, currentChunkY, chunkWidth, chunkHeight);
            ShowChunkInfo();
            chunkName = $"{fileName}_Chunk_{currentChunkX}_{currentChunkY}.tiff"; // Уникальное имя чанка
            chunkPath = Path.Combine(Path.GetDirectoryName(imagePath), chunkName); // Полный путь к чанку
            if (LoadChunkMask(chunkPath))
            {
                PrintSelectingAreasImage(pictureBox2, ChunkImage, 300, 800);
            }


        }

        private void SaveChunkImage(string chunkPath)
        {
            // Проверяем, существует ли файл
            if (File.Exists(chunkPath))
            {
                File.Delete(chunkPath);
            }

            // Сохраняем Bitmap в файл
            drawingBitmap.Save(chunkPath, ImageFormat.Tiff);
            drawingBitmap.Dispose();
        }

        private bool LoadChunkMask(string chunkPath)
        {
            if (File.Exists(chunkPath))
            {
                Bitmap originalBitmap = new Bitmap(chunkPath);
                drawingBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    for (int x = 0; x < originalBitmap.Width; x++)
                    {
                        // Получаем цвет пикселя из originalBitmap
                        Color pixelColor = originalBitmap.GetPixel(x, y);

                        // Устанавливаем цвет пикселя в drawingBitmap
                        drawingBitmap.SetPixel(x, y, pixelColor);
                    }
                }
                originalBitmap.Dispose();
                // Отображаем клонированный Bitmap на PictureBox'ах
                pictureBox2.Image = drawingBitmap;
                pictureBox3.Image = drawingBitmap;
                return false;


            }
            return true;
        }

        /// <summary>
        /// Метод для отображения предыдущего чанка
        /// </summary>
        private void ShowPreviousChunk()
        {
            // Сохраняем текущий чанк под уникальным именем
            string fileName = Path.GetFileNameWithoutExtension(imagePath); // Имя файла без расширения
            string chunkName = $"{fileName}_Chunk_{currentChunkX}_{currentChunkY}.tiff"; // Уникальное имя чанка
            string chunkPath = Path.Combine(Path.GetDirectoryName(imagePath), chunkName); // Полный путь к чанку
            SaveChunkImage(chunkPath);

            int newChunkX = currentChunkX - chunkSize;

            // Если новая координата X отрицательная, значит нужно перейти к предыдущему ряду
            if (newChunkX < 0 && currentChunkY >= chunkSize)
            {
                // Перейти к предыдущему ряду
                currentChunkX = Math.Max(0, width - chunkSize);
                currentChunkY -= chunkSize;
            }
            // Если новая координата X не отрицательная, оставляем ее без изменений
            else
            {
                currentChunkX = Math.Max(0, newChunkX);
            }

            // Открыть маску предыдущего чанка, если она существует


            // Отобразить чанк
            PrintImageChunk(pictureBox1, currentChunkX, currentChunkY, chunkSize, chunkSize);
            string maskFolderPath = Path.GetDirectoryName(imagePath);
            string fileName2 = Path.GetFileNameWithoutExtension(imagePath); // Имя файла без расширения
            string maskFileName = $"{fileName2}_Chunk_{currentChunkX}_{currentChunkY}.tiff";
            string maskFilePath = Path.Combine(maskFolderPath, maskFileName);
            LoadChunkMask(maskFilePath);
            ShowChunkInfo();
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            ShowPreviousChunk();
        }

        private void Further_btn_Click(object sender, EventArgs e)
        {
            ShowNextChunk();
        }

        private void ShowChunkInfo()
        {
            int chunkWidth = Math.Min(chunkSize, width - currentChunkX);
            int chunkHeight = Math.Min(chunkSize, height - currentChunkY);

            int chunkNumberX = currentChunkX / chunkSize + 1;
            int chunkNumberY = currentChunkY / chunkSize + 1;
            int totalChunksX = (int)Math.Ceiling((double)width / chunkSize);
            int totalChunksY = (int)Math.Ceiling((double)height / chunkSize);
            int totalChunks = totalChunksX * totalChunksY;
            int currentChunkNumber = (chunkNumberY - 1) * totalChunksX + chunkNumberX;

            string info = string.Format("Координаты чанка: ({0}, {1})\n", currentChunkX, currentChunkY);
            info += string.Format("Размер чанка: {0}x{1}\n", chunkWidth, chunkHeight);
            info += string.Format("Общее количество чанков: {0}\n", totalChunks);
            info += string.Format("Это чанк номер {0} из {1}", currentChunkNumber, totalChunks);

            ChunkInfo_lbl.Text = info;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
