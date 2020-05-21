using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bounce_CS3160
{
    public partial class Form1 : Form
    {
        const int iTimerInterval = 25;
        const int iBallSize = 12;
        const int iMoveSize = 4;
        Bitmap bitmap;
        int xCenter, yCenter;   //center of ball
        int cxRadius, cyRadius; //radius of ball
        int cxMove, cyMove;     //horizontal and vertical distance to move
        int cxTotal, cyTotal;   //width and height of ball image

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            Timer timer = new Timer();
            timer.Interval = iTimerInterval;
            timer.Tick += new EventHandler(timer1_Tick);

            timer.Start(); //starting timer

            this.ResizeRedraw = true; //force redraw on form resize

            //setting yCenter and xCenter equal to form's center
            Point newP = new Point();
            newP.X = Size.Width / 2;
            newP.Y = Size.Height / 2;
            xCenter = newP.X;
            yCenter = newP.Y;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(BackColor);
           
            //setting radius of ball so that it's proportional to form
            cxRadius =  DisplayRectangle.Width / iBallSize;
            cyRadius = DisplayRectangle.Width / iBallSize;

            //making amount of movement proportional to ball's radius
            cxMove = (int)(Math.Max(1, cxRadius / iMoveSize));
            cyMove = (int)(Math.Max(1, cyRadius / iMoveSize));

            //make bitmap size aware of the ball size and extra space around it
            cxTotal = 2 * (cxRadius + cxMove);
            cyTotal = 2 * (cyRadius + cyMove);

            //creating new bitmap
            bitmap = new Bitmap(cxTotal, cyTotal);
            g = Graphics.FromImage(bitmap); //getting graphics object for alteration
            g.Clear(BackColor);

            //drawing circle on bitmap in dark orange
            g.FillEllipse(Brushes.DarkOrange, new Rectangle(cxMove, cyMove, 2 * cxRadius, 2 * cyRadius));

            g.Dispose(); //disposing Graphics object

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            //drawing bitmap
            g.DrawImage(bitmap, (int)(xCenter - cxTotal / 2), (int) (yCenter - cyTotal / 2), cxTotal, cyTotal);
            //keeping ball moving
            xCenter += cxMove;
            yCenter += cyMove;

            if(xCenter + cxRadius >= DisplayRectangle.Width || xCenter - cxRadius <= 0)
            {
                cxMove = System.Convert.ToInt32(-cxMove);
            }
            if (yCenter + cyRadius >= DisplayRectangle.Width || yCenter - cyRadius <= 0)
            {
                cyMove = System.Convert.ToInt32(-cyMove);
            }
        }
    }
}
