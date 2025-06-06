namespace AGEX.CORE.Dtos.Login.Get
{
    public class GetUsersResDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Attempts { get; set; }
        public string CreateDatetime { get; set; }
    }
}
