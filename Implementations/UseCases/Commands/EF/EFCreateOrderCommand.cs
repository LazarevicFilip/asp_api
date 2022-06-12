using Application.DTO;
using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Commands.EF
{
    public class EFCreateOrderCommand : EFUseCaseConnection,ICreateOrderCommand
    {
        private CreateOrderValidator _validator;
        private OrderLineValidator _validatorOrderLine;

        public EFCreateOrderCommand(LibaryContext context, CreateOrderValidator validator, OrderLineValidator validatorLine) : base(context)
        {
            _validator = validator;
            _validatorOrderLine = validatorLine;
        }

        public int Id => 24;

        public string Name => "Use case for creating a order";

        public string Description => "Use case for creating a order with EF.";

        public void Execute(MakeOrderDto request)
        {
            _validator.ValidateAndThrow(request);
            var order = new Order
            {
                Adress = request.Adress,
                Phone = request.Phone,
                Recipient = request.Recipient,
                UserId = request.UserId,
                SumPrice = request.OrderLines.Sum(x => x.Quantity * x.BookPrice),
                OrderLines = request.OrderLines.Select(x => new OrderLine
                {
                    BookId = x.BookId,
                    Quantity = x.Quantity,
                    BookName = x.BookName,
                    BookPublisher = Context.Publishers.FirstOrDefault(y => y.Id == x.BookPublisherId).Name,
                    Price = x.BookPrice
                }).ToList()
            };
            Context.Add(order);
            Context.SaveChanges();
        }

      
    }
}
