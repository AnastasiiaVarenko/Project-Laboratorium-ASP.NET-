using Data.Entities;
using Data.Models;

namespace Laboratorium_3.Models.PostModels;

public interface IPostService
{
    int Add(Post post);
    Post? FindById(int id);
    List<Post> FindAll();
    void DeleteById(int id);
    void Update(Post post);
    PagingList<Post> FindPage(int page, int size);
    List<ContactEntity> FindAllContacts();


    Task<int> AddAsync(Post post);
    Task<Post?> FindByIdAsync(int id);
    Task<List<Post>> FindAllAsync();
    Task DeleteByIdAsync(int id);
    Task UpdateAsync(Post post);
    Task<PagingList<Post>> FindPageAsync(int page, int size);
    Task<List<ContactEntity>> FindAllContactsAsync();

}