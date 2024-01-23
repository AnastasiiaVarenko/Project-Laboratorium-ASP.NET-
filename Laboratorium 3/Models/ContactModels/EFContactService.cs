using Data;
using Data.Entities;

namespace Laboratorium_3.Models.ContactModels;

public class EFContactService : IContactService
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _timeProvider;

    public EFContactService(AppDbContext context, IDateTimeProvider timeProvider)
    {
        _context = context;
        _timeProvider = timeProvider;
    }

    public int Add(Contact contact)
    {
        contact.Created = _timeProvider.Now();
        var e = _context.Contacts.Add(ContactMapper.ToEntity(contact));
        _context.SaveChanges();
        return e.Entity.ContactId;
    }


    public void DeleteById(int id)
    {
        ContactEntity? find = _context.Contacts.Find(id);
        if (find != null)
        {
            _context.Contacts.Remove(find);
        }
        _context.SaveChanges();
    }

    public List<Contact> FindAll()
    {
        return _context.Contacts.Select(e => ContactMapper.FromEntity(e)).ToList(); 
    }

    public List<OrganizationEntity> FindAllOrganizations()
    {
        return _context.Organizations.ToList();
    }

    public Contact? FindById(int id)
    {
        return ContactMapper.FromEntity(_context.Contacts.Find(id));
    }

    public PagingList<Contact> FindPage(int page, int size)
    {
        return PagingList<Contact>.Create(
            (p, s) => _context.Contacts.OrderBy(c => c.Name).Skip((p - 1) * s).Take(s).Select(ContactMapper.FromEntity).ToList(),
            page,
            size,
            _context.Contacts.Count()
            );
    }

    public void Update(Contact contact)
    {
        _context.Contacts.Update(ContactMapper.ToEntity(contact));
        _context.SaveChanges();
    }
    public Task<int> AddAsync(Contact contact)
    {
        return Task.Run(() => Add(contact));
    }

    public Task<Contact?> FindByIdAsync(int id)
    {
        return Task.Run(() => FindById(id));
    }

    public Task<List<Contact>> FindAllAsync()
    {
        return Task.Run(() => FindAll());
    }

    public Task DeleteByIdAsync(int id)
    {
        return Task.Run(() => DeleteById(id));
    }

    public Task UpdateAsync(Contact contact)
    {
        return Task.Run(() => Update(contact));
    }

    public Task<List<OrganizationEntity>> FindAllOrganizationsAsync()
    {
        return Task.Run(() => FindAllOrganizations());
    }

    public Task<PagingList<Contact>> FindPageAsync(int page, int size)
    {
        return Task.Run(() => FindPage(page, size));
    }
}
