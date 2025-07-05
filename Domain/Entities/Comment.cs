using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        public int? TaskID { get; set; }
        public int? ProjectID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string CommentText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(TaskID))]
        public Task? Task { get; set; }

        [ForeignKey(nameof(ProjectID))]
        public Project? Project { get; set; }

        [ForeignKey(nameof(UserID))]
        public User? User { get; set; }
    }
}