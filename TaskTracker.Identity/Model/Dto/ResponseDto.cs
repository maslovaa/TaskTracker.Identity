namespace TaskTracker.Identity.Model.Dto
{
    public class ResponseDto
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public string ErrorMessage { get; set; } = "";
    }
}
