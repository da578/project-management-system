using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Application.DTOs
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public int? TaskID { get; set; }
        public int? ProjectID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int FileSizeKB { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }

    public class CreateAttachmentDto
    {
        public int? TaskID { get; set; }
        public int? ProjectID { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "File name is required.")]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string FileName { get; set; } = string.Empty;

        [Required(ErrorMessage = "File path is required.")]
        public string FilePath { get; set; } = string.Empty;

        public int FileSizeKB { get; set; }
    }

    public class UpdateAttachmentDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "File name is required.")]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string FileName { get; set; } = string.Empty;
    }
}