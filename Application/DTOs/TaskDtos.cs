using System.ComponentModel.DataAnnotations;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.DTOs
{
    public class TaskDto
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? AssignedToUserID { get; set; }
        public string? AssignedToUserName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Project ID is required.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "The title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? AssignedToUserId { get; set; }

        [StringLength(20, ErrorMessage = "The status cannot exceed 20 characters")]
        public string Status { get; set; } = "To Do";

        [StringLength(20, ErrorMessage = "The priority cannot exceed 20 characters")]
        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }
    }

    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "The title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? AssignedToUserId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20, ErrorMessage = "The status cannot exceed 20 characters.")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required.")]
        [StringLength(20, ErrorMessage = "The priority cannot exceed 20 characters.")]
        public string Priority { get; set; } = string.Empty;

        public DateTime? DueDate;
    }
}