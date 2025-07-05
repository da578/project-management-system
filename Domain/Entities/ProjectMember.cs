using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Entities{
    public class ProjectMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectMemberID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "Member";

        public DateTime JoinedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ProjectID))]
        public Project? Project { get; set; }

        [ForeignKey(nameof(UserID))]
        public User? User { get; set; }
    }
}