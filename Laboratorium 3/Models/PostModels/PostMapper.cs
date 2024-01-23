using Data.Entities;
using Data.Models;
using System.Xml.Linq;

namespace Laboratorium_3.Models.PostModels;

public class PostMapper
{
    public static Post FromEntity(PostEntity entityr, AthorsDetailsEntity entityp)
    {
        if (entityr == null)
        {
            return null;
        }

        return new Post()
        {
            Id = entityr.PostEntityId,
            Content = entityr.Content,
            PublicationDate = (DateTime)entityr.PublicationDate,
            Miasto = entityr.Adress?.City,
            AuthorName = entityp.Name,
            AuthorEmail = entityp.Email,
            AuthorPhone = entityp.Phone,
            ContactId = entityr.ContactEntityContactId,
            ContactName = entityr.ContactName
        };
    }

    public static Post FromEntity(PostEntity entityr)
    {
        if (entityr == null)
        {
            return null;
        }

        return new Post()
        {
            Id = entityr.PostEntityId,
            Content = entityr.Content,
            PublicationDate = (DateTime)entityr.PublicationDate,
            Miasto = entityr.Adress?.City,
            ContactName = entityr.ContactName
        };
    }


    public static AthorsDetailsEntity ToA(Post model)
    {
        if (model == null)
        {
            return null;
        }

        return new AthorsDetailsEntity()
        {
            Id = model.Id,
            Name = model.AuthorName,
            Email = model.AuthorEmail,
            Phone = model.AuthorPhone,
        };
    }

    public static PostEntity ToEntity(Post model)
    {
        if (model == null)
        {
            return null;
        }

        return new PostEntity()
        {
            PostEntityId = model.Id,
            Content = model.Content,
            PublicationDate = model.PublicationDate,
            Adress = new Adress()
            {
                City = model.Miasto,
                Street = model.Adress?.Split(' ').FirstOrDefault() ?? string.Empty,
                PostalCode = model.Adress?.Split(' ').Skip(1).FirstOrDefault() ?? "NULL",
            },
            ContactEntityContactId = model.ContactId,
            ContactName = model.ContactName,
        };
    }


}
