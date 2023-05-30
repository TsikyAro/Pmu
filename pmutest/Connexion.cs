using System.Data.SqlClient;
namespace pmutest;
public class Connexion{
    private static string connectionString ="Server=ETU2035-ARO;Database=terrain;Trusted_Connection=True;";
    public Connexion(){}
    public  SqlConnection GetConnexion(){
        SqlConnection connexion = new SqlConnection(connectionString);
        return connexion;
    }
    
}
