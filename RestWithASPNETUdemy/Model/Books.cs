using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNETUdemy.model.Base;

namespace RestWithASPNETUdemy.model;

[Table("books")]
public class Books : BaseEntity
{
    [Column("title")]
    public string Title { get; set; }
    
    [Column("author")]
    public string Author { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("launch_date")]
    public DateTime LaunchDate { get; set; }
}