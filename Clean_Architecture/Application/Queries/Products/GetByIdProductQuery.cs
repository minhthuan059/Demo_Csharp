using Clean_Architecture.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Queries.Products
{
    public class GetByIdProductQuery : IRequest<Product>
    {
        public Guid Id { get; private set; }
        public GetByIdProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
