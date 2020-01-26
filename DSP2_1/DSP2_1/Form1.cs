using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DSP2_1
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.DataVisualization.Charting.Series DataSer_1;
        System.Windows.Forms.DataVisualization.Charting.Series DataSer_2;
        System.Windows.Forms.DataVisualization.Charting.Series DataSer_3;

        int K, phi;
        double[] phc = new double[15];
        public Form1()
        {
            InitializeComponent();
            BuildGraph();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            BuildGraph();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            BuildGraph();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BuildGraph()
        {
            chart1.Series.Clear();
            DataSer_1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataSer_1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            DataSer_1.Color = Color.Red;
            DataSer_1.Name = "Погрешность СКЗ 1";
            DataSer_2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataSer_2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            DataSer_2.Color = Color.Blue;
            DataSer_2.Name = "Погрешность СКЗ 2";
            DataSer_3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataSer_3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            DataSer_3.Color = Color.Green;
            DataSer_3.Name = "Погрешность амплитуды";
            int N = 1024;
            K = (int)trackBar2.Value;
           // K = (int)3 * N / 4;
           // phi = (int)180/32;
            phi = (int)trackBar1.Value;
            label3.Text = Convert.ToString(phi);
            label4.Text = Convert.ToString(K);
            for (int M = K; M <= 2 * N; M++)
            {
                double a = M * 2 * Math.PI / N;

                double rms_1 = 0, rms_2 = 0;
                double Re = 0, Im = 0;
                for (int n = 0; n <= M; n++)
                {
                    double t = Math.Sin(2 * Math.PI * n / N + (double)phi / 180 * Math.PI);
                    rms_1 += Math.Pow(t, 2);
                    rms_2 += t;

                    double t2 = Math.Sin(2 * Math.PI * n / N + (double)phi / 180 * Math.PI);
                    Re += t * Math.Cos(2 * Math.PI * n / M);
                    Im += t * Math.Sin(2 * Math.PI * n / M);
                }

                //double Xk = t2;
                DataSer_3.Points.AddXY(M, 1 - Math.Sqrt(Math.Pow(2 * Re / M, 2) + Math.Pow(2 * Im / M, 2)));
                DataSer_1.Points.AddXY(M, 0.707 - Math.Sqrt(rms_1 / (M + 1)));
                DataSer_2.Points.AddXY(M, 0.707 - (Math.Sqrt(rms_1 / (M + 1) - Math.Pow(rms_2 / (M + 1), 2))));
            }
            chart1.ResetAutoValues();
            chart1.Series.Add(DataSer_1);
            chart1.Series.Add(DataSer_2);
            chart1.Series.Add(DataSer_3);

        }

    }
}
