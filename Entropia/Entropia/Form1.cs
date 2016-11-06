using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Dynamic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;


namespace Entropia
{






    public partial class Form1 : Form
    {
        IniFile INI = new IniFile("config.ini");

       public static String FileName = "";
       public static ArrayList Value = new ArrayList();
       public static ArrayList Value2 = new ArrayList();
    //   public static List<double> list = new List<double>();
       public static Boolean flag = false;
       public static double UpBound = 0, BotBound = 0;
       public static int  iterNumber = 0;
       public static double sigmNumber = 0;
       public static bool checkValue = true;
       public static double[] sho;
       public static Color color;
       public static double[] pohidnaS;
       public static double[] pohidnaArray;
       public static double[] pohidnaSave;
       public static double[] clearSignal;
       public static bool ifcalc = false;

       protected override void OnFormClosing(FormClosingEventArgs e)
       {
           auto_write();
           //Application.Exit();
       }


       private void auto_read()
       {
           
           string iterstring = INI.ReadINI("Entr", "iterCount");
           if (iterstring == "")
           {
               MessageBox.Show("Значение количества иттераций задано по умолчанию");
               checkIter0.Checked = true;
           }
           else
           {
               if (iterstring == "0")
               {
                   checkIter0.Checked = true;
               }
               else if (iterstring == "1")
               {
                   checkIter1.Checked = true;
               }
               else
               {
                   checkIter2.Checked = true;
               }
           }


           if (INI.ReadINI("Entr", "sigmCount") == "")
           {
               MessageBox.Show("Значение количества СКО задано по умолчанию");
               boxSigm.SelectedIndex = 4;
           }
           else
           {
               boxSigm.SelectedIndex = int.Parse(INI.ReadINI("Entr", "sigmCount"));
           }


           if (INI.ReadINI("Entr", "tabControl") == "")
           {
              // MessageBox.Show("Значение количества СКО задано по умолчанию");
               tabControl1.SelectedIndex = 0;
           }
           else
           {
               tabControl1.SelectedIndex = int.Parse(INI.ReadINI("Entr", "tabControl"));
           }



           FileName = INI.ReadINI("Entr", "FileName");

           if (INI.ReadINI("Entr", "txtPorog") == "")
           {
               MessageBox.Show("Значение порога задано по умолчанию");
               txtPorog.Text = "50";
           }
           else
           {
               txtPorog.Text = INI.ReadINI("Entr", "txtPorog");
           }


           string porogstring = INI.ReadINI("Entr", "porogType");



           if (porogstring == "")
           {
               MessageBox.Show("Тип порога задан по умолчанию");
               rbAbs.Checked = true;
           }
           else
           {
               if (porogstring == "abs")
                   rbAbs.Checked = true;
               else
                   rbProc.Checked = true;
           }

           if (INI.ReadINI("Entr", "boxEntr") == "")
           {
               MessageBox.Show("Тип энтропии задан по умолчанию");
               BoxEntr.SelectedIndex = 0;
           }
           else
           {
               BoxEntr.SelectedIndex = int.Parse(INI.ReadINI("Entr", "boxEntr"));
           }

           if (INI.ReadINI("Entr", "txtOkno") == "")
           {
               MessageBox.Show("Значение окна задано по умолчанию");
               txtOkno.Text = "50";
           }
           else
           {
               txtOkno.Text = INI.ReadINI("Entr", "txtOkno");
           }


           if (INI.ReadINI("Entr", "txtSglaj") == "")
           {
               MessageBox.Show("Значение окна сглаживания задано по умолчанию");
               txtSglaj.Text = "11";
           }
           else
           {
               txtSglaj.Text = INI.ReadINI("Entr", "txtSglaj");
           }


           if (INI.ReadINI("Entr", "checkLine") == "")
           {
             //  MessageBox.Show("Значение окна сглаживания задано по умолчанию");
               checkLine.Checked = true;
           }
           else
           {
               checkLine.Checked = bool.Parse(INI.ReadINI("Entr", "checkLine")); // TRYYY
           }



           if (INI.ReadINI("Entr", "checkPoint") == "")
           {
               //  MessageBox.Show("Значение окна сглаживания задано по умолчанию");
               checkPoint.Checked = true;
           }
           else
           {
               checkPoint.Checked = bool.Parse(INI.ReadINI("Entr", "checkPoint")); // TRYYY
           }


           string analizstring = INI.ReadINI("Entr", "Analiz");

           if (analizstring == "")
           {
               MessageBox.Show("Вид анализа задан по умолчанию");
               rbVO.Checked = true;
           }
           else
           {
               if (analizstring == "por")
                   rbPor.Checked = true;
               else
                   rbVO.Checked = true;
           }


           color = Color.FromArgb(Convert.ToInt32(INI.ReadINI("Entr", "color")));
           

       }

       private void auto_write()
       {
           if (checkIter0.Checked == true)
           {
               INI.Write("Entr", "iterCount", "0");
           }
           else if (checkIter1.Checked == true)
           {
               INI.Write("Entr", "iterCount", "1");
           }
           else
           {
               INI.Write("Entr", "iterCount", "2");
           }

           INI.Write("Entr", "sigmCount", boxSigm.SelectedIndex.ToString());
           INI.Write("Entr", "tabControl", tabControl1.SelectedIndex.ToString());
           INI.Write("Entr", "FileName", FileName);
           INI.Write("Entr", "txtPorog", txtPorog.Text);

           if(rbAbs.Checked==true)
               INI.Write("Entr", "porogType", "abs");
           else
               INI.Write("Entr", "porogType", "proc");
           INI.Write("Entr", "boxEntr", BoxEntr.SelectedIndex.ToString());
           INI.Write("Entr", "txtOkno", txtOkno.Text);
           INI.Write("Entr", "txtSglaj", txtSglaj.Text);

           INI.Write("Entr", "checkLine", checkLine.Checked.ToString());
           INI.Write("Entr", "checkPoint", checkPoint.Checked.ToString());

           if (rbPor.Checked == true)
               INI.Write("Entr", "Analiz", "por");
           else
               INI.Write("Entr", "Analiz", "vo");

           INI.Write("Entr", "color", color.ToArgb().ToString());

           
           
       }


        public Form1()
        {
            InitializeComponent();
            
            this.mainChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            this.mainChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            mainChart.BorderlineDashStyle = ChartDashStyle.Solid;
            chartClear.BorderlineDashStyle = ChartDashStyle.Solid;


            toolTip1.SetToolTip(btnSaveSE, "Сохранить сигнал скорости изменения энтропии в Файл");
            toolTip1.SetToolTip(btnSaveE, "Сохранить сигнал энтропии в Файл");
            toolTip1.SetToolTip(btnSaveS, "Сохранить результат очистки в Файл");
            this.checkIter0.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            this.checkIter1.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            this.checkIter2.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            this.boxSigm.TextChanged += boxSigm_TextChanged;



            this.mainChart.MouseWheel += mainChart_MouseWheel;
            this.mainChart.MouseLeave += mainChart_MouseLeave;
            this.mainChart.MouseEnter += mainChart_MouseEnter;

            this.chartClear.MouseWheel += chartClear_MouseWheel;
            this.chartClear.MouseEnter += chartClear_MouseEnter;
            this.chartClear.MouseLeave += chartClear_MouseLeave;

            this.chartEntr1.MouseWheel += chartEntr1_MouseWheel;
            this.chartEntr1.MouseEnter += chartEntr1_MouseEnter;
            this.chartEntr1.MouseLeave += chartEntr1_MouseLeave;

            this.chartEntr2.MouseWheel += chartEntr2_MouseWheel;
            this.chartEntr2.MouseEnter += chartEntr2_MouseEnter;
            this.chartEntr2.MouseLeave += chartEntr2_MouseLeave;


            this.checkIter0.CheckedChanged += checkIter0_CheckedChanged;
            checkIter0.Checked = true;
            boxSigm.SelectedIndex = 0;

            if (checkIter0.Checked)
            {

                boxSigm.Enabled = false;

            }
            else
            {

                boxSigm.Enabled = true;
            }

            auto_read();

            ifcalc = true;

            if (FileName != "")
                calculate();
            else
            {

                double[] tm = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double[] tm1 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                // chartClear.ChartAreas[0].AxisY.

                mainChart.ChartAreas[0].AxisY.Interval = 100;
                mainChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
                this.mainChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                this.mainChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                chartClear.ChartAreas[0].AxisY.Interval = 100;
                chartClear.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
                this.chartClear.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                this.chartClear.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
               // chartEntr1.ChartAreas[0].AxisY.Interval = 100;
                chartEntr1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
                this.chartEntr1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                this.chartEntr1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
               // chartEntr2.ChartAreas[0].AxisY.Interval = 100;
                chartEntr2.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
                this.chartEntr2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                this.chartEntr2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;


                mainChart.Series["ser1"].Points.DataBindXY(tm1, tm);
                chartEntr1.Series["Series1"].Points.DataBindXY(tm1, tm);
                chartEntr2.Series["Series1"].Points.DataBindXY(tm1, tm);
                chartClear.Series["ser1"].Points.DataBindXY(tm1, tm);

                

                //btnSaveS.Visible = false;

            }


            labStatus.Text = FileName;
            labStatus1.Text = FileName;
            labStatus2.Text = FileName;

          

        }

        void chartEntr2_MouseLeave(object sender, EventArgs e)
        {
            if (chartEntr2.Focused)
                chartEntr2.Parent.Focus();

        }

        void chartEntr2_MouseEnter(object sender, EventArgs e)
        {
            if (!chartEntr2.Focused)
                chartEntr2.Focus();

        }

        void chartEntr2_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chartEntr2.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chartEntr2.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {

                    double xMin = Math.Round(chartEntr2.ChartAreas[0].AxisX.ScaleView.ViewMinimum, 2);
                    double xMax = Math.Round(chartEntr2.ChartAreas[0].AxisX.ScaleView.ViewMaximum, 2);
                    // double yMin = Math.Round( chartEntr1.ChartAreas[0].AxisY.ScaleView.ViewMinimum,2);
                    // double yMax = Math.Round(chartEntr1.ChartAreas[0].AxisY.ScaleView.ViewMaximum, 2);

                    double posXStart = Math.Round(chartEntr2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2, 2);
                    double posXFinish = Math.Round(chartEntr2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 2, 2);
                    // double posYStart =  Math.Round(chartEntr1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2,2);
                    //double posYFinish = Math.Round(chartEntr1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 2, 2);




                    chartEntr2.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    // chartEntr1.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);

                    // chartEntr1.ChartAreas[0].AxisY.Interval = 0.05;

                    // chartEntr1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

                }
            }
            catch { }
        }

        void chartEntr1_MouseLeave(object sender, EventArgs e)
        {
            if (chartEntr1.Focused)
                chartEntr1.Parent.Focus();

        }

        void chartEntr1_MouseEnter(object sender, EventArgs e)
        {
            if (!chartEntr1.Focused)
                chartEntr1.Focus();

        }

        void chartEntr1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chartEntr1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chartEntr1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                   
                    double xMin = Math.Round( chartEntr1.ChartAreas[0].AxisX.ScaleView.ViewMinimum,2);
                    double xMax = Math.Round( chartEntr1.ChartAreas[0].AxisX.ScaleView.ViewMaximum,2);
                   // double yMin = Math.Round( chartEntr1.ChartAreas[0].AxisY.ScaleView.ViewMinimum,2);
                   // double yMax = Math.Round(chartEntr1.ChartAreas[0].AxisY.ScaleView.ViewMaximum, 2);
                    
                    double posXStart =  Math.Round(chartEntr1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2,2);
                    double posXFinish =  Math.Round(chartEntr1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 2,2);
                   // double posYStart =  Math.Round(chartEntr1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2,2);
                    //double posYFinish = Math.Round(chartEntr1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 2, 2);
                    
                   


                    chartEntr1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                   // chartEntr1.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                    
                   // chartEntr1.ChartAreas[0].AxisY.Interval = 0.05;

                   // chartEntr1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

                }
            }
            catch { }

        }

        void chartClear_MouseLeave(object sender, EventArgs e)
        {
            if (chartClear.Focused)
                chartClear.Parent.Focus();

        }

        void chartClear_MouseEnter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!chartClear.Focused)
                chartClear.Focus();

        }

        void chartClear_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chartClear.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chartClear.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = (int)chartClear.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = (int)chartClear.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = (int)chartClear.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = (int)chartClear.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = (int)chartClear.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2;
                    double posXFinish = (int)chartClear.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 2;
                    double posYStart = (int)chartClear.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2;
                    double posYFinish = (int)chartClear.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 2;

                    chartClear.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    chartClear.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);

                    chartClear.ChartAreas[0].AxisY.Interval = Math.Round((maxi(Value, true) - mini(Value, true)) / 7);

                    chartClear.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
                }
            }
            //throw new NotImplementedException();
            catch { }
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton radioButton = sender as RadioButton;

            if (this.checkIter0.Checked)
            {
                iterNumber = 0;
                if (FileName != "")
                {
                    // sigmNumber = Double.Parse(boxSigm.Text);
                    calculate();
                    btnSaveS.Visible = true;
                }
                this.mainChart.Series["ser2"].Color = Color.Transparent;
                this.mainChart.Series["ser3"].Color = Color.Transparent;

            }
            else if (this.checkIter1.Checked)
            {
                iterNumber = 1;
                if (FileName != "")
                {
                    // sigmNumber = Double.Parse(boxSigm.Text);
                    calculate();
                    btnSaveS.Visible = true;
                }
            }
            else if (this.checkIter2.Checked)
            {
                iterNumber = 2;
                if (FileName != "")
                {
                    // sigmNumber = Double.Parse(boxSigm.Text);
                    calculate();
                    btnSaveS.Visible = true;
                }
            }



        }

        private void boxSigm_TextChanged(object sender, EventArgs e)
        {
            //sigmNumber = Double.Parse(boxSigm.Text);
            if (FileName != "")
            {
                calculate();
                btnSaveS.Visible = true;
            }




        }

        void checkIter0_CheckedChanged(object sender, EventArgs e)
        {
            if (checkIter0.Checked)
            {
                boxSigm.Enabled = false;

            }
            else
            {
                boxSigm.Enabled = true;
            }
        }

        public static double sred(double[] mas)
        {
            double sr = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                sr = sr + mas[i];
            }
            sr = sr / mas.Length;
            return sr;
        }

        public static double sko(double[] mass)
        {
            double result = 0;
            double sred = 0;

            for (int i = 0; i < mass.Length; i++)
            {
                sred = sred + mass[i];
            }
            sred = sred / mass.Length;
            //System.out.println("sred "+sred);

            for (int i = 0; i < mass.Length; i++)
            {
                result = result + Math.Pow((mass[i] - sred), 2);
            }
            result = Math.Sqrt(result / (mass.Length - 1)); // TUT -1




            return result;
        }



       public static double [] medf (double [] x1, double sigm){

        double skoValue = 0;
        skoValue = sko(x1);
        double sredValue = sred(x1);
        ArrayList masbuf = new ArrayList();

        for (int i = 0; i <x1.Length; i++) {
            if(x1[i]>= (sredValue - sigm*skoValue) && x1[i]<= (sredValue + sigm*skoValue))
                masbuf.Add(x1[i]);
        }
        double [] mas1 = new double[masbuf.Count];
        for (int i = 0; i <mas1.Length; i++) {
            mas1[i]=(double)masbuf[i];
        }
        return mas1;    
    }


        void mainChart_MouseEnter(object sender, EventArgs e)
        {
            if (!mainChart.Focused)
                mainChart.Focus();
        }

        void mainChart_MouseLeave(object sender, EventArgs e)
        {
            if (mainChart.Focused)
                mainChart.Parent.Focus();
        }

        private void mainChart_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    mainChart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    mainChart.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = (int)mainChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = (int)mainChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = (int)mainChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = (int)mainChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = (int)mainChart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2;
                    double posXFinish = (int)mainChart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 2;
                    double posYStart = (int)mainChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2;
                    double posYFinish = (int)mainChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 2;

                    mainChart.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    mainChart.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);

                    mainChart.ChartAreas[0].AxisY.Interval = Math.Round((maxi(Value, true) - mini(Value, true)) / 7);
                  
                    mainChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

                }
            }
            catch { }
        }

        public static double mini(ArrayList mas, bool check)
        {
            string[] array;
            double[] mas1;
            if (check)
            {
                array = mas.ToArray(typeof(string)) as string[];
                mas1 = new double[mas.Count];
                for (int i = 0; i < mas1.Length; i++)
                {
                    mas1[i] = double.Parse(array[i], System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else
            {
                mas1 = mas.ToArray(typeof(double)) as double[];
            }


            double result = mas1[0];

            for(int i=1;i<mas1.Length;i++)
            {
                if (mas1[i] < result)
                    result = mas1[i];
            }
            return result;
        }


        public static double maxi(ArrayList mas, bool check)
        {
            string[] array;
            double[] mas1;
            if (check)
            {
                array = mas.ToArray(typeof(string)) as string[];
                mas1 = new double[mas.Count];
                for (int i = 0; i < mas1.Length; i++)
                {
                    mas1[i] = double.Parse(array[i], System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else
            {
                mas1 = mas.ToArray(typeof(double)) as double[];
            }

            double result = mas1[0];

            for (int i = 1; i < mas1.Length; i++)
            {
                if (mas1[i] > result)
                    result = mas1[i];
            }
            return result;
        }




        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void mainChart_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void labSettings_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.dtt|Text File|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               // txtPath.Text += ofd.FileName;
                flag = true;
                FileName = ofd.FileName;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void labSettings_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void chartClear_Click(object sender, EventArgs e)
        {

        }

        private void mainChart_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            /*
            Value.Clear();
            Value2.Clear();
            if (!flag)
            {
                MessageBox.Show("Выберите файл");
                return;
            }

            string[] lines = System.IO.File.ReadAllLines(@FileName);

            // Display the file contents by using a foreach loop. 
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Value.Add(line);
                // MessageBox.Show("\t" + line);
            }



            string[] array = Value.ToArray(typeof(string)) as string[];
            //  list = Value.Cast<double>().ToList();
            double[] bufferArray = new double[Value.Count];
            for (int i = 0; i < Value.Count; i++)
            {
                bufferArray[i] = double.Parse(array[i], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!checkIter0.Checked)
            {
                if (checkIter1.Checked)
                    iterNumber = 1;
                else
                    iterNumber = 2;


                if (checkSigm1.Checked)
                    sigmNumber = 1;
                else
                {
                    if (checkSigm2.Checked)
                        sigmNumber = 2;
                    else
                    {
                        if (checkSigm3.Checked)
                            sigmNumber = 3;
                        else 
                        {
                            if (checkSigm15.Checked)
                                sigmNumber = 1.5;
                            else
                                sigmNumber = 2.5;
                        }
                    }
                }
                
                UpBound = sred(bufferArray) + sko(bufferArray) * sigmNumber;
                BotBound = sred(bufferArray) - sko(bufferArray) * sigmNumber;

                List<double> buff = new List<double>();

                double[] promArray = medf(bufferArray, sigmNumber);
                double[] promArray2;


                for (int i = 0; i < promArray.Length; i++)
                    Value2.Add(promArray[i]);

                if (iterNumber == 2) // 2 itteracii
                {
                    promArray2 = medf(promArray, sigmNumber);
                    UpBound = sred(promArray) + sko(promArray) * sigmNumber;
                    BotBound = sred(promArray) - sko(promArray) * sigmNumber;
                    Value2.Clear();
                    for (int i = 0; i < promArray2.Length; i++)
                        Value2.Add(promArray2[i]);
                }
                checkValue = false;
            }
            else
            {
                for (int i = 0; i < Value.Count; i++)
                    Value2.Add(Value[i]);

                checkValue = true;

               // chartClear.Series["ser2"].Dispose();
               // chartClear.Series["ser3"].Dispose();
            }


            //  MessageBox.Show("mini: "+mini(Value2,false));
            //  MessageBox.Show("mini: " + maxi(Value));

            int[] arrX = new int[Value.Count];
            double[] skoA = new double[Value.Count];
            double[] skoB = new double[Value.Count];

            for (int i = 0; i < arrX.Length; i++)
            {
                arrX[i] = i;
                skoA[i] = UpBound;
                skoB[i] = BotBound;

            }

            int[] arrX2 = new int[Value2.Count];
            double[] skoA2 = new double[Value2.Count];
            double[] skoB2 = new double[Value2.Count];

            for (int i = 0; i < arrX2.Length; i++)
            {
                arrX2[i] = i;
                skoA2[i] = UpBound;
                skoB2[i] = BotBound;

            }


            for (int i = 0; i < Value.Count; i++)
            {
                //  MessageBox.Show("i: " + skoA[i].ToString());
            }

            this.mainChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            this.mainChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            mainChart.ChartAreas[0].AxisX.Minimum = 0.0;
            mainChart.ChartAreas[0].AxisY.Minimum = Math.Round(mini(Value, true)) - 100;
            mainChart.ChartAreas[0].AxisY.Maximum = Math.Round(maxi(Value, true)) + 100; ;

            mainChart.Series["ser1"].Points.DataBindXY(arrX, Value);
            mainChart.Series["ser2"].Points.DataBindXY(arrX, skoA);
            mainChart.Series["ser2"].Color = Color.Red;
            mainChart.Series["ser3"].Points.DataBindXY(arrX, skoB);
            mainChart.Series["ser3"].Color = Color.Red;
            mainChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            
            mainChart.ChartAreas[0].AxisY.Title = "RR, ms";

            this.chartClear.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            this.chartClear.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;

            chartClear.ChartAreas[0].AxisX.Minimum = 0.0;
            chartClear.ChartAreas[0].AxisY.Minimum = Math.Round(mini(Value2, checkValue)) - 100;
            chartClear.ChartAreas[0].AxisY.Maximum = Math.Round(maxi(Value2, checkValue)) + 100; ;


            chartClear.Series["ser1"].Points.DataBindXY(arrX2, Value2);
            chartClear.Series["ser2"].Points.DataBindXY(arrX2, skoA2);
            chartClear.Series["ser2"].Color = Color.Red;
            chartClear.Series["ser3"].Points.DataBindXY(arrX2, skoB2);
            chartClear.Series["ser3"].Color = Color.Red;
            chartClear.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            
            chartClear.ChartAreas[0].AxisY.Title = "RR, ms";

*/

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(ifcalc && (txtPorog.Text!= ""))
            calculate();
        }

       


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileChooser();
            calculate();
            btnSaveS.Visible = true;
            
        }

        public void fileChooser ()
        {
              OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.dtt|Text File|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //txtPath.Text += ofd.FileName;
                flag = true;
                FileName = ofd.FileName;
                labStatus.Text = FileName;
                labStatus1.Text = FileName;
                labStatus2.Text = FileName;
            }
        }

        private void calculate()
        {

            if (txtOkno.Text == "" || txtPorog.Text == "" || txtSglaj.Text == "")
            {
              //  MessageBox.Show("Все поля должны быть заполнены");
                return;
            }

            Value.Clear();
            Value2.Clear();
            if (FileName=="")
            {
               // MessageBox.Show("Выберите файл");
                return;
            }
            string[] lines = null ;

            try
            {
                lines = System.IO.File.ReadAllLines(@FileName);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Выбраный файл не найден. Выберите файл заново!");
                return;
            }

            // Display the file contents by using a foreach loop. 
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Value.Add(line);
                // MessageBox.Show("\t" + line);
            }



            string[] array = Value.ToArray(typeof(string)) as string[];
            //  list = Value.Cast<double>().ToList();
            double[] bufferArray = new double[Value.Count];
            for (int i = 0; i < Value.Count; i++)
            {
                bufferArray[i] = double.Parse(array[i], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!checkIter0.Checked)
            {
                if (checkIter1.Checked)
                    iterNumber = 1;
                else
                    iterNumber = 2;

                switch (boxSigm.Text)
                {
                    case "0.5":
                        sigmNumber = 0.5;
                        break;
                    case "0.25":
                        sigmNumber = 0.25;
                        break;
                    case "0.75":
                        sigmNumber = 0.75;
                        break;
                    case "1.25":
                        sigmNumber = 1.25;
                        break;
                    case "1.75":
                        sigmNumber = 1.75;
                        break;
                    case "2.25":
                        sigmNumber = 2.25;
                        break;
                    case "2.75":
                        sigmNumber = 2.75;
                        break;
                    case "1":
                        sigmNumber = 1;
                        break;
                    case "1.5":
                        sigmNumber = 1.5;
                        break;
                    case "2":
                        sigmNumber = 2;
                        break;
                    case "2.5":
                        sigmNumber = 2.5;
                        break;
                    case "3":
                        sigmNumber = 3;
                        break;

                }

                UpBound = sred(bufferArray) + sko(bufferArray) * sigmNumber;
                BotBound = sred(bufferArray) - sko(bufferArray) * sigmNumber;

                List<double> buff = new List<double>();

                double[] promArray = medf(bufferArray, sigmNumber);
                double[] promArray2;


                for (int i = 0; i < promArray.Length; i++)
                    Value2.Add(promArray[i]);

                if (iterNumber == 2) // 2 itteracii
                {
                    promArray2 = medf(promArray, sigmNumber);
                    UpBound = sred(promArray) + sko(promArray) * sigmNumber;
                    BotBound = sred(promArray) - sko(promArray) * sigmNumber;
                    Value2.Clear();
                    for (int i = 0; i < promArray2.Length; i++)
                        Value2.Add(promArray2[i]);
                }
                checkValue = false;
            }
            else
            {
                for (int i = 0; i < Value.Count; i++)
                    Value2.Add(Value[i]);

                checkValue = true;

                // chartClear.Series["ser2"].Dispose();
                // chartClear.Series["ser3"].Dispose();
            }


            //  MessageBox.Show("mini: "+mini(Value2,false));
            //  MessageBox.Show("mini: " + maxi(Value));

            int[] arrX = new int[Value.Count];
            double[] skoA = new double[Value.Count];
            double[] skoB = new double[Value.Count];

            for (int i = 0; i < arrX.Length; i++)
            {
                arrX[i] = i;
                skoA[i] = UpBound;
                skoB[i] = BotBound;

            }

            int[] arrX2 = new int[Value2.Count];
            double[] skoA2 = new double[Value2.Count];
            double[] skoB2 = new double[Value2.Count];

            for (int i = 0; i < arrX2.Length; i++)
            {
                arrX2[i] = i;
                skoA2[i] = UpBound;
                skoB2[i] = BotBound;

            }





            for (int i = 0; i < Value.Count; i++)
            {
                //  MessageBox.Show("i: " + skoA[i].ToString());
            }

            this.mainChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            this.mainChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            mainChart.ChartAreas[0].AxisX.Minimum = 0.0;
            mainChart.ChartAreas[0].AxisY.Minimum = Math.Round(mini(Value, true)) - 100;
            mainChart.ChartAreas[0].AxisY.Maximum = Math.Round(maxi(Value, true)) + 100; ;

            mainChart.Series["ser1"].Points.DataBindXY(arrX, Value);
            mainChart.Series["ser2"].Points.DataBindXY(arrX, skoA);
            mainChart.Series["ser2"].Color = Color.Red;
            mainChart.Series["ser3"].Points.DataBindXY(arrX, skoB);
            mainChart.Series["ser3"].Color = Color.Red;
            mainChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            mainChart.ChartAreas[0].AxisY.Interval = Math.Round((maxi(Value, true) - mini(Value, true)) / 7);
            chartClear.ChartAreas[0].AxisY.Interval = Math.Round((maxi(Value, true) - mini(Value, true)) / 7);



            mainChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
            mainChart.ChartAreas[0].AxisY.Title = "RR, мс";
            mainChart.ChartAreas[0].AxisX.Title = "i";

            this.chartClear.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            this.chartClear.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;

            chartClear.ChartAreas[0].AxisX.Minimum = 0.0;
            chartClear.ChartAreas[0].AxisY.Minimum = Math.Round(mini(Value, true)) - 100;
            chartClear.ChartAreas[0].AxisY.Maximum = Math.Round(maxi(Value, true)) + 100; ;

            chartClear.Series["ser1"].Points.DataBindXY(arrX2, Value2);
            chartClear.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartClear.ChartAreas[0].AxisY.Title = "RR, мс";
            chartClear.ChartAreas[0].AxisX.Title = "i";

            //chartClear.ChartAreas[0].AxisY.Interval = 100;
            chartClear.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

            mainChart.BorderlineDashStyle = ChartDashStyle.Solid;
            chartClear.BorderlineDashStyle = ChartDashStyle.Solid;

            mainChart.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            mainChart.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chartClear.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartClear.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;



            double[] forsko;

            if (checkIter0.Checked == false)
                forsko = Value2.ToArray(typeof(double)) as double[];
            else
            {
                string[] arrayParse1;

                arrayParse1 = Value2.ToArray(typeof(string)) as string[];
                forsko = new double[Value2.Count];
                for (int i = 0; i < forsko.Length; i++)
                {
                    forsko[i] = double.Parse(arrayParse1[i], System.Globalization.CultureInfo.InvariantCulture);
                    //  MessageBox.Show(clearSignal[i].ToString());
                }
            }


            double porog =0;

            if (rbAbs.Checked == true)
            {
                porog = double.Parse(txtPorog.Text);
            }
            else
            {
                porog = double.Parse(txtPorog.Text)*sko(forsko)/100;
            }
            
            // MessageBox.Show(porog.ToString());
            int okno = int.Parse(txtOkno.Text);

            sho = shenEntr(Value2, okno, porog);// MASSIV ENTROPII
            if (sho == null)
            {
                this.chartEntr1.Series["Series1"].Color = Color.Transparent;
                this.chartEntr2.Series["Series1"].Color = Color.Transparent;
                return;
            }
            else
            {
                this.chartEntr1.Series["Series1"].Color = color;
                this.chartEntr2.Series["Series1"].Color = color;
            }


            mainChart.Series["ser1"].Color = color;
            chartClear.Series["ser1"].Color = color;


            int[] entrCounter = new int[sho.Length];
            for (int i = 0; i < sho.Length; i++)
            {
                entrCounter[i] = i;
                   //  MessageBox.Show("# " + i + ": " + sho[i]);
            }


            pohidnaArray = pohidnaEntr(sho); // PROIZVODNAYA ENTROPII


          //  for (int i = 0; i < pohidnaArray.Length; i++)
              //  MessageBox.Show("poh "+pohidnaArray[i].ToString());


            int[] entrCounter2 = new int[pohidnaArray.Length];
            for (int i = 0; i < pohidnaArray.Length; i++)
            {
                entrCounter2[i] = i;
                //     MessageBox.Show("# " + i + ": " + sho[i]);
            }





            pohidnaS = skolzSglaj(pohidnaArray, int.Parse(txtSglaj.Text));

            
            List <double> pohView = new List<double>();

            int bufCount = int.Parse(txtSglaj.Text) + 3;

            if (pohidnaS == null)
                return;

            for (int i = 0; i < bufCount; i++)
                pohView.Add(0);

            for (int i = 0; i < pohidnaS.Length; i++)
                pohView.Add(pohidnaS[i]);

            for (int i = 0; i < bufCount; i++)
                pohView.Add(0);



            double [] pohidnaView = pohView.ToArray();



            int[] counterS = new int[pohidnaView.Length];
            for(int i=0;i<counterS.Length;i++)
            {
                counterS[i] = i;
               // MessageBox.Show(pohidnaS[i].ToString());
            }
          //  pohidnaSave = pohidnaS;
            
           
            //double[] savePohidna = pohidnaS;



            chartEntr1.ChartAreas[0].AxisX.Minimum = 0.0;
            chartEntr2.ChartAreas[0].AxisX.Minimum = 0.0;

            // this.chartEntr1.Series["Series1"].Points.DataBindXY(test2, test1);
            chartEntr1.Series["Series1"].Points.DataBindXY(entrCounter, sho);
            chartEntr2.Series["Series1"].Points.DataBindXY(counterS, pohidnaView);

            chartEntr1.ChartAreas[0].AxisX.Title = "i";
            chartEntr1.ChartAreas[0].AxisY.Title = "Hi, %";
            chartEntr1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartEntr1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;

            chartEntr2.ChartAreas[0].AxisX.Title = "i";
            chartEntr2.ChartAreas[0].AxisY.Title = "dH/di, %/c";

            chartEntr2.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartEntr2.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;


            chartEntr1.BorderlineDashStyle = ChartDashStyle.Solid;
            chartEntr2.BorderlineDashStyle = ChartDashStyle.Solid;

            chartEntr1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartEntr1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chartEntr2.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartEntr2.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;


            this.chartEntr1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            this.chartEntr1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            this.chartEntr2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            this.chartEntr2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;

             //chartEntr1.ChartAreas[0].AxisX.Minimum = 0.0;
             chartEntr1.ChartAreas[0].AxisY.Minimum = Math.Round((minArray(sho)-(maxArray(sho)-minArray(sho))*0.3));
             chartEntr1.ChartAreas[0].AxisY.Maximum = Math.Round((maxArray(sho) + (maxArray(sho) - minArray(sho)) * 0.3));
             chartEntr1.ChartAreas[0].AxisY.Interval = 10;
             chartEntr1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
             //chartEntr2.ChartAreas[0].AxisY.Minimum = Math.Round((minArray(pohidnaView) - (maxArray(pohidnaView) - minArray(pohidnaView)) * 0.3),2);
            // chartEntr2.ChartAreas[0].AxisY.Maximum = Math.Round((maxArray(pohidnaView) + (maxArray(pohidnaView) - minArray(pohidnaView)) * 0.3),2);
            // chartEntr1.ChartAreas[0].AxisY.Maximum = Math.Round(( + (maxArray(sho) - minArray(sho)) * 0.1)); ;
            // chartEntr1.ChartAreas[0].AxisY.Maximum = Math.Round(maxArray(sho)) + 1;
           //  chartEntr1.ChartAreas[0].AxisY.Minimum = Math.Round(minArray(sho)) - 1;

            //chartEntr1.ChartAreas[0].AxisY.Minimum = 1.2;
            //  chartEntr1.ChartAreas[0].AxisY.Maximum = 2.5; ;
            //  chartEntr1.ChartAreas[0].AxisY.Interval = 0.5;





            //  double[] pohidnaS = skolzSglaj(pohidnaArray, int.Parse(txtSglaj.Text));



          


            


            double[] normPohidna = norma(pohidnaS);

            double[] normNewsho = norma(sho);



          //  for (int i = 0; i < normNewsho.Length; i++)
                //MessageBox.Show("## " + normNewsho[i]);


            double[] newsho = new double[normNewsho.Length - 6 - 2 * int.Parse(txtSglaj.Text)];

         //   for (int i = 0; i < normNewsho.Length; i++)
          //      MessageBox.Show("# " + normNewsho[i]);

            int jj = 0;
            for (int i = 3 + int.Parse(txtSglaj.Text); i < sho.Length - 3 - int.Parse(txtSglaj.Text); i++)
            {
               // MessageBox.Show("i " + i + "  " + normNewsho[i].ToString());
                newsho[jj] = normNewsho[i];
                //newsho[jj] = sho[i];
                jj++;
            }



           // for (int i = 0; i < newsho.Length; i++)
              //  MessageBox.Show("# " + newsho[i]);

           // MessageBox.Show("sho " + sho.Length.ToString());
           // MessageBox.Show("newsho "+newsho.Length.ToString());
           // MessageBox.Show("pohidna "+normPohidna.Length.ToString());

            if (checkLine.Checked == false)
            {
                this.chartPortret.Series["Series1"].Color = Color.Transparent;
            }
            else
            {
                this.chartPortret.Series["Series1"].Color = color;
            }

            if (checkPoint.Checked == false)
            {
                this.chartPortret.Series["Series2"].Color = Color.Transparent;
            }
            else
            {
                this.chartPortret.Series["Series2"].Color = Color.Red;
            }


            chartPortret.Series["Series1"].Points.DataBindXY(normPohidna, newsho);
            chartPortret.Series["Series2"].Points.DataBindXY(normPohidna, newsho);


           // chartPortret.Series["Series1"].Points.DataBindXY(newsho, pohidnaS);
            chartPortret.ChartAreas[0].AxisY.Minimum = -0.05;
            chartPortret.ChartAreas[0].AxisX.Minimum = -0.05;
            chartPortret.ChartAreas[0].AxisX.Maximum = 1.05;
            chartPortret.ChartAreas[0].AxisY.Maximum = 1.25;

            chartPortret.ChartAreas[0].AxisX.Title = "dH/di, %/c";
            chartPortret.ChartAreas[0].AxisY.Title = "H(i), %";
            chartPortret.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartPortret.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;

            chartPortret2.Series["Series1"].Points.DataBindXY(normPohidna, newsho);
            chartPortret2.Series["Series2"].Points.DataBindXY(normPohidna, newsho);


            // chartPortret.Series["Series1"].Points.DataBindXY(newsho, pohidnaS);
            chartPortret2.ChartAreas[0].AxisY.Minimum = -0.05;
            chartPortret2.ChartAreas[0].AxisX.Minimum = -0.05;
            chartPortret2.ChartAreas[0].AxisX.Maximum = 1.05;
            chartPortret2.ChartAreas[0].AxisY.Maximum = 1.25;

            chartPortret2.ChartAreas[0].AxisX.Title = "d(H)/d(i), %/c";
            chartPortret2.ChartAreas[0].AxisY.Title = "H(i), %";
            chartPortret2.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartPortret2.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chartPortret2.Series["Series1"].Color = color;
            chartPortret2.Series["Series2"].Color = Color.Red;



        }

        private void BoxEntr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chartEntr2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private double[] shenEntr(ArrayList SArray, int Ssize, double step)
        {
            int x;
        //String sizeS=SBox.getSelectedItem().toString(); PARAMETR
       // int Ssize=Integer.parseInt(sizeS); PARAMETR
       //  MessageBox.Show("SARAAAAAAAAAAAAAAAAAAY SIZE "+SArray.Count);
        if (Ssize>SArray.Count)
        {
            MessageBox.Show(  "Размер окна превишает длину массива");
            return null;
           
        }          
        
        double [] s111=new double[SArray.Count-Ssize+1]; // massiv znacheniy
        double [] mass5=new double[Ssize]; // kvantovanuy ryad
        double [] sko5=new double[SArray.Count-Ssize+1];

        //double[] SArrayMas = SArray.ToArray(typeof(double)) as double[];


        

       
        double[] SArrayMas;

        if (checkIter0.Checked==true)
        {
            string[] array;
            array = SArray.ToArray(typeof(string)) as string[];
            SArrayMas = new double[SArray.Count];
            for (int i = 0; i < SArrayMas.Length; i++)
            {
                SArrayMas[i] = double.Parse(array[i], System.Globalization.CultureInfo.InvariantCulture);
            }

        }
        else
        {
            SArrayMas = SArray.ToArray(typeof(double)) as double[];
        }


        double mmin= SArrayMas[0];
        double mmax = SArrayMas[0];
         for(int i=1;i<SArrayMas.Length;i++)
         {
             if (SArrayMas[i]<=mmin)
                 mmin=SArrayMas[i];
             if (SArrayMas[i]>=mmax)
                 mmax=SArrayMas[i];
         }       
         
         double raznica = Math.Abs(mmax-mmin);
              // System.out.println("max "+mmax+" min "+mmin);
              // System.out.println("raznica "+raznica);
    //     MessageBox.Show("max "+mmax+" min "+mmin);
          //  MessageBox.Show("raznica "+raznica);

       
              // System.out.println("STEP "+step);
           //  MessageBox.Show("STEP "+step);
               
    
          
         
               
               x=(int) (raznica/step);
             //  System.out.println("xxxxxxxxx "+x);
         //   MessageBox.Show( "xxxxxxxxx "+x);
               
        for(int i=0;i<SArrayMas.Length-Ssize+1;i++)
        {
            
            for (int j=0;j<Ssize;j++)
            {
                mass5[j]=SArrayMas[i+j];
            }
            
            s111[i]=shen1(mass5,Ssize,x,step,mmin);
            sko5[i]=sko(mass5);
        }
        
        if (Math.Abs(s111[0])==0)
        {
            //JOptionPane.showMessageDialog( null, , JOptionPane.WARNING_MESSAGE );
            MessageBox.Show("Значение энтропии в первом окне = 0", "Предупреждение");
            return null;
        }



     //  for (int i = 0; i < s111.Length; i++)
          //  MessageBox.Show(s111[i].ToString());

        double[] result = new double[s111.Length];
        result[0] = 100;
        for(int i=1;i<result.Length;i++)
        {
            result[i] = (s111[i] * 100) / s111[0];
        }
        
        
         

  
    
            return result;
        }


        public static double shen1(double[] mass, int Ssize, int x, double step, double mmin)
        {


            //System.out.println(ko+" "+half1+" "+half2);



            // Vector se = new Vector();
            double[] see = new double[x];
            // System.out.println("XXXXXXXXXXXXXXXXXXXXXXXXXX "+x);
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 1; j <= x; j++)
                {
                    if (j == x)
                    {

                        if ((mass[i]) >= mmin + step * (j - 1) && mass[i] <= mmin + step * j)
                        {
                            see[j - 1] = see[j - 1] + 1;
                        }

                    }
                    else
                    {
                        if ((mass[i]) >= mmin + step * (j - 1) && mass[i] < mmin + step * j)
                        {
                            see[j - 1] = see[j - 1] + 1;
                        }

                    }


                }

         
            }

            double sum11 = 0;
            for (int i = 0; i < x; i++)
            {
                if (see[i] == 0)
                    continue;
                else
                    sum11 = sum11 + (see[i] / mass.Length) * Math.Log((see[i] / mass.Length),Math.E);
                
            }
         


            return -sum11;
        }


        private double[] pohidnaEntr(double[] mas)
        {
            double[] result = new double[mas.Length-6]; ;

            for (int i = 3; i < mas.Length - 3; i++)
            {
                result[i-3] = (mas[i + 3] - (mas[i + 2] * 9) + (mas[i + 1] * 45) - (mas[i - 1] * 45) + (mas[i - 2]*9) - mas[i - 3]) / 60;
            }

           // for(int i=0;i<result.Length;i++)
           // MessageBox.Show("op: "+result[i]);
            
            return result;
        }

        public static double[] norma(double[] x)
        {
            double [] x2 = new double [x.Length];
            double minx = 0;
            double maxx = 0;

            minx = minArray(x);
            maxx = maxArray(x);

            for (int i = 0; i < x.Length; i++)
            {

                x2[i] = (x[i] - minx) / (maxx - minx);
            }


            return x2;
        }

        private static double minArray(double[] array)
        {
            double min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                    min = array[i];
            }
            return min;
        }

        private static double maxArray(double[] array)
        {
            double max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                    max = array[i];
            }
            return max;
        }

        private double[] skolzSglaj(double[] mas, int okno)
        {
            if (okno > 200)
            {
                MessageBox.Show("Окно сглаживания принимает некрректное значение");
                return null;
            }

            double buffer = 0;
            double[] result = new double[mas.Length-okno*2];

            for (int i = okno; i < mas.Length - okno; i++)
            {
                buffer = 0;
                for (int j = i - okno; j <= i + okno; j++)
                {
                  //  MessageBox.Show("j "+j+" mas: "+mas[j]);
                    buffer = buffer + mas[j];
                   
                }
                result[i - okno] = buffer / (2 * okno - 1);
               // MessageBox.Show("res " + result[i - okno].ToString());
            }


            return result;
        }

        public static void saveFile(double[] mas, string name)
        {


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File|*.txt";
            sfd.FileName = name;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                sfd.DefaultExt = "@.txt";
                string[] lin = new string[mas.Length];

                for (int i = 0; i < mas.Length; i++)
                {
                    mas[i] = Math.Round(mas[i], 4);
                    lin[i] =   mas[i].ToString();
                }
                // WriteAllLines creates a file, writes a collection of strings to the file,
                // and then closes the file.  You do NOT need to call Flush() or Close().
                System.IO.File.WriteAllLines(@path, lin);
                
                MessageBox.Show("Данные успешно сохранены");
            }

           
        }

        private void btnSaveE_Click(object sender, EventArgs e)
        {
            //string[] lines = { "First line", "Second line", "Third line" };
            if (sho == null)
                MessageBox.Show("Данные не найдены");
            else
            saveFile(sho, "Ряд энтропии");
           
        }

        private void btnSaveSE_Click(object sender, EventArgs e)
        {
            if (pohidnaS == null)
                MessageBox.Show("Данные не найдены");
            else
            {
                saveFile(pohidnaS, "Ряд производной энтропии");
            }
                
           // MessageBox.Show("S " + pohidnaS.Length.ToString());
           // MessageBox.Show("array " + pohidnaArray.Length.ToString());

        }

        private void btnSaveS_Click(object sender, EventArgs e)
        {

            if (boxSigm.Enabled == true)
                clearSignal = Value2.ToArray(typeof(double)) as double[];
            else
            {
                string[] arrayParse;

                arrayParse = Value2.ToArray(typeof(string)) as string[];
                clearSignal = new double[Value2.Count];
                for (int i = 0; i < clearSignal.Length; i++)
                {
                    clearSignal[i] = double.Parse(arrayParse[i], System.Globalization.CultureInfo.InvariantCulture);
                    //  MessageBox.Show(clearSignal[i].ToString());
                }
            }
           // 


            if (clearSignal == null)
                MessageBox.Show("Данные не найдены");
            else
            {
                saveFile(clearSignal, "Очищенный входной сигнал");
            }
        }

        private void checkIter0_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkIter0.Checked == false)
                boxSigm.Enabled = true;
            else
                boxSigm.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLine.Checked == false)
            {
                this.chartPortret.Series["Series1"].Color = Color.Transparent;
            }
            else
            {
                this.chartPortret.Series["Series1"].Color = color;
            }
        }

        private void checkPoint_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void checkPoint_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkPoint.Checked == false)
            {
                this.chartPortret.Series["Series2"].Color = Color.Transparent;
            }
            else
            {
                this.chartPortret.Series["Series2"].Color = Color.Red;
            }
        }

        private void txtPorog_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        
        }

        private void txtOkno_TextChanged(object sender, EventArgs e)
        {
            if (ifcalc && (txtOkno.Text != ""))
                calculate();
        }

        private void txtSglaj_TextChanged(object sender, EventArgs e)
        {

           // MessageBox.Show(Value2.Count.ToString());
            if (ifcalc && (txtSglaj.Text != ""))
            {
                if (int.Parse(txtSglaj.Text) > 150)
            {
                MessageBox.Show("Окно сглаживания принимает некрректное значение");
            }
            else
             calculate();
            }
                    
            
        }

        private void txtOkno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void txtSglaj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void rbAbs_CheckedChanged(object sender, EventArgs e)
        {
            if(FileName!="" && txtPorog.Text!="" && ifcalc)
            calculate();
        }

        private void rbProc_CheckedChanged(object sender, EventArgs e)
        {
            if (FileName != "" && txtPorog.Text != "" && ifcalc)
                calculate();
        }

        private void выбратьЦветГрафиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorChoose.ShowDialog();
            color = colorChoose.Color;
            mainChart.Series["ser1"].Color = color;
            chartClear.Series["ser1"].Color = color;
            chartEntr1.Series["Series1"].Color = color;
            chartEntr2.Series["Series1"].Color = color;
            chartPortret.Series["Series1"].Color = color;
            chartPortret2.Series["Series1"].Color = color;
            
            

        }
    }
}
