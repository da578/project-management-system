using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Entities
{
    public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttachmentID { get; set; }

        public int? TaskID { get; set; }
        public int? ProjectID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public int FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(TaskID))]
        public Task? Task { get; set; }

        [ForeignKey(nameof(ProjectID))]
        public Project? Project { get; set; }

        [ForeignKey(nameof(UserID))]
        public User? User { get; set; }
    }
}