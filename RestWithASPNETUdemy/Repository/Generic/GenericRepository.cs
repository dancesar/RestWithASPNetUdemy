using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.model.Base;
using RestWithASPNETUdemy.model.Context;

namespace RestWithASPNETUdemy.Repository.Generic;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private MySQLContext _context;
    private DbSet<T> _dataset;

    public GenericRepository(MySQLContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }

    public T FindById(long id)
    {
        return _dataset.SingleOrDefault(p => p.Id.Equals(id));
    }

    public List<T> FindAll()
    {
        return _dataset.ToList();
    }

    public T Create(T item)
    {
        try
        {
            _dataset.Add(item);
            _context.SaveChanges();
            return item;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public T Update(T item)
    {
        var result = _dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
        if (result != null)
        {
            try
            {
                _dataset.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            return null;
        }
    }

    public void Delete(long id)
    {
        var result = _dataset.SingleOrDefault(p => p.Id.Equals(id));
        if (result != null)
        {
            try
            {
                _dataset.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool Exists(long id)
    {
        return _dataset.Any(p => p.Id.Equals(id));
    }
}