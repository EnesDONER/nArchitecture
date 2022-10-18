using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery : IRequest<BrandGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
        {
            private readonly IBrandRespository _brandRespository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;

            public GetByIdBrandQueryHandler(IBrandRespository brandRespository, IMapper mapper,BrandBusinessRules brandBusinessRulles)
            {
                _mapper = mapper;
                _brandRespository = brandRespository;
                _brandBusinessRules = brandBusinessRulles; 
            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                Brand? brand = await _brandRespository.GetAsync(b => b.Id == request.Id);
                _brandBusinessRules.BrandShouldExistWhenRequested(brand);
                BrandGetByIdDto brandGetByIdDto = _mapper.Map<BrandGetByIdDto>(brand);
                return brandGetByIdDto; 
            }
        }


    }
}
