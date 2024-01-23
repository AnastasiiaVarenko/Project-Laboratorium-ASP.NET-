using Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("posts")]
public class PostEntity
{
    [Key]
    public int PostEntityId { get; set; }
    public string Content { get; set; }
    public DateTime? PublicationDate { get; set; }
    public Adress? Adress { get; set; }
    public AthorsDetailsEntity AthorsDetailsEntity { get; set; } = null!;
    public int ContactEntityContactId { get; set; }
    public string ContactName { get; set; }
    public virtual ContactEntity ContactEntity { get; set; }
}
