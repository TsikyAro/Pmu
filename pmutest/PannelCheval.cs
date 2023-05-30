using System.Data.SqlClient;
using pmutest;

namespace cheval{
    public partial class Cheval:Panel{
        public double vitesse = 0;
        public int idcheval;
        public int radiusX ; // Rayon horizontal
        public int radiusY ;
        public int numeroequipe ;
        public int mouvement = 1;
        public int tempcourse =0;
        public int endurence = 0;
        public Cheval(int idcheval,double vitesse,int radiusX, int radiusY,int localx,int localy,int numerocheval) {
            this.idcheval = idcheval;
            this.BackColor = Color.Blue;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.Size = new Size(30, 20);
            this.Location = new Point(localx,localy);
            this.BorderStyle = BorderStyle.FixedSingle;

            this.vitesse = vitesse;
            this.numeroequipe = numerocheval;

        }
        public Cheval(){}           
        public Cheval[] selectCheval(Connexion c){
            List<Cheval> ChvalList = new List<Cheval>();
            SqlConnection con = c.GetConnexion();
            con.Open();
            String sql = "SELECT * FROM Cheval  ";
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read()){
                Cheval temp = new Cheval(data.GetInt32(0),data.GetDouble(1),data.GetInt32(2),data.GetInt32(3), data.GetInt32(4),data.GetInt32(5),data.GetInt32(6));
                ChvalList.Add(temp);
            }
            Cheval[] chevalList = new Cheval[ChvalList.Count];
            con.Close();
            chevalList = ChvalList.ToArray();
            return chevalList;
        }

        public void variation(double radians,double position,Cheval cheval,Connexion c){
            Endurence[] endure = Endurence.selectendurencebyidcheval(c,cheval.idcheval);
            if(endure.Length!=0){
                for(int i = 0; i<endure.Length ;i++){
                    double rad = ( ( (-(Math.PI / 180)+(radians)) * endure[i].routes) / 100)+(Math.PI / 180);
                    if(position >= rad && cheval.endurence == i){
                        double vitess = (cheval.vitesse*endure[i].endurence)/100;
                        cheval.vitesse = cheval.vitesse + vitess;
                        cheval.endurence = i + 1;
                    }
                }
            }

        }
    }

}