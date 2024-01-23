using Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("contacts")]
public class ContactEntity
{
    [Key]
    public int ContactId { get; set; }
    [Column("name")]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? Birth { get; set; }
    public DateTime Created { get; set; }
    public Priority Priority { get; set; }
    public OrganizationEntity? Organization { get; set; }
    public int? OrganizationId { get; set; }
}
