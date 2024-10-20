using Core.Application.Requests;
using MediatR;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery
{
    public PageRequest PageRequest { get; set; }

    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, PagedResponse<GetListBrandListItemDto>>
    {

    }
}
