using Data.Entities;
using Data.Models;
using Microsoft.Extensions.Hosting;

namespace Laboratorium_3.Models.PostModels;

public class MemoryPostService : IPostService
{
    private Dictionary<int, Post> _posts = new Dictionary<int, Post>();

    private int id = 1;

    public int Add(Post post)
    {
        post.Id = id++;
        _posts[post.Id] = post;
        return post.Id;

    }

    public void DeleteById(int id)
    {
        _posts.Remove(id);
    }

    public List<Post> FindAll()
    {
        return _posts.Values.ToList();
    }

    public Post? FindById(int id)
    {
        return _posts[id];
    }

    public void Update(Post post)
    {
        if (_posts.ContainsKey(post.Id))
        {
            _posts[post.Id] = post;
        }
    }
    public PagingList<Post> FindPage(int page, int size)
    {
        return PagingList<Post>.Create(
        (p, s) => _posts.OrderBy(c => c.Value.ContactId)
            .Skip((p - 1) * s)
            .Take(s)
            .Select(c => c.Value)
            .ToList(),
            page,
            size,
            _posts.Count()
        );
    }

    public Task<int> AddAsync(Post post)
    {
        return Task.Run(() =>
        {
            return Add(post);
        });
    }

    public Task<Post?> FindByIdAsync(int id)
    {
        return Task.Run(() =>
        {
            return FindById(id);
        });
    }

    public Task<List<Post>> FindAllAsync()
    {
        return Task.Run(() =>
        {
            return FindAll();
        });
    }

    public Task DeleteByIdAsync(int id)
    {
        return Task.Run(() =>
        {
            DeleteById(id);
        });
    }

    public Task UpdateAsync(Post post)
    {
        return Task.Run(() =>
        {
            Update(post);
        });
    }

    public Task<PagingList<Post>> FindPageAsync(int page, int size)
    {
        return Task.Run(() =>
        {
            return FindPage(page, size);
        });
    }

    public List<ContactEntity> FindAllContacts()
    {
        throw new NotImplementedException();
    }

    public Task<List<ContactEntity>> FindAllContactsAsync()
    {
        throw new NotImplementedException();
    }

}
