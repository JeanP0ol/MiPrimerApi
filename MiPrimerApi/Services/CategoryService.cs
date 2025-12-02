using MiPrimerApi.DAL.Models;
using MiPrimerApi.DAL.Models.Dtos;
using Api.W.Movies.Repository.IRepository;
using Api.W.Movies.Services.IServices;
using AutoMapper;

namespace Api.W.Movies.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<ICollection<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
                throw new InvalidOperationException($"No se encontró la categoría con ID: '{id}'");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            return await _categoryRepository.CategoryExistsByIdAsync(id);
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await _categoryRepository.CategoryExistsByNameAsync(name);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto categoryCreateDto)
        {
            if (await _categoryRepository.CategoryExistsByNameAsync(categoryCreateDto.Name))
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{categoryCreateDto.Name}'");

            var category = _mapper.Map<Category>(categoryCreateDto);
            var created = await _categoryRepository.CreateCategoryAsync(category);

            if (!created)
                throw new Exception("Ocurrió un error al crear la categoría.");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryCreateUpdateDto dto, int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
                throw new InvalidOperationException($"No se encontró la categoría con ID: '{id}'");

            if (await _categoryRepository.CategoryExistsByNameAsync(dto.Name))
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{dto.Name}'");

            _mapper.Map(dto, category);
            var updated = await _categoryRepository.UpdateCategoryAsync(category);

            if (!updated)
                throw new Exception("Ocurrió un error al actualizar la categoría.");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
                throw new InvalidOperationException($"No se encontró la categoría con ID: '{id}'");

            var deleted = await _categoryRepository.DeleteCategoryAsync(id);
            if (!deleted)
                throw new Exception("Ocurrió un error al eliminar la categoría.");

            return deleted;
        }
    }
}
