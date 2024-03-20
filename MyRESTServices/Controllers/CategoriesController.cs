﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IValidator<CategoryCreateDTO> _validatorCreate;
        private readonly IValidator<CategoryUpdateDTO> _validatorUpdate;
        public CategoriesController(ICategoryBLL categoryBLL, IValidator<CategoryCreateDTO> validatorCreate,
            IValidator<CategoryUpdateDTO> validatorUpdate)
        {
            _categoryBLL = categoryBLL;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            var results = await _categoryBLL.GetAll();
            return results;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryBLL.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("categories/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _categoryBLL.GetByName(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("category/{names}")]
        public async Task<IActionResult> GetCount(string names)
        {
            var result = await _categoryBLL.GetCountCategories(names);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var validatorResult = await _validatorCreate.ValidateAsync(categoryCreateDTO);
                await _categoryBLL.Insert(categoryCreateDTO);
                return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            if (await _categoryBLL.GetById(id) == null)
            {
                return NotFound();
            }

            try
            {
                var validatorResult = await _validatorUpdate.ValidateAsync(categoryUpdateDTO);
                await _categoryBLL.Update(categoryUpdateDTO);
                return Ok("Update data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _categoryBLL.GetById(id) == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryBLL.Delete(id);
                return Ok("Delete data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
