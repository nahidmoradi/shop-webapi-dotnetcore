using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Shop.Core.Domain.Entities;
using Shop.Core.Domain.Interfaces;
using Shop.Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? q = null)
    {
        var products = await _productRepository.GetProductsWithCategoryAsync();
        
        var query = products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(p => p.Name.Contains(q) || (p.Description != null && p.Description.Contains(q)));
        }

        var total = query.Count();
        var items = query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var dto = _mapper.Map<List<ProductDto>>(items);

        return Ok(new { total, page, pageSize, items = dto });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productRepository.GetProductWithCategoryByIdAsync(id);
        if (product == null) return NotFound();
        
        var dto = _mapper.Map<ProductDto>(product);
        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        var created = await _productRepository.AddAsync(product);
        var result = _mapper.Map<ProductDto>(created);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
    {
        if (id != dto.Id) return BadRequest();
        
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        
        _mapper.Map(dto, product);
        await _productRepository.UpdateAsync(product);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        
        await _productRepository.DeleteAsync(product);
        return NoContent();
    }
}