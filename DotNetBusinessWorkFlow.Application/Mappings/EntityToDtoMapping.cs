using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.Mappings;

public static class EntityToDtoMapping
{
    public static ProductResponseDto? MapProductEntityToDto(Product? product)
    {
        if(product == null) return null;
        return new ProductResponseDto()
        {
            CreatedAt = product.CreatedAt,
            Id = product.Id,
            IsActive = product.IsActive,
            Name = product.Name,
            Price = product.Price,
            UpdateAt = product.UpdateAt
        };
    }

    public static CustomerResponseDto? MapCustomerEntityToDto(Customer? customer)
    {
        if (customer == null) return null;
        return new CustomerResponseDto()
        {
            Email = customer.Email,
            Name = customer.Name,
            CreatedAt = customer.CreatedAt,
            Id = customer.Id,
            IsActive = customer.IsActive,
            UpdateAt = customer.UpdateAt
        };
    }

}