namespace Shop.Model
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Sex sex { get; set; }
        public string AvatarUrl { get; set; }
        public string Password { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? BirthDay { get; set; }

        public virtual ICollection<ReviewProduct> Reviews { get; set; } = new List<ReviewProduct>();
        

        public virtual Cart Cart { get; set; }
    }
    public enum Sex
    {
        Male = 0,FeMale = 1,Other = 2
    }
}
