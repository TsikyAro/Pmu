using System.Data.SqlClient;

namespace pmutest;
public class Endurence{
    public double routes;
    public double endurence ;
    public int idcheval;
    public Endurence(double routes,double endurence,int idcheval){
        this.routes = routes;
        this.endurence = endurence;
        this.idcheval =idcheval;
    }

    public static Endurence[] selectendurencebyidcheval(Connexion c, int idcheval){
         List<Endurence> EndurenceList = new List<Endurence>();
            SqlConnection con = c.GetConnexion();
            con.Open();
            String sql = "SELECT * FROM Endurence where idcheval = " + idcheval;
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read()){
                Endurence temp = new Endurence(data.GetDouble(1),data.GetDouble(2),data.GetInt32(3));
                EndurenceList.Add(temp);
            }
            Endurence[] EndurenceArray = new Endurence[EndurenceList.Count];
            con.Close();
            EndurenceArray = EndurenceList.ToArray();
            return EndurenceArray;
    }
}