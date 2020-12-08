namespace DataService
{
    public class PostgresSQL_Connect_String
    {
        private string host = "localhost";
        private string db = "postgres";
        private string UserId = "postgres";
        private string pwd = "Hcn27wzv";

        public override string ToString()
        {
            return $"host={host};db={db};uid={UserId};pwd={pwd}";
        }
    }
}