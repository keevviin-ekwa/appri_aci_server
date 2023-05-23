using ApproACI.DTO;
using ApproACI.Models;
using AutoMapper;
using Microsoft.Extensions.Hosting;


namespace ApproACI.Configurations
{
    public class MapperInitializer:Profile
    {   
        public MapperInitializer()
        {
            CreateMap<Produit, CreateProductDTO>().ReverseMap();
            CreateMap<Produit, UpdateProductDTO>().ReverseMap();
            CreateMap<Produit, ProductDTO>().ReverseMap();
            CreateMap<Matiere, CreateMatiereDTO>().ReverseMap();
            CreateMap<Matiere, UpdateMatiereDTO>().ReverseMap();
            CreateMap<Matiere, MatiereDTO>().ReverseMap();
            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<Objectif, ObjectifDTO>().ReverseMap();
            CreateMap<MatiereProduit, MatiereProduitDTO>().ReverseMap();
            CreateMap<MatiereProduit, ResultMatiereProduitDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
