using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean_Architecture.Entities;
using MediatR;

namespace Clean_Architecture.Application.Commands.Products
{
    public class AddProductCommand : IRequest<Product>
    {
    }
}
