
using System.Data.SqlClient;

namespace pmutest;
public class Intervalle {
       public int cheval;
    public int temp;
    public Intervalle(int idcheval,int temp){
        this.cheval = idcheval;
        this.temp = temp;
    }
    public Intervalle(){}
    public static Intervalle selectIntervallebyidcheval(Connexion c, int idcheval){
            SqlConnection con = c.GetConnexion();
            Intervalle temp = null ;
            con.Open();
            String sql = "SELECT * FROM Intervalle where idcheval = " + idcheval;
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataReader data = command.ExecuteReader();
            if (data.Read()){
                temp = new Intervalle(data.GetInt32(0),data.GetInt32(1));
            }
            con.Close();
            return temp;
    }
}