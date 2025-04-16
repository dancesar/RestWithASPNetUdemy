using RestWithASPNETUdemy.Hypermedia.Abstract;

namespace RestWithASPNETUdemy.Filters.Utils;

public class PagedSearchDTO<T> where T : ISupportsHyperMedia
{
    public int CurretPage { get; set; }
    
    public int PageSize { get; set; }
    
    public int TotalResults { get; set; }
    
    public string SortFields { get; set; }
    
    public string SortDirections { get; set; }
    
    public Dictionary<string, Object> Filters { get; set; }
    
    public List<T> List { get; set; }

    public PagedSearchDTO() {}

    public PagedSearchDTO(int curretPage, int pageSize, string sortFields, string sortDirections)
    {
        CurretPage = curretPage;
        PageSize = pageSize;
        SortFields = sortFields;
        SortDirections = sortDirections;
    }

    public PagedSearchDTO(int curretPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters)
    {
        CurretPage = curretPage;
        PageSize = pageSize;
        SortFields = sortFields;
        SortDirections = sortDirections;
        Filters = filters;
    }

    public PagedSearchDTO(int curretPage, string sortFields, string sortDirections) : this (curretPage, 10, sortFields, sortDirections) {}

    public int GetCurrentPage()
    {
        return CurretPage == 0 ? 2 : CurretPage;
    }

    public int GetPageSize()
    {
        return PageSize == 0 ? 10 : PageSize;
    }
}