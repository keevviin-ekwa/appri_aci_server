using ApproACI.DLA;
using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ApproACI.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DatabaseContext _context;

        private IGenericRepository<Produit> _produits { get; set; }

        private IGenericRepository<Matiere> _matieres { get; set; }

        private IGenericRepository<Stock> _stocks { get; set; }

        private IGenericRepository<Marque> _marques { get; set; }

        private IGenericRepository<Objectif> _objectifs { get; set; }
        private IGenericRepository<User> _users { get; set; }

        private IGenericRepository<DroitsAcces> _droitsAccess { get; set; }
        private IGenericRepository<MatiereProduit> _matiereProduits { get; set; }
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;

        }

        public IGenericRepository<Produit> Produits => _produits ??= new GenericRepository<Produit>(_context);

        public IGenericRepository<Matiere> Matieres => _matieres ??= new GenericRepository<Matiere>(_context);

        public IGenericRepository<Stock> Stocks => _stocks ??= new GenericRepository<Stock>(_context);

        public IGenericRepository<MatiereProduit> MatiereProduits => _matiereProduits ??= new GenericRepository<MatiereProduit>(_context);

        public IGenericRepository<Marque> Marques => _marques ??= new GenericRepository<Marque>(_context);

   

        public IGenericRepository<Objectif> Objectifs => _objectifs ??= new GenericRepository<Objectif>(_context);

        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);

        public IGenericRepository<DroitsAcces> DroitsAcces => _droitsAccess ??= new GenericRepository<DroitsAcces>(_context);

        public void Save()
        {
           _context.SaveChanges();
        }

        public void Dispose()
        {

            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
