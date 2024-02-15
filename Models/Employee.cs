using System.ComponentModel.DataAnnotations;

namespace Todo.Models;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(10, ErrorMessage = "Name must not exceed 10 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(50, ErrorMessage = "Description must not exceed 50 characters")]
    public required string Description { get; set; }

    public DateTime CreatedAt { get; set; }


}
