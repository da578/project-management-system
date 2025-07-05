using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Entities{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? AssignedToUserID { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "To Do";

        [Required]
        [StringLength(20)]
        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ProjectID))]
        public Project? Project { get; set; }

        [ForeignKey(nameof(AssignedToUserID))]
        public User? AssignedToUser { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
    }
}