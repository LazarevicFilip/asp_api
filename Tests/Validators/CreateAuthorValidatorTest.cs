using Application.DTO;
using DataAccess;
using FluentAssertions;
using Implementations.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Validators
{
    public class CreateAuthorValidatorTest
    {
        [Fact]
        public void CreateAuthorTest()
        {
            //priprema testa
            var validator = new CreateAuthorValidator(Context);
            //var dto = new AuthorDto
            //{
            //    Name = "Eduardo Stamm"
            //};v
            var dto = new AuthorDto();
            //izvrsavanje testa
            var result = validator.Validate(dto);
            //verifikacija testa
            result.IsValid.Should().BeFalse();
            result.Errors.Where(x => x.PropertyName == "Name").Should().HaveCount(1);
            result.Errors.Where(x => x.PropertyName == "Name").First().ErrorMessage.Should().Be("Ime je obavezan podatak.");
        }
        private LibaryContext Context
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                var connection = "Data Source=FILIP-PC\\SQLEXPRESS;Initial Catalog=libary;Integrated Security=True";
                optionsBuilder.UseSqlServer(connection);
                var options = optionsBuilder.Options;
                return new LibaryContext(options);
            }
        }
    }
}
