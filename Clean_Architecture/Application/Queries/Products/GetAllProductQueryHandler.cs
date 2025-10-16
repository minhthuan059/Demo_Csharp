using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Queries.Products
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAll();
        }
    }
}
