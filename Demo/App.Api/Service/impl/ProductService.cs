using System.Net;
using App.Api.common;
using App.Api.Domains;
using App.Api.exception;
using App.Api.Models;
using App.Api.repository;
using AutoMapper;

namespace App.Api.Service.impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductDTO> GetById(long id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            throw new BusinessException("Product not found", HttpStatusCode.NotFound);
        }
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<PageResponse<ProductDTO>> GetAllProducts(PageRequest request)
    {
        var productsPage = await _productRepository.GetAllProducts(request);

        var list = _mapper.Map<List<ProductDTO>>(productsPage.Items);
        
        return new PageResponse<ProductDTO>
        {
            Items = list,
            TotalItems = productsPage.TotalItems,
            PageSize = productsPage.PageSize,
            PageNumber = productsPage.PageNumber
        };
    }
    
    public async Task<ProductDTO> CreateProduct(ProductCreatedDTO productCreatedDto)
    {
        var categoryExists = await _categoryRepository.CheckIfCategoryExists(productCreatedDto.CategoryId);
        if (!categoryExists)
        {
            throw new BusinessException("Category not found", HttpStatusCode.NotFound);
        }
        
        var exists = await _productRepository.CheckIfExists(productCreatedDto.Name, productCreatedDto.CategoryId);
        if (exists)
        {
            throw new BusinessException("Product with the same name already exists in this category", HttpStatusCode.Conflict);
        }
        
        var product = _mapper.Map<Product>(productCreatedDto);
        var createdProduct = await _productRepository.CreateProduct(product);
        return _mapper.Map<ProductDTO>(createdProduct);
    }
    
    public async Task DeleteProduct(long id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            throw new BusinessException("Product not found", HttpStatusCode.NotFound);
        }
        product.IsDeleted = true;
        product.DeletedAt = DateTime.UtcNow;
        await _productRepository.DeleteProduct(product);
    }
}