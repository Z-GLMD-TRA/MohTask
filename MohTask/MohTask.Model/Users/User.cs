namespace MohTask.Model.Users
{
    /// <summary>
    /// the user entity properties
    /// </summary>
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty ;
        //true for available and false for not available user
        public bool Status { get; set; } = true;
    }
}
