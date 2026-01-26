using System;
using System.Collections.ObjectModel;
using Collections.Domain;

namespace Collections.Application
{
    public interface IMonnaieRepository
    {
        ObservableCollection<Monnaie> GetAll();
        Monnaie? GetById(Guid id);
        void Add(Monnaie monnaie);
        bool Update(Monnaie monnaie);
        bool Update(Guid id,Dictionary<string, object> monnaie);
        bool Remove(Guid id);
    }
}