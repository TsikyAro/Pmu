using System;
using System.Drawing;
using System.Windows.Forms;
using cheval;
using pmutest;

namespace CircularPanelMovement
{
    public partial class MainForm : Form
    {
        private Cheval[] chevals;
        private double[] radians;
        private Panel ovalPanel1;
        private int chrono = 0;
        private System.Windows.Forms.Timer timer;
        private double[] angle ;
        private int radiusX = 450; // Rayon horizontal
        private int radiusY = 350; // Rayon vertical

        private int radiusX1 = 400; // Rayon horizontal
        private int radiusY1 = 300;
        private int centerX;
        private int centerY;
        Connexion connex = new Connexion();
        public MainForm()
        {
            Button btnStart = new Button();
            btnStart.Text = "Démarrer";
            btnStart.Click += btnMove_Click;
            this.Controls.Add(btnStart);
            Cheval chev = new Cheval();
            chevals = chev.selectCheval(connex);
            MessageBox.Show("bb"+chevals.Count());
            // InitializeComponent();
            this.Paint += new PaintEventHandler(MainForm_Load);
            this.ClientSize = new Size(1000,800);
            centerX = ClientSize.Width / 2;
            centerY = ClientSize.Height / 2;
            InitializeOvalPanel();
            InitializeTimer();
        }

        private void InitializeOvalPanel(){
            angle = new double[chevals.Length];
            radians = new double[chevals.Length];
            for(int i =0; i<chevals.Length; i++){
                Controls.Add(chevals[i]);
                angle[i]= chevals[i].vitesse;
            }
           
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;

        }

        private void tour(double radians,Cheval cheval){
            if(radians >= (Math.PI / 180)+Math.PI*6){
                cheval.mouvement = 0;
                cheval.tempcourse = this.chrono;
            }
        }

        private int checkCheval(){
            for(int i = 0; i<chevals.Count();i++){
                if(chevals[i].mouvement==1){
                    return 1;
                }
            }
            return 0;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(checkCheval()==0){

                timer.Stop();
                string resultat = "";
                for(int i =0; i <chevals.Length; i++){
                    resultat += $"cheval{i} = {chevals[i].tempcourse} \n";
                    TempArrive temp = new TempArrive(chevals[i].idcheval,chevals[i].tempcourse);
                    temp.insertTemps(connex);
                }
                    MessageBox.Show(resultat);
                AfficheState afficheState = new AfficheState();
                afficheState.Show();
                timer.Interval = 10000;
            }else
            for(int i = 0; i<chevals.Length; i++){
                if(chevals[i].mouvement == 0){

                }else{
                    angle[i] += chevals[i].vitesse;
                    radians[i] = (Math.PI / 180) * angle[i];
                    int x = centerX + (int)(Math.Cos(radians[i]) * chevals[i].radiusX);
                    int y = centerY + (int)(Math.Sin(radians[i]) * chevals[i].radiusY);
                    chevals[i].Location = new Point(x, y);
                    chevals[i].variation((Math.PI / 180)+Math.PI*6,radians[i],chevals[i],connex);
                    tour(radians[i],chevals[i]);
                    // MessageBox.Show(chevals[i].vitesse+"");
                }
            }
            chrono++;
        }

        private void MainForm_Load(object sender, EventArgs e){
            CreateOvalTrack();
        }

        private void CreateOvalTrack(){
            Brush brush = new SolidBrush(Color.LightGreen);
            Graphics g = CreateGraphics();
            for(int i =0; i<chevals.Length;i++){
                int trackWidth = chevals[i].radiusX * 2;
                int trackHeight = chevals[i].radiusY * 2;
                int trackX = centerX - chevals[i].radiusX;
                int trackY = centerY - chevals[i].radiusY;
                if(i== chevals.Length -1){
                   g.FillEllipse(brush,trackX, trackY, trackWidth, trackHeight);    
                }
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawEllipse(Pens.Black, trackX, trackY, trackWidth, trackHeight);
                g.DrawLine(Pens.Black,new Point( trackX + trackWidth, trackY +(trackHeight/2)),new Point( trackX + trackWidth - 50, trackY +(trackHeight/2)));
            }
                // this.Controls.Add(lab3);
                
        }
        private void btnMove_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void MainForm_FormClosing(object sender, EventArgs e)
        {
            timer.Stop();
        }
        private void MainForm_FormClosing1(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }
    }
}

