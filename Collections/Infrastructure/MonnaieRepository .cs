using Collections.Application;
using Collections.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Infrastructure
{
    public class MonnaieRepository : IMonnaieRepository
    {
        private readonly ObservableCollection<Monnaie> _listMonnaie;
        private readonly ILogger<MonnaieService> _logger;
        public MonnaieRepository(ILogger<MonnaieService> logger)
        {
            _listMonnaie = new ObservableCollection<Monnaie>();
            _logger = logger;
        }

        public ObservableCollection<Monnaie> GetAll()
        {
            _logger.LogInformation("Récupération de toutes les monnaies (count={Count}).", _listMonnaie.Count);

            return _listMonnaie;
        }
        public Monnaie? GetById(Guid id)
        {
            var monnaie = _listMonnaie.FirstOrDefault(e => e.Id == id);

            if (monnaie == null)
                _logger.LogWarning("Aucune monnaie trouvée avec Id {Id}", id);
            else
                _logger.LogInformation("Monnaie {Id} trouvée.", id);

            return monnaie;
        }
        public void Add(Monnaie monnaie)
        {
            _listMonnaie.Add(monnaie);
            _logger.LogInformation("Monnaie {Id} ajoutée (Code={Code}, Name={Name}).",
                monnaie.Id, monnaie.Code, monnaie.Name);
        }

        public bool Update(Monnaie? monnaie)
        {
            if (monnaie is null)
            {
                _logger.LogError("Échec mise à jour : Monnaie null.");
                return false;
            }

            var existing = GetById(monnaie.Id);
            if (existing is null)
            {
                _logger.LogWarning("Échec mise à jour : aucune monnaie trouvée avec Id {Id}", monnaie.Id);
                return false;
            }

            var props = typeof(Monnaie).GetProperties();
            foreach (var prop in props)
            {
                if (prop.CanWrite)
                {
                    var newValue = prop.GetValue(monnaie);
                    prop.SetValue(existing, newValue);
                }
            }

            _logger.LogInformation("Monnaie {Id} mise à jour avec succès.", monnaie.Id);
            return true;
        }


        public bool Update(Guid id, Dictionary<string, object> updates)
        {
            var existing = GetById(id);
            if (existing is null)
            {
                _logger.LogWarning("Échec mise à jour partielle : aucune monnaie trouvée avec Id {Id}", id);
                _logger.LogError("Échec mise à jour partielle : aucune monnaie trouvée avec Id {Id}", id);

                return false;
            }

            var props = typeof(Monnaie).GetProperties();
            foreach (var kv in updates)
            {
                var prop = props.FirstOrDefault(p => p.Name.Equals(kv.Key, StringComparison.OrdinalIgnoreCase));
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(existing, kv.Value);
                    _logger.LogInformation("Propriété {Property} mise à jour pour Monnaie {Id}.", kv.Key, id);
                }
            }

            return true;
        }

        public bool Remove(Guid id)
        {
            var monnaie = GetById(id);
            if (monnaie != null)
            {
                _listMonnaie.Remove(monnaie);
                _logger.LogInformation("Monnaie {Id} supprimée.", id);
                return true;
            }

            _logger.LogWarning("Échec suppression : aucune monnaie trouvée avec Id {Id}", id);
            return false;
        }
    }
}
