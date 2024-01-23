using System.Reflection.Metadata.Ecma335;

namespace Laboratorium_3.Models;

public class PagingList<T>
{
    public IEnumerable<T> Data { get; set; }
    public int Page { get; }
    public int Size { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public bool IsPrevious { get; }
    public bool IsNext { get; }

    private PagingList(IEnumerable<T> data, int page, int size, int totalItems)
    {
        Data = data;
        Page = page;
        Size = size;
        TotalItems = totalItems;
        TotalPages = TotalItems / Size + (TotalItems % Size == 0 ? 0 : 1);
        IsPrevious = Page > 1;
        IsNext = Page < TotalPages;        
    }
    private static int ClipPage(int page, int size, int totalItems)
    {
        int totalPages = totalItems / size + (totalItems % size == 0 ? 0 : 1);
        if(page <= 0) 
        {
            page = 1;
        }
        if(page>totalPages)
        {
            return totalPages;
        }
        return page;
    }
    public static PagingList<T> Create(Func<int, int, IEnumerable<T>> dataGenerator, int page, int size, int totalItems)
    {
        page = ClipPage(page, size, totalItems);
        return new PagingList<T>(
            dataGenerator.Invoke(page, size),
            page,
            size,
            totalItems
            );
    }
}
