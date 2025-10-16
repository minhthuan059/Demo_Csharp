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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(command.Id);
            if (product == null)
                throw new Exception("Product not found.");

            product.SetName(command.Name);
            product.UpdatePrice(command.Price);
            product.IncreaseStock(command.Stock - product.Stock);
            await _productRepository.Update(product);
            return product;
        }
    }
}
