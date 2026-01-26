using Collections.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Collections.Application
{
    public interface IMonnaieService
    {
        ObservableCollection<Monnaie> GetAll();
        Monnaie? GetById(Guid id);
        void Add(Monnaie monnaie);
        bool Update(Monnaie monnaie);
        bool Update(Guid id, Dictionary<string, object> updates);
        bool Remove(Guid id);
    }
}