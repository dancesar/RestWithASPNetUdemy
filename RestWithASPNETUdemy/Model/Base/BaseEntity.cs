using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.model.Base;

public class BaseEntity
{
    [Column("id")]
    public long Id { get; set; }
}