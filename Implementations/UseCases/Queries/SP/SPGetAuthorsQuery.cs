//using Application.DTO;
//using Application.DTO.Searches;
//using Application.UseCases.Queries;
//using Dapper;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Implementations.UseCases.Queries.SP
//{
//    public class SPGetAuthorsQuery : IGetAuthorsQuery
//    {
//        public int Id => 2;

//        public string Name => "Use case for searching authors";

//        public string Description => "Use case for searching authors with stored procedure";

//        public IEnumerable<AuthorDto> Execute(BaseSearch request)
//        {
//            var connection = new SqlConnection(@"Data Source=FILIP-PC\SQLEXPRESS;Initial Catalog=libary;Integrated Security=True");
//            var result = connection.Query<AuthorDto>("SearchAuthors", new { request.Keyword }, commandType: System.Data.CommandType.StoredProcedure);
//            return result;
//        }
//    }
//}
