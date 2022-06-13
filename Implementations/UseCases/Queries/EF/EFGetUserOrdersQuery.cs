using Application;
using Application.DTO;
using Application.DTO.Searches;
using Application.Exceptions;
using Application.UseCases.Queries;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Queries.EF
{
    public class EFGetUserOrdersQuery : EFUseCaseConnection, IGetUsersOrderQuery
    {
        private IApplicationUser _user;
        public EFGetUserOrdersQuery(LibaryContext context,IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 25;

        public string Name => "Use case for searching a user orders.";

        public string Description => "Use case for searching a user orders with EF.";

        public PagedResponse<GetOrderDto> Execute(OrderBasePagedSearch request)
        {
            if(request.UserId != _user.Id)
            {
                throw new ForbbidenUseCaseException(Name,_user.Identity);
            }
           
            var orders = Context.Orders.Include(x => x.OrderLines).Where(x => x.UserId == request.UserId).AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                orders = orders.Where(x => x.Recipient.Contains(request.Keyword) || x.Adress.Contains(request.Keyword));
            }
            if (request.PerPage == null || request.PerPage < 10)
            {
                request.PerPage = 10;
            }
            if (request.Page == null || request.Page < 1)
            {
                request.Page = 1;
            }
            var toSkip = (request.Page - 1) * request.PerPage;
            var response = new PagedResponse<GetOrderDto>();
            response.TotalCount = orders.Count();
            response.ItemsPerPage = request.PerPage.Value;
            response.CurrentPage = request.Page.Value;
            response.Data = orders.Skip(toSkip.Value).Take(request.PerPage.Value).Select(x => new GetOrderDto
            {
              Adress = x.Adress,
              Recipient = x.Recipient,
              Date = x.CreatedAt,
              OrderId = x.Id,
              OrderLines = x.OrderLines.Select(y => new OrderLineDto
              {
                  BookId = y.BookId,
                  BookName = y.BookName,
                  BookPrice = y.Price,
                  Quantity = y.Quantity,
                  BookPublisherId = Context.Publishers.FirstOrDefault(z => z.Name == y.BookPublisher).Id
              })
            }).ToList();
            return response;
           
        }
    }
}
