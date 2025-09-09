using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CatalogoApi;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Model.Dto;
using CatalogoApi.Repository;
using CatalogoApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace CatalogoApiTests.UnitTest
{
    public class ProdutosUnitTestController
    {
        public IUnitOfWork _unitOfWork;
        public IMapper mapper;
        public static DbContextOptions<CatalogoContext> dbContextOptions { get; }

        public static string connectionString =
            "server=localhost;database=Catalogo;user=root;password=20486501";
        static ProdutosUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<CatalogoContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
        }
        public ProdutosUnitTestController()
        {
            var context = new CatalogoContext(dbContextOptions);
            _unitOfWork = new UnitOfWork(context);
        }
    }
}
