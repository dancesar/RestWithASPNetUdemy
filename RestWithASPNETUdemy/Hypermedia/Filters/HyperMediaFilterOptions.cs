using RestWithASPNETUdemy.Hypermedia.Abstract;

namespace RestWithASPNETUdemy.Hypermedia.Filters;

public class HyperMediaFilterOptions
{
    public List<IResponseEnricher> ContentResponseEnrichersList { get; set; } = new List<IResponseEnricher>();
}