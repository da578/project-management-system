using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Entities{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Not Started";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required]
        public int CreatedByUserID { get; set; }

        [ForeignKey(nameof(CreatedByUserID))]
        public User? CreatedByUser { get; set; }

        public ICollection<ProjectMember>? ProjectMembers { get; set; }
        public ICollection<Task>? Tasks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
    }
}