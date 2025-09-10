namespace BLH.ApproveIQ.Domain.Models;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int count)
    {
        Items = items;
        Count = count;
    }

    public IEnumerable<T> Items { get; }

    public int Count { get; }
}