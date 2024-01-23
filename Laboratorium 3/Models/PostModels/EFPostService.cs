using Data;
using Data.Entities;
using Data.Models;
using Microsoft.Extensions.Hosting;

namespace Laboratorium_3.Models.PostModels;

public class EFPostService : IPostService
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _timeProvider;

    public EFPostService(AppDbContext context, IDateTimeProvider timeProvider)
    {
        _context = context;
        _timeProvider = timeProvider;
    }

    public int Add(Post post)
    {
        var postEntity = PostMapper.ToEntity(post);

        var addedPost = _context.Posts.Add(postEntity);
        _context.SaveChanges();

        var athorsDetailsEntity = PostMapper.ToA(post);
        athorsDetailsEntity.Id = addedPost.Entity.PostEntityId;

        _context.AthorsDetails.Add(athorsDetailsEntity);
        _context.SaveChanges();

        return addedPost.Entity.PostEntityId;
    }

    public Post? FindById(int id)
    {
        var entityr = _context.Posts.Find(id);
        var entityd = _context.AthorsDetails.Find(id);
        if (entityr != null)
        {
            return PostMapper.FromEntity(entityr, entityd);
        }
        return null;
    }

    public List<Post> FindAll()
    {
        return _context.Posts.Select(e => PostMapper.FromEntity(e)).ToList();
    }

    public void DeleteById(int id)
    {
        PostEntity? find = _context.Posts.Find(id);
        if (find != null)
        {
            _context.Posts.Remove(find);
            _context.SaveChanges();
        }
    }

    public void Update(Post post)
    {
        _context.Posts.Update(PostMapper.ToEntity(post));
        _context.AthorsDetails.Update(PostMapper.ToA(post));

        _context.SaveChanges();

    }

    public PagingList<Post> FindPage(int page, int size)
    {
        return PagingList<Post>.Create(
        (p, s) => _context.Posts.OrderBy(c => c.ContactEntityContactId)
            .Skip((p - 1) * s)
            .Take(s)
            .Select(PostMapper.FromEntity)
            .ToList(),
            page,
            size,
            _context.Posts.Count()
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
        return _context.Contacts.ToList();
    }

    public Task<List<ContactEntity>> FindAllContactsAsync()
    {
        return Task.Run(() =>
        {
            return FindAllContacts();
        });
    }
}











    /*
    public void DeleteById(int id)
    {
        ContactEntity? find = _post.Contacts.Find(id);
        if (find != null)
        {
            _post.Contacts.Remove(find);
        }
        _post.SaveChanges();
    }

    public List<Post> FindAll()
    {
        return _post.Contacts.Select(e => PostMapper.FromEntity(e)).ToList();
    }

    public List<AthorsDetailsEntity> FindAllAthorsDetail()
    {
        return _post.AthorsDetail.ToList();
    }

    public Post? FindById(int id)
    {
        return PostMapper.FromEntity(_post.Posts.Find(id));
    }

    public PagingList<Post> FindPage(int page, int size)
    {
        return PagingList<Post>.Create(
            (p, s) => _context.Contacts.OrderBy(c => c.Name).Skip((p - 1) * s).Take(s).Select(PostMapper.FromEntity).ToList(),
            page,
            size,
            _context.Contacts.Count()
            );
    }

    public void Update(Post post)
    {
        _post.Contacts.Update(PostMapper.ToEntity(post));
        _post.SaveChanges();
    }
    public Task<int> AddAsync(Post contact)
    {
        return Task.Run(() => Add(contact));
    }

    public Task<Post?> FindByIdAsync(int id)
    {
        return Task.Run(() => FindById(id));
    }

    public Task<List<Post>> FindAllAsync()
    {
        return Task.Run(() => FindAll());
    }

    public Task DeleteByIdAsync(int id)
    {
        return Task.Run(() => DeleteById(id));
    }

    public Task UpdateAsync(Post contact)
    {
        return Task.Run(() => Update(contact));
    }

    public Task<List<OrganizationEntity>> FindAllOrganizationsAsync()
    {
        return Task.Run(() => FindAllAthorsDetail());
    }

    public Task<PagingList<Post>> FindPageAsync(int page, int size)
    {
        return Task.Run(() => FindPage(page, size));
    }
    */
