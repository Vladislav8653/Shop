﻿using ProductManagement.Application.DTO;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;

namespace ProductManagement.Application.Contracts.UseCasesContracts;

public interface IGetProducts
{
    Task<PagedResult<ProductResponseDto>> Handle(PageParams pageParams, ProductFilters filters, CancellationToken cancellationToken);
}