namespace DataService
{
    public class PostgresSQL_Connect_String
    {
        private string host = "localhost";
        private string db = "imdb";
        private string UserId = "postgres";
        private string pwd = "nvp92agn";

        public override string ToString()
        {
            return $"host={host};db={db};uid={UserId};pwd={pwd}";
        }
    }
}