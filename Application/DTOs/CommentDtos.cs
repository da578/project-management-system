using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Application.DTOs
{
    public class CommentDto
    {
        public int ID { get; set; }
        public int? TaskID { get; set; }
        public int? ProjectID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CommentText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class CreateCommentDto
    {
        public int? TaskID { get; set; }
        public int? ProjectID { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Comment text is required.")]
        public string CommentText { get; set; } = string.Empty;
    }

    public class UpdateCommentDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Comment text is required.")]
        public string CommentText { get; set; } = string.Empty;
    }
}