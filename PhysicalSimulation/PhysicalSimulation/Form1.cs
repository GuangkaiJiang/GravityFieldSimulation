using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicalSimulation
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        List<Circle> circles=new List<Circle>();
        int FPS = 60;
        double elastic = 0.90;
        int numberOfCircle=64;
        int R = 10;
        double M = 6e12;
        double T = 1;
        double G = 6.67e-11;
        Random rnd = new Random();
        double limita = 6.67 * 1 / 100;
        public Form1()
        {
            InitializeComponent();
            bitmap=new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Console.WriteLine("bitmapW" + bitmap.Width);

            Console.WriteLine("bitmapH"+bitmap.Height);
      
   
            InitBoarder();
            initCircle();
            //Console.WriteLine(bitmap.GetPixel(500, 500).ToString());
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
          //  this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (FPS);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < numberOfCircle; i++)
            {

                ErasingMidpointCircle(R, (int)circles[i].x, (int)circles[i].y);
            }
            collision();
       

            physics();
           // Console.WriteLine("position X1" + ":" + circles[0].x);
           // Console.WriteLine("position Y1" + ":" + circles[0].y);
           // Console.WriteLine("position X2" + ":" + circles[1].x);
            //Console.WriteLine("position Y2" + ":" + circles[1].y);
            for (int i = 0; i < numberOfCircle; i++)
            {

                MidpointCircle(R, (int)circles[i].x, (int)circles[i].y);
            }

            pictureBox1.Image = bitmap;

        }
        public void InitBoarder()
        {
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            Console.WriteLine(width);
            Console.WriteLine(height);
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < height; j++)
                {
                   
                    bitmap.SetPixel(i, j, Color.Yellow);
   
                }
            }
            for (int i = width-R; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    bitmap.SetPixel(i, j, Color.Yellow);

                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < R; j++)
                {

                    bitmap.SetPixel(i, j, Color.Yellow);

                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = height-R; j < height; j++)
                {

                    bitmap.SetPixel(i, j, Color.Yellow);

                }
            }



            pictureBox1.Image = bitmap;

        }
        public void initCircle()
        {
            /* for (int i = 0; i < numberOfCircle; i++)
             {
                 circles.Add(new Circle(30 * i+50, 50, 0, 0));
             }*/
            // circles.Add(new Circle(300,500,0,0));
            // circles.Add(new Circle(700, 500, 0, 0));
            // circles.Add(new Circle(432, 680, 0, 0));
            //circles.Add(new Circle(100, 100, 0, 0));
            /* circles.Add(new Circle(rnd.Next(100,350), rnd.Next(100, 250), 0, 0));
             circles.Add(new Circle(rnd.Next(100, 350), rnd.Next(500, 750), 0, 0));
             circles.Add(new Circle(rnd.Next(400, 650), rnd.Next(300, 450), 0, 0));
             circles.Add(new Circle(rnd.Next(400, 650), rnd.Next(100, 250), 0, 0));
             circles.Add(new Circle(rnd.Next(700, 950), rnd.Next(500, 650), 0, 0));
             circles.Add(new Circle(rnd.Next(700, 950), rnd.Next(300, 500), 0, 0));
             circles.Add(new Circle(rnd.Next(1000, 1250), rnd.Next(700, 850), 0, 0));
             circles.Add(new Circle(rnd.Next(1000, 1250), rnd.Next(200, 600), 0, 0));*/
            /* circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));
             circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));
             circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));
             circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));
             circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));
             circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));
             circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));
             circles.Add(new Circle(rnd.Next(200, 1200), rnd.Next(200, 800), 0, 0));*/

            //32
            circles.Add(new Circle(rnd.Next(100, 900), rnd.Next(100, 900), rnd.Next(-10, 10), rnd.Next(-10, 10)));
            for(int i=0;i<numberOfCircle;i++ )
            {

                circles.Add(new Circle(rnd.Next(100, 900), rnd.Next(100, 900), rnd.Next(-10, 10), rnd.Next(-10, 10)));
            }
            for (int i = 0; i < numberOfCircle; i++)
            {
                
                MidpointCircle(R, (int)circles[i].x, (int)circles[i].y);
            }
            pictureBox1.Image = bitmap;
        }
        public void collision()
        {
           /* for (int i = 0; i < numberOfCircle; i++)
            {
                for (int j = i+1; j < numberOfCircle; j++)
                {
                    if(i!=j)
                    {
                        if((circles[i].x- circles[j].x) * (circles[i].x - circles[j].x)+ (circles[i].y - circles[j].y) * (circles[i].y - circles[j].y)<R*R*10)
                        {
                            //collision happened
                            circles[i].vx = -circles[i].vx;
                            circles[i].vy = -circles[i].vy;
                            circles[j].vx = -circles[j].vx;
                            circles[j].vy = -circles[j].vy;


                        }
                    }
                }
            }*/
        }
        public void physics()
        {
            double dx = 0;
            double dy = 0;
            double r2 = 0;
            double a = 0;
            double distance2 = 0;
            //wall bounce
            for (int i = 0; i < numberOfCircle; i++)
            {
                if (circles[i].x <= 2*R || circles[i].x >= 1000 - 2*R)
                    circles[i].vx = -circles[i].vx;
                if (circles[i].y <= 2*R || circles[i].y >= 1000 - 2*R)
                    circles[i].vy = -circles[i].vy;
            }


                ////speed change !!!!!!should calcute all then change
                List<double> speedx = new List<double>();
            List<double> speedy = new List<double>();
            for (int i = 0; i < numberOfCircle; i++)
            {
                double v_x = 0;
                double v_y = 0;
                for (int j = 0; j < numberOfCircle; j++)
                {
                    if (j!=i)
                    {
                        /*f=GMm/r^2=ma
                         *f_x=f/r*(jx-ix)=ma_x
                         *f_y=f/r*(jy-iy)=ma_y
                         *Gm/r^3*(jx-ix)=a_x
                         *v_x=v0_x+a_x*t;
                         *v_y=v0_y+a_y*t;
                         *_x=
   
                        */
                        dx = circles[j].x - circles[i].x;
                        dy = circles[j].y - circles[i].y;
                     
                        r2 = dx * dx + dy * dy;
                        a = G * M / r2;
                        if (a > limita) a = limita;
                        if (r2 < R * R) r2 = R * R;
                        v_x+=a * dx * T / Math.Sqrt(r2);
                        v_y+=a * dy * T / Math.Sqrt(r2);
                    }
                }
                speedx.Add(v_x);
                speedy.Add(v_y); 
            }

            //clash
            for (int i = 0; i < numberOfCircle; i++)
            {
                for (int j = 0; j < numberOfCircle; j++)
                {
                    if (i != j)
                    {
                        dx = circles[j].x - circles[i].x;
                        dy = circles[j].y - circles[i].y;
                        distance2 = dx * dx + dy * dy;
                        if (distance2 <= R * R * 4)
                        {
                            //reset points
                            //R/dxm==dis/dx
                            double dxm = R * dx / Math.Sqrt(distance2);
                            circles[j].x += (dxm - dx / 2);
                            circles[i].x -= (dxm - dx / 2);
                            double dym = R * dy / Math.Sqrt(distance2);
                            circles[j].y += (dym - dy / 2);
                            circles[i].y -= (dym - dy / 2);
                            //reverse velocity
                            circles[i].vx = -circles[i].vx * elastic;
                            circles[i].vy = -circles[i].vy * elastic;
                            circles[j].vx = -circles[j].vx * elastic;
                            circles[j].vy = -circles[j].vy * elastic;


                        }
                    }
                }
            }
            for (int i = 0; i < numberOfCircle; i++)
            {
                circles[i].x += (circles[i].vx + speedx[i]) / 2 * T;
                circles[i].y += (circles[i].vy + speedy[i]) / 2 * T;
                circles[i].vx += speedx[i];
                circles[i].vy += speedy[i];
              //  Console.WriteLine("vx" + i + ":" + circles[i].vx);
              //  Console.WriteLine("vy" + i + ":" + circles[i].vy);
            }
        }

        
        public void drawing(Bitmap bitmap,int centerX,int centerY, int x, int y)
        {
            Color newColor = Color.FromArgb(0, 0, 0);
            if (centerX < 2 * R || centerY < 2 * R || centerX > Width - 2 * R || centerY > Height - 2 * R) return;

            bitmap.SetPixel(centerX + x, centerY + y, newColor);
            bitmap.SetPixel(centerX + y, centerY + x, newColor);
            bitmap.SetPixel(centerX - x, centerY - y, newColor);
            bitmap.SetPixel(centerX - y, centerY - x, newColor);
            bitmap.SetPixel(centerX + x, centerY - y, newColor);
            bitmap.SetPixel(centerX - x, centerY + y, newColor);
            bitmap.SetPixel(centerX + y, centerY - x, newColor);
            bitmap.SetPixel(centerX - y, centerY + x, newColor);

        }
        public void MidpointCircle(int R, int centerX, int centerY)
        {

            int d = 1 - R;
            int x = 0;
            int y = R;
            drawing(bitmap, centerX, centerY, x, y);
            while (y > x)
            {
                if (d < 0) //move to E
                    d += 2 * x + 3;
                else //move to SE
                {
                    d += 2 * x - 2 * y + 5;
                    --y;
                }
                ++x;
                drawing(bitmap, centerX, centerY, x, y);
            }
            pictureBox1.Image = bitmap;
        }

        public void Erasing(Bitmap bitmap, int centerX, int centerY, int x, int y)
        {

            Color newColor = Color.FromArgb(0,0,0,0);

            if (centerX < 2*R || centerY < 2*R || centerX > Width - 2*R || centerY > Height- 2*R) return;
            bitmap.SetPixel(centerX + x, centerY + y, newColor);
            bitmap.SetPixel(centerX + y, centerY + x, newColor);
            bitmap.SetPixel(centerX - x, centerY - y, newColor);
            bitmap.SetPixel(centerX - y, centerY - x, newColor);
            bitmap.SetPixel(centerX + x, centerY - y, newColor);
            bitmap.SetPixel(centerX - x, centerY + y, newColor);
            bitmap.SetPixel(centerX + y, centerY - x, newColor);
            bitmap.SetPixel(centerX - y, centerY + x, newColor);

        }
        public void ErasingMidpointCircle(int R, int centerX, int centerY)
        {

            int d = 1 - R;
            int x = 0;
            int y = R;
            Erasing(bitmap, centerX, centerY, x, y);
            while (y > x)
            {
                if (d < 0) //move to E
                    d += 2 * x + 3;
                else //move to SE
                {
                    d += 2 * x - 2 * y + 5;
                    --y;
                }
                ++x;
                Erasing(bitmap, centerX, centerY, x, y);
            }
            pictureBox1.Image = bitmap;
        }

    }
}
