using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TextureLayerGeneratorApp
{
    public partial class Form1 : Form
    {
        private const string InputDirName = "Input";
        private const string MasksDirName = "Masks";
        private const string OutputDirName = "Output";
        public static char Dsc = Path.DirectorySeparatorChar;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var layerCount = int.Parse(txtLayerCount.Text);

            if (layerCount < 2)
                throw new IndexOutOfRangeException("layer count must be 2 or more!");
            
            var workDir = txtWorkDir.Text;
            var inputDir = workDir + Dsc + InputDirName;
            var masksDir = workDir + Dsc + MasksDirName;
            var outputDir = workDir + Dsc + OutputDirName;
            
   
            if (!Directory.Exists(workDir))
                throw new FileNotFoundException("Folder don't exist: " + workDir);
            if (!Directory.Exists(inputDir))
                throw new FileNotFoundException("Folder don't exist: " + inputDir);
            if (!Directory.Exists(masksDir))
                throw new FileNotFoundException("Folder don't exist: " + masksDir);
            if (!Directory.Exists(outputDir))
                throw new FileNotFoundException("Folder don't exist: " + outputDir);

            foreach (var file in Directory.GetFiles(inputDir, "*_layer_0.bmp"))
            {
                var pathSplit = file.Split(Dsc);
                var namePrefix = pathSplit[^1].Replace("_layer_0.bmp", "");
                ProcessTextureSet(workDir, inputDir, masksDir, outputDir, layerCount, namePrefix);
            }
            
            MessageBox.Show("DONE!");
        }

        private void ProcessTextureSet(string workDir, string inputDir, string masksDir, string outputDir, int layerCount,
            string namePrefix)
        {
        
            foreach (var file in Directory.GetFiles(masksDir, "*_layer_1.bmp"))
            {
                var pathSplit = file.Split(Dsc);
                var maskPrefix = pathSplit[^1].Replace("_layer_1.bmp", "");
                ProcessLayers(layerCount, inputDir, masksDir, maskPrefix, outputDir, namePrefix);
            }
        }

        private void ProcessLayers(int layerCount, string inputDir, string masksDir, string maskPrefix,
            string outputDir, string namePrefix)
        {
            var resultBitmap = CreateNonIndexedBmp(inputDir + Dsc + namePrefix + "_layer_0.bmp");
            for (var layer = 1; layer < layerCount; layer++)
            {
                var currentBitmap = CreateNonIndexedBmp(inputDir + Dsc + namePrefix + "_layer_" + layer + ".bmp");
                var currentMask = CreateNonIndexedBmp(masksDir + Dsc + maskPrefix + "_layer_" + layer + ".bmp");

                for (var x = 0; x < currentMask.Width; x++)
                {
                    for (var y = 0; y < currentMask.Height; y++)
                    {
                        var maskPixelColor = currentMask.GetPixel(x, y);
                        var currentLayerPixelColor = currentBitmap.GetPixel(x, y);

                        var value = maskPixelColor.R;

                        if (value > 0)
                        {
                            resultBitmap.SetPixel(x, y, currentLayerPixelColor);
                        }
                    }
                }

                currentBitmap.Dispose();
                currentMask.Dispose();
            }

            resultBitmap.Save(outputDir + Dsc + namePrefix + "_" + maskPrefix + ".png");
            resultBitmap.Dispose();
        }

        public Bitmap CreateNonIndexedBmp(string imgPath)
        {
            Image img = Image.FromFile(imgPath);
            Bitmap newBmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

            using (Graphics gfx = Graphics.FromImage(newBmp)) {
                gfx.DrawImage(img, 0, 0);
            }
            img.Dispose();
            return newBmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
    }
}