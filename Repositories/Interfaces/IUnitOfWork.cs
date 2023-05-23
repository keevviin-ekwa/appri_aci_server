using ApproACI.Models;
using System;
using System.Threading.Tasks;

namespace ApproACI.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        public IGenericRepository<Produit> Produits { get; }

        public IGenericRepository<Matiere> Matieres { get; }

        public IGenericRepository<Stock> Stocks { get; }

        public IGenericRepository<Marque> Marques { get; }

        public IGenericRepository<Objectif> Objectifs { get; }
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<DroitsAcces> DroitsAcces { get; }
        public IGenericRepository<MatiereProduit> MatiereProduits { get; }

        public void Save();
    }
}
