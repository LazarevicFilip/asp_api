using System.Collections.Generic;

namespace Application.DTO
{
    public class UpdateUserUseCaseDto
    {
        public IEnumerable<int> UseCaseIds { get; set; }
        public int UserId { get; set; }
    }
}