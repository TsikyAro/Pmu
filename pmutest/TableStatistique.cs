using cheval;

namespace pmutest;
public partial class AfficheState : Form{
    Label label1 = new Label();
    Label label2 = new Label();
    Label label3 = new Label();
    Label label4 = new Label();

    public AfficheState()
    {
        InitializeComponent();
    }


    public Cheval[] trier (Cheval [] liste,Connexion con){
        Cheval result = null;
        for (int i = 0; i < liste.Length; i++){
             Intervalle listIntervalle = Intervalle.selectIntervallebyidcheval(con,liste[i].idcheval);
            for (int j = i+1 ; j < liste.Length; j++)
            {
                Intervalle listIntervalle2 = Intervalle.selectIntervallebyidcheval(con,liste[j].idcheval);
                if(listIntervalle.temp > listIntervalle2.temp){
                    result = liste[i];
                    liste[i]= liste[j];
                    liste[j]= result;
                }
            }
            
        }
        return liste;
    }
      private void InitializeComponent(){
            Connexion con = new Connexion();
            Cheval cheval = new Cheval();
            Cheval [] chevals =this.trier(cheval.selectCheval(con),con);

            Label label7 = new Label(){
                Text = "Temps",
                Location = new Point(110,10)
            };
            Label label0 = new Label(){
                Text = "Numero cheval",
                Location = new Point(10,10)
            };
            Label label1 = new Label(){
                Text = "Intervalle de Temps",
                Location = new Point(210,10)
            };
            Label label2 = new Label(){
                Text = "Argent",
                Location = new Point(310,10)
            };
            this.Controls.AddRange(new Control[]{label0,label1,label2,label7});
            for(int i = 0;i<chevals.Length ;i++){
                Intervalle listIntervalle = Intervalle.selectIntervallebyidcheval(con,chevals[i].idcheval);
                TempArrive tempArrive = TempArrive.selectTempArrivebyidcheval(con,chevals[i].idcheval);
                TimeSpan timeSpanValue = TimeSpan.FromSeconds(tempArrive.temp);
                Label lab0 = new Label(){
                    Text = $"{timeSpanValue}",
                    Location = new Point(110,60+(50*i))
                };
                Controls.Add(lab0);
                Label lab = new Label(){
                    Text = $"{listIntervalle.temp}",
                    Location = new Point(210,60+(50*i))
                };
                this.Controls.Add(lab);
                Label lab2 = new Label(){
                    Text = $"Cheval00{chevals[i].numeroequipe}",
                    Location = new Point(10,60+(50*i))
                };
                this.Controls.Add(lab2);
                Label lab3;
                if(listIntervalle.temp > 0){
                lab3 = new Label(){
                    Text = $"{listIntervalle.temp * -10}",
                    Location = new Point(310,60+(50*i))
                };
                }else{
                    lab3 = new Label(){
                    Text = "0",
                    Location = new Point(310,60+(50*i))
                     };
                }
                this.Controls.Add(lab3);

            }

           
            // // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Name = "Form2";
            this.Text = "Statistique Match";
            this.ResumeLayout(false);

    }
}

