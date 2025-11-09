using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Shop.Core.Domain.Interfaces;
using Shop.Application.DTOs;
using Shop.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? q = null)
    {
        var categories = await _categoryRepository.GetAllAsync();
        var query = categories.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(c => c.Name.Contains(q));
            
        var total = query.Count();
        var items = query
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var dto = _mapper.Map<List<CategoryDto>>(items);
        return Ok(new { total, page, pageSize, items = dto });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();
        
        var dto = _mapper.Map<CategoryDto>(category);
        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        var created = await _categoryRepository.AddAsync(category);
        var result = _mapper.Map<CategoryDto>(created);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
    {
        if (id != dto.Id) return BadRequest();
        
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();
        
        _mapper.Map(dto, category);
        await _categoryRepository.UpdateAsync(category);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();
        
        await _categoryRepository.DeleteAsync(category);
        return NoContent();
    }
}
