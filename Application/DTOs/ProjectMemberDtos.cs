using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Application.DTOs
{
    public class ProjectMemberDto
    {
        public int Id { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = "Member";
        public DateTime JoinedAt { get; set; }
    }

    public class CreateProjectMemberDto
    {
        [Required(ErrorMessage = "Project ID is required.")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        public string Role { get; set; } = "Member";// [cite: 45]
    }

    public class UpdateProjectMemberRoleDto
    {
        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        public string Role { get; set; } = string.Empty;
    }
}