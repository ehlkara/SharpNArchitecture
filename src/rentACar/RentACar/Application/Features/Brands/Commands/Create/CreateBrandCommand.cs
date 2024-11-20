using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>
{
    public string Name { get; set; }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRule _brandBusinessRule;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRule brandBusinessRule)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRule = brandBusinessRule;
        }

        public async Task<CreatedBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {

            await _brandBusinessRule.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

            Brand brand = _mapper.Map<Brand>(request);
            brand.Id = Guid.NewGuid();

            await _brandRepository.AddAsync(brand);

            CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);
            return createdBrandResponse;
        }
    }
}

