using Application.Features.Brands.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand:IRequest<CreatedBrandDto>
    {
        public int Name { get; set; }
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand,CreatedBrandDto>
        {
            private readonly IBrandRespository _brandRespository;
            private readonly IMapper _mapper;
            public CreateBrandCommandHandler(IBrandRespository brandRespository,IMapper mapper)
            {
                _brandRespository = brandRespository;
                _mapper = mapper;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                Brand mappedBrand = _mapper.Map<Brand>(request);
                Brand createdBrand= await _brandRespository.AddAsync(mappedBrand);
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);
                return createdBrandDto;
            }

        }
    }
}
