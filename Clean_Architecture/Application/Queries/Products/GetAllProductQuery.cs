using Clean_Architecture.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Queries.Products
{
   public class GetAllProductQuery : IRequest<IEnumerable<Product>>
   {
   }
}
