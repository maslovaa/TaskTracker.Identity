namespace TaskTracker.Identity.Model.Dto
{
    public class ResponseDto
    {
        public string Token { get; set; }
        public string ErrorMessage { get; set; } = "";
    }
}
