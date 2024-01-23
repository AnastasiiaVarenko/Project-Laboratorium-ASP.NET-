using Data.Entities;
using System.Reflection;

namespace Laboratorium_3.Models.ContactModels
{
    public class MemoryContactService : IContactService
    {
        private Dictionary<int, Contact> _contacts = new Dictionary<int, Contact>();
        private int _id = 1;

        private readonly IDateTimeProvider _timeProvider;
        public MemoryContactService(IDateTimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public int Add(Contact model)
        {
            model.Created = _timeProvider.Now();
            model.Id = _id++;
            _contacts[model.Id] = model;
            return model.Id;

        }

        public void DeleteById(int id)
        {
            _contacts.Remove(id);
        }

        public List<Contact> FindAll()
        {
            return _contacts.Values.ToList();
        }

        public Contact? FindById(int id)
        {
            return _contacts.ContainsKey(id) ? _contacts[id] : null;
        }

        public void Update(Contact contact)
        {
            if (_contacts.ContainsKey(contact.Id))
            {
                _contacts[contact.Id] = contact;
            }
        }


        public List<OrganizationEntity> FindAllOrganizations()
        {
            throw new NotImplementedException();
        }

        public PagingList<Contact> FindPage(int page, int size)
        {
            return PagingList<Contact>.Create(
            (p, s) => _contacts.OrderBy(c => c.Value.Name)
                .Skip((p - 1) * s)
                .Take(s)
                .Select(c => c.Value)
            .ToList(),
                page,
                size,
                _contacts.Count()
            );
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
}
