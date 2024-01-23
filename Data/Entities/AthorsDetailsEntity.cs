using Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class AthorsDetailsEntity
{
    [ForeignKey("PostEntity")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public virtual PostEntity PostEntity { get; set; } = null!;
}
