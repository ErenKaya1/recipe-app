using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecipeApp.Core.Enum;
using RecipeApp.Core.Extensions;
using RecipeApp.Core.Models.Category;
using RecipeApp.Entity.Entities;
using RecipeApp.Repository.Contracts;
using RecipeApp.Service.Contracts;

namespace RecipeApp.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IRecipeService _recipeService;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService, IRecipeService recipeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
            _recipeService = recipeService;
        }

        public ServiceResponse<List<Category>> GetAll()
        {
            var entities = _unitOfWork.CategoryRepository.FindAll().ToList();
            var response = new ServiceResponse<List<Category>>(true, data: entities);

            return response;
        }

        public async Task<ServiceResponse<Category>> GetByIdAsync(string id)
        {
            var entity = await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Id == id);
            if (entity == null)
                return new ServiceResponse<Category>(false, ErrorType.EntityNotFound);

            return new ServiceResponse<Category>(true, data: entity);
        }

        public async Task<ServiceResponse> InsertCategoryAsync(NewCategoryModel model)
        {
            if (model == null) return new ServiceResponse(false, ErrorType.NullModel);
            if (await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Name == model.Name) != null)
                return new ServiceResponse(false, ErrorType.DuplicatedCategoryName);

            var entity = _mapper.Map<Category>(model);
            entity.Slug = model.Name.ConvertToSlugFormat();

            if (model.ImageFile != null)
            {
                var suffix = Guid.NewGuid().ToString();
                await _imageService.SaveCategoryImage(model.ImageFile, suffix);
                entity.ImageName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName) + "-" + suffix + Path.GetExtension(model.ImageFile.FileName);
            }

            await _unitOfWork.CategoryRepository.AddAsync(entity);

            return new ServiceResponse(true);
        }

        public async Task<ServiceResponse> EditAsync(EditCategoryModel model)
        {
            if (model == null) return new ServiceResponse(false, ErrorType.NullModel);
            var entity = await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Id == model.Id);
            if (entity == null) return new ServiceResponse(false, ErrorType.EntityNotFound);
            if (entity.Name != model.Name)
                if (await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Name == model.Name) != null)
                    return new ServiceResponse(false, ErrorType.DuplicatedCategoryName);

            entity.Name = model.Name;
            entity.Slug = model.Name.ConvertToSlugFormat();

            if (model.ImageFile != null)
            {
                var suffix = Guid.NewGuid().ToString();
                await _imageService.SaveCategoryImage(model.ImageFile, suffix, entity.ImageName);
                entity.ImageName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName) + "-" + suffix + Path.GetExtension(model.ImageFile.FileName);
            }

            await _unitOfWork.CategoryRepository.UpdateAsync(entity);
            return new ServiceResponse(true);
        }

        public async Task<ServiceResponse> DeleteByIdAsync(string id)
        {
            var entity = await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Id == id);
            if (entity == null)
                return new ServiceResponse(false, ErrorType.EntityNotFound);

            await _unitOfWork.CategoryRepository.DeleteAsync(entity);
            await _recipeService.DeleteRangeByCategoryId(entity.Id);

            if (!string.IsNullOrEmpty(entity.ImageName))
                _imageService.DeleteCategoryImage(entity.ImageName);

            return new ServiceResponse(true);
        }
    }
}