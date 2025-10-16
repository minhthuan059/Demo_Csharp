using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Clean_Architecture.Application.Commands.Products
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Entities.Product(command.Name, command.Price, command.Stock);
            await _productRepository.Add(product);
            return product;
        }
    }
}
