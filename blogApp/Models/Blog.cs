namespace blogApp.Models
{
    public class Blog
    {
        public int id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; } 
        public DateTime CreatedAt { get; set; }
        //public List<Comment> Comments { get; set; }
    }

    //public class Comment
    //{
    //    public int id { get; set; }
    //    public string Text { get; set; }
    //    public DateTime CreatedAt { get; set;}
    //    public string AuthorEmail { get; set; }
    //}
}
