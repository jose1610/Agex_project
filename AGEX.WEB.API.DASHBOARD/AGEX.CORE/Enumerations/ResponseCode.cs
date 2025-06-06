namespace AGEX.CORE.Enumerations
{
    public class ResponseCode
    {
        public static int Success => 0;
        public static int FatalError => -1;
        public static int Error => 1;
        public static int Timeout => 99;
    }
}
