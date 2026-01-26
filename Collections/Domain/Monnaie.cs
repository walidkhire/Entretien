using System;

namespace Collections.Domain
{
    /// <summary>
    /// Représente une devise monétaire.
    /// </summary>
    public class Monnaie
    {
        /// <summary>
        /// Identifiant unique de la monnaie.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Code ISO de la monnaie (ex : "USD", "EUR").
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Nom complet de la monnaie.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description optionnelle de la monnaie.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Taux ou valeur associée à la monnaie (ex : par rapport à une devise de référence).
        /// </summary>
        public int Valeur { get; set; }

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Monnaie() { }

        /// <summary>
        /// Constructeur pratique avec initialisation.
        /// </summary>
        /// 

        // Pour HashSet comparer les objets par Id
        public override bool Equals(object obj) => obj is Monnaie m && m.Id == Id;
        public override int GetHashCode() => Id.GetHashCode();

        public Monnaie(Guid id, string code, string name, string description = "", int valeur = 0)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Valeur = valeur;
        }
    }
}
