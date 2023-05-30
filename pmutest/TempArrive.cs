using System.Data.SqlClient;

namespace pmutest;
public class TempArrive{
   public int cheval;
    public int temp;
    public TempArrive(int idcheval,int temp){
        this.cheval = idcheval;
        this.temp = temp;
    } 
    public void insertTemps(Connexion c){
            SqlConnection con = c.GetConnexion();
            con.Open();
            String sql = "INSERT INTO Temparrive (idCheval,tempArrive) VALUES ( @idCheval,@temps)";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.Parameters.AddWithValue("@idCheval", this.cheval);
            cmd.Parameters.AddWithValue("@temps", temp);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    public static TempArrive selectTempArrivebyidcheval(Connexion c, int idcheval){
            SqlConnection con = c.GetConnexion();
            TempArrive temp = null ;
            con.Open();
            String sql = "SELECT * FROM TempArrive where idcheval = " + idcheval;
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataReader data = command.ExecuteReader();
            if (data.Read()){
                temp = new TempArrive(data.GetInt32(0),data.GetInt32(1));
            }
            con.Close();
            return temp;
    }
}