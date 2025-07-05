using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Application.DTOs
{
    public class ProjectDto
    {
        public int ID { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedByUserName { get; set; } = string.Empty;
    }

    public class CreateProjectDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }
        
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "The project name cannot exceed 100 characters.")]
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class UpdateProjectDto
    {
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Th project name cannot exceed 100 characters.")]
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20, ErrorMessage = "The status cannot exceed 20 characters")]
        public string Status { get; set; } = string.Empty;
    }
}