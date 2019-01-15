using AwokeKnowing.GnuplotCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageHandler
{
    /// <summary>
    /// Логика взаимодействия для Graphic.xaml
    /// </summary>
    public partial class Graphic : Window
    {
        Picture[] pictures;
        public Graphic(Picture[] pictures)
        {
            this.pictures = pictures;
            InitializeComponent();
        }
        private void Plot2d(object sender, RoutedEventArgs e)
        {
            if(XAxis2d.SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("Выберите координату X");
                return;
            }
            if (YAxis2d.SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("Выберите координату Y");
                return;
            }
            double[] xArray = CreateArray(XAxis2d.SelectedIndex);
            double[] yArray = CreateArray(YAxis2d.SelectedIndex);




            double Xmax = 0, Xmin = 0, Ymax = 0, Ymin = 0;

            for (int i = 0; i < pictures.Length; i++)
            {
                if (xArray[i] > Xmax)
                {
                    Xmax = xArray[i];
                }
                if (xArray[i] < Xmin)
                {
                    Xmin = xArray[i];
                }
                if (yArray[i] > Ymax)
                {
                    Ymax = yArray[i];
                }
                if (yArray[i] < Ymin)
                {
                    Ymin = yArray[i];
                }
            }
            Xmax += Xmax / 10;
            Xmin -= Xmin / 10;
            Ymax += Ymax / 10;
            Ymin -= Ymin / 10;
            string xRange = "xrange[" + Xmin.ToString() + ":" + Xmax.ToString() + "]";
            string yRange = "yrange[" + Ymin.ToString() + ":" + Ymax.ToString() + "]";
            string xTitle = "xlabel " + "'" + CreateTitleAxis(XAxis2d.SelectedIndex) + "'";
            string yTitle = "ylabel " + "'" + CreateTitleAxis(YAxis2d.SelectedIndex) + "'";
            GnuPlot.Set(xTitle, yTitle, xRange, yRange);
            GnuPlot.Plot(xArray, yArray);
        }

        private void Plot3d(object sender, RoutedEventArgs e)
        {
            if (XAxis3d.SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("Выберите координату X");
                return;
            }
            if (YAxis3d.SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("Выберите координату Y");
                return;
            }
            if (ZAxis3d.SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("Выберите координату Z");
                return;
            }
            double[] xArray = CreateArray(XAxis3d.SelectedIndex);
            double[] yArray = CreateArray(YAxis3d.SelectedIndex);
            double[] zArray = CreateArray(ZAxis3d.SelectedIndex);


            double Xmax = 0, Xmin = 0, Ymax = 0, Ymin = 0, Zmax = 0, Zmin = 0;

            for (int i = 0; i < pictures.Length; i++)
            {
                if (xArray[i] > Xmax)
                {
                    Xmax = xArray[i];
                }
                if (xArray[i] < Xmin)
                {
                    Xmin = xArray[i];
                }
                if (yArray[i] > Ymax)
                {
                    Ymax = yArray[i];
                }
                if (yArray[i] < Ymin)
                {
                    Ymin = yArray[i];
                }
                if (zArray[i] > Zmax)
                {
                    Zmax = zArray[i];
                }
                if (zArray[i] < Zmin)
                {
                    Zmin = zArray[i];
                }
            }
            Xmax += Xmax / 10;
            Xmin -= Xmin / 10;
            Ymax += Ymax / 10;
            Ymin -= Ymin / 10;
            Zmax += Zmax / 10;
            Zmin -= Zmin / 10;
            string xRange = "xrange[" + Xmin.ToString() + ":" + Xmax.ToString() + "]";
            string yRange = "yrange[" + Ymin.ToString() + ":" + Ymax.ToString() + "]";
            string zRange = "zrange[" + Zmin.ToString() + ":" + Zmax.ToString() + "]";
            string xTitle = "xlabel " + "'" + CreateTitleAxis(XAxis3d.SelectedIndex) + "'";
            string yTitle = "ylabel " + "'" + CreateTitleAxis(YAxis3d.SelectedIndex) + "'";
            string zTitle = "zlabel " + "'" + CreateTitleAxis(ZAxis3d.SelectedIndex) + "'";
            GnuPlot.Set(xTitle, yTitle, zTitle, xRange, yRange, zRange);
            GnuPlot.SPlot(xArray, yArray, zArray, "with points pointtype 8 lc rgb 'blue'");
        }
        private double[] CreateArray(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        double[] red = new double[pictures.Length];
                        for (int i = 0; i < pictures.Length; i++)
                        {
                            red[i] = pictures[i].PercentOfRed;
                        }
                        return red;
                    }
                case 1:
                    {
                        double[] blue = new double[pictures.Length];
                        for (int i = 0; i < pictures.Length; i++)
                        {
                            blue[i] = pictures[i].PercentOfBlue;
                        }
                        return blue;
                    }
                case 2:
                    {
                        double[] green = new double[pictures.Length];
                        for (int i = 0; i < pictures.Length; i++)
                        {
                            green[i] = pictures[i].PercentOfGreen;
                        }
                        return green;
                    }
                case 3:
                    {
                        double[] heights = new double[pictures.Length];
                        for (int i = 0; i < pictures.Length; i++)
                        {
                            heights[i] = pictures[i].Height;
                        }
                        return heights;
                    }
                case 4:
                    {
                        double[] widths = new double[pictures.Length];
                        for (int i = 0; i < pictures.Length; i++)
                        {
                            widths[i] = pictures[i].Width;
                        }
                        return widths;
                    }
                case 5:
                    {
                        double[] years = new double[pictures.Length];
                        for (int i = 0; i < pictures.Length; i++)
                        {
                            years[i] = pictures[i].YearMap;
                        }
                        return years;
                    }
                default:
                    {
                        double[] zero = new double[0];
                        return zero;
                    }
            }
        }
        private string CreateTitleAxis(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        return "Красный цвет";
                    }
                case 1:
                    {
                        return "Синий цвет";
                    }
                case 2:
                    {
                        return "Зеленый цвет";
                    }
                case 3:
                    {
                        return "Высота";
                    }
                case 4:
                    {
                        return "Ширина";
                    }
                case 5:
                    {
                        return "Год создания";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
    }
}

