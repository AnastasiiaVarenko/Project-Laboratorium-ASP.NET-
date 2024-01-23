using Data.Models;

namespace Data.Entities;

public class OrganizationEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Adress? Adress { get; set; }
    public ISet<ContactEntity> Contacts { get; set; }

}
