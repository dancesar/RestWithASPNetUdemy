using System.Text;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Hypermedia.Constats;

namespace RestWithASPNETUdemy.Hypermedia.Enricher;

public class PersonEnricher : ContentResponseEnricher<PersonDTO>
{
    private readonly object _lock = new object();
    
    protected override Task EnrichModel(PersonDTO content, IUrlHelper urlHelper)
    {
        var path = "api/person/v1/person";
        string link = GetLink(content.Id, urlHelper, path);
        
        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.GET,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });
        
        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.POST,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });
        
        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.PUT,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });
        
        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.DELETE,
            Href = link,
            Rel = RelationType.self,
            Type = "int"
        });
        
        return null;
    }

    private string GetLink(long id, IUrlHelper urlHelper, string path)
    {
        lock (_lock)
        {
            var url = new { controller = path, id = id };
            return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
        }
    }
}