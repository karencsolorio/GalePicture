using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gale_Picture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmpImage = (Bitmap)pictureBox1.Image;
            Bitmap bmpImage2 = new Bitmap(bmpImage.Width, bmpImage.Height);
            Color colCurrent;
            string pixelInfo = "";
            int currentPixel = 0;
            double red = 0;
            double blue = 0;
            double green = 0;
            double redPixel = 0;
            double otherColor = 0;
            double percentage = 0;
            

            //Determine how many pixels we need to process
            int maxPixels = bmpImage.Height * bmpImage.Width;

            //Set max of the progressbar to Pixel Count
            progressBar.Maximum = maxPixels + 1;

            //Reset Progress Bar
            progressBar.Value = 0;

            //Clear Listbox
            //colorDataList.Items.Clear();

            //Walk over every pixel in the bitmap
            for(int y=0;y<bmpImage.Height; y++)
            {
                for(int x =0; x<bmpImage.Width; x++)
                {
                    //Get Color of Pixel
                    colCurrent = bmpImage.GetPixel(x, y);

                    //Build String of Pixel Info
                    pixelInfo = string.Format($" Pixel {currentPixel} Coord = [{x}, {y}]  Color RGB = {colCurrent.R}, {colCurrent.G}, {colCurrent.B}");

                    //Assign color variable to the color current
                    red = colCurrent.R;
                    blue = colCurrent.B;
                    green = colCurrent.G;

                    //Check for red pixels
                    if(red >= 70 && blue <= 100 && green <= 100)
                    {
                        redPixel++;
                        bmpImage2.SetPixel(x, y, colCurrent);
                    }
                    else
                    {
                        otherColor++;
                    }

                    //Increment the current pixel
                    currentPixel++;

                    //Increase Value of the Progress Bar
                    progressBar.Value = currentPixel;

                    //Add to list
                    //colorDataList.Items.Add(pixelInfo);

                }//End of for loop
                lblProgress.Text = string.Format($"{currentPixel}/{maxPixels} processed");
            }
            //Show image in Picture Box 1
            pictureBox1.Image = bmpImage;

            //Transfer specific color to Picture Box 2
            pictureBox2.Image = bmpImage2;

            //Percentage of red pixels
            percentage = redPixel / otherColor;

            //Exhibit the percentage of the red pixels to the Color Percentage label
            lblColorPercentage.Text = $"Red Pixels: {redPixel}  \t Red Color Percentage: {percentage * 100:F2}%";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show Dialog Box
            DialogResult dgrTemp = openFileDialog1.ShowDialog();

            //Check if User Selected "Open"
            if(dgrTemp == DialogResult.OK)
            {
                //Use Slected File Top Open Bitmap
                Bitmap bmpTemp = new Bitmap(openFileDialog1.FileName);

                //Set BitMap to Picture Box
                pictureBox1.Image = bmpTemp;
            }//end if
        }//end event
    }
}
