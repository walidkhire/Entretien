using Collections.Domain;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace Collections.Application
{
    public class MonnaieService : IMonnaieService
    {
        private readonly IMonnaieRepository _repository;
        private readonly ILogger<MonnaieService> _logger;

        public MonnaieService(IMonnaieRepository repository, ILogger<MonnaieService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public ObservableCollection<Monnaie> GetAll()
        {
            _logger.LogInformation("Récupération de toutes les monnaies.");
            return _repository.GetAll();
        }

        public Monnaie? GetById(Guid id)
        {
            _logger.LogInformation("Recherche de la monnaie avec Id {Id}", id);
            var monnaie = _repository.GetById(id);

            if (monnaie == null)
            {
                _logger.LogWarning("Aucune monnaie trouvée avec Id {Id}", id);
            }

            return monnaie;
        }

        public void Add(Monnaie monnaie)
        {
            if (monnaie is null)
            {
                _logger.LogError("Tentative d'ajout d'une monnaie null.");
                throw new ArgumentNullException(nameof(monnaie), "La monnaie ne peut pas être nulle.");
            }

            if (string.IsNullOrWhiteSpace(monnaie.Code))
            {
                _logger.LogError("Échec de l'ajout : Code obligatoire manquant.");
                throw new ArgumentException("Le code de la monnaie est obligatoire.", nameof(monnaie.Code));
            }

            _repository.Add(monnaie);
            _logger.LogInformation("Monnaie {Code} ajoutée avec succès.", monnaie.Code);

        }

        public bool Update(Monnaie monnaie)
        {
            if (monnaie is null)
            {
                _logger.LogError("Tentative de mise à jour d'une monnaie null.");
                throw new ArgumentNullException(nameof(monnaie), "La monnaie ne peut pas être nulle.");
            }

            var result = _repository.Update(monnaie);

            if (result)
                _logger.LogInformation("Monnaie {Id} mise à jour avec succès.", monnaie.Id);
            else
                _logger.LogWarning("Échec de la mise à jour : aucune monnaie trouvée avec Id {Id}", monnaie.Id);

            return result;
        }

        public bool Update(Guid id, Dictionary<string, object> updates)
        {
            if (updates is null || updates.Count == 0)
            {
                _logger.LogError("Tentative de mise à jour partielle sans données (Id {Id}).", id);
                throw new ArgumentException("Aucune mise à jour fournie.", nameof(updates));
            }

            var result = _repository.Update(id, updates);

            if (result)
                _logger.LogInformation("Monnaie {Id} mise à jour partiellement avec succès.", id);
            else
                _logger.LogWarning("Échec de la mise à jour partielle : aucune monnaie trouvée avec Id {Id}", id);

            return result;
        }

        public bool Remove(Guid id)
        {
            var result = _repository.Remove(id);

            if (result)
                _logger.LogInformation("Monnaie {Id} supprimée avec succès.", id);
            else
                _logger.LogWarning("Échec de la suppression : aucune monnaie trouvée avec Id {Id}", id);

            return result;
        }
    }
}
