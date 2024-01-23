using Data.Entities;

namespace Laboratorium_3.Models.ContactModels
{
    public interface IContactService
    {
        int Add(Contact contact);
        Contact? FindById(int id);
        List<Contact> FindAll();
        void DeleteById(int id);
        void Update(Contact contact);
        List<OrganizationEntity> FindAllOrganizations();
        PagingList<Contact> FindPage(int page, int size);

        Task<int> AddAsync(Contact contact);
        Task<Contact?> FindByIdAsync(int id);
        Task<List<Contact>> FindAllAsync();
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(Contact contact);
        Task<List<OrganizationEntity>> FindAllOrganizationsAsync();
        Task<PagingList<Contact>> FindPageAsync(int page, int size);
    }
}
