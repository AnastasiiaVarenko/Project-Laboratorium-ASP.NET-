using Data;

namespace Laboratorium_3_5_EF.Models;

public class EFContactService : IContactService
{
    private readonly AppDbContext _context;
    public EFContactService(AppDbContext context)
    {
        _context = context;
    }
    public int Add(Contact contact)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Contact> FindAll()
    {
        throw new NotImplementedException();
    }

    public Contact? FindById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Contact contact)
    {
        throw new NotImplementedException();
    }
}
