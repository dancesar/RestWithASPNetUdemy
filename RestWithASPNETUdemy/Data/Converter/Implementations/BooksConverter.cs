using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.model;

namespace RestWithASPNETUdemy.Data.Converter.Implementations;

public class BooksConverter : IParser<BooksDTO, Books>, IParser<Books, BooksDTO>
{
    public Books Parse(BooksDTO origin)
    {
        if (origin == null) return null;
        return new Books()
        {
            Id = origin.Id,
            Title = origin.Title,
            Author = origin.Author,
            Price = origin.Price,
            LaunchDate = origin.LaunchDate,
        };
    }

    public BooksDTO Parse(Books origin)
    {
        if (origin == null) return null;
        return new BooksDTO()
        {
            Id = origin.Id,
            Title = origin.Title,
            Author = origin.Author,
            Price = origin.Price,
            LaunchDate = origin.LaunchDate,
        };
    }

    public List<Books> Parse(List<BooksDTO> origin)
    {
        if (origin == null) return null;
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<BooksDTO> Parse(List<Books> origin)
    {
        if (origin == null) return null;
        return origin.Select(item => Parse(item)).ToList();
    }
}