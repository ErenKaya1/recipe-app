using System.Threading.Tasks;
using AutoMapper;
using RecipeApp.Repository.Contracts;
using RecipeApp.Service.Contracts;
using RecipeApp.Core.Models.Recipe;
using RecipeApp.Entity.Entities;
using System;
using System.IO;
using RecipeApp.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp.Service.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public RecipeService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ServiceResponse> InsertRecipeAsync(NewRecipeModel model)
        {
            var entity = _mapper.Map<Recipe>(model);

            entity.Ingredients = model.Ingredients.Split('\n');
            entity.Directions = model.Directions.Split('\n');
            entity.Slug = model.Title.ConvertToSlugFormat();
            entity.Category = new Category
            {
                Id = model.CategoryId,
                Name = (await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Id == model.CategoryId)).Name,
                Slug = (await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Id == model.CategoryId)).Slug
            };

            if (model.ImageFile != null)
            {
                var suffix = Guid.NewGuid().ToString();
                await _imageService.SaveRecipeImage(model.ImageFile, suffix);
                entity.ImageName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName) + "-" + suffix + Path.GetExtension(model.ImageFile.FileName);
            }

            await _unitOfWork.RecipeRepository.AddAsync(entity);
            return new ServiceResponse(true);
        }

        public ServiceResponse<List<ListRecipeModel>> GetAll()
        {
            var entities = _unitOfWork.RecipeRepository.FindAll().ToList();
            var data = entities.Select(x => _mapper.Map<ListRecipeModel>(x)).ToList();

            return new ServiceResponse<List<ListRecipeModel>>(true, data: data);
        }

        public ServiceResponse<List<ListRecipeModel>> GetAll(string categorySlug)
        {
            var entities = _unitOfWork.RecipeRepository.Find(x => x.Category.Slug == categorySlug).ToList();
            var data = entities.Select(x => _mapper.Map<ListRecipeModel>(x)).ToList();

            return new ServiceResponse<List<ListRecipeModel>>(true, data: data);
        }

        public async Task<ServiceResponse<EditRecipeModel>> GetByIdAsync(string id)
        {
            var entity = await _unitOfWork.RecipeRepository.FindOneAsync(x => x.Id == id);
            if (entity == null) return new ServiceResponse<EditRecipeModel>(false);
            var model = _mapper.Map<EditRecipeModel>(entity);
            model.Ingredients = string.Join('\n', entity.Ingredients);
            model.Directions = string.Join('\n', entity.Directions);

            return new ServiceResponse<EditRecipeModel>(true, data: model);
        }

        public async Task<ServiceResponse> EditAsync(EditRecipeModel model)
        {
            var entity = await _unitOfWork.RecipeRepository.FindOneAsync(x => x.Id == model.Id);
            if (entity == null)
                return new ServiceResponse(false);

            entity.Title = model.Title;
            entity.Slug = model.Title.ConvertToSlugFormat();
            entity.Ingredients = model.Ingredients.Split('\n');
            entity.Directions = model.Directions.Split('\n');
            entity.Category = new Category
            {
                Id = model.CategoryId,
                Name = (await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Id == model.CategoryId)).Name,
                Slug = (await _unitOfWork.CategoryRepository.FindOneAsync(x => x.Id == model.CategoryId)).Slug
            };
            entity.OnHomepage = model.OnHomepage;
            entity.Time = model.Time;
            entity.Difficulty = model.Difficulty;
            entity.Servings = model.Servings;

            if (model.ImageFile != null)
            {
                var suffix = Guid.NewGuid().ToString();
                await _imageService.SaveRecipeImage(model.ImageFile, suffix, entity.ImageName);
                entity.ImageName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName) + "-" + suffix + Path.GetExtension(model.ImageFile.FileName);
            }

            await _unitOfWork.RecipeRepository.UpdateAsync(entity);
            return new ServiceResponse(true);
        }

        public async Task<ServiceResponse> DeleteByIdAsync(string id)
        {
            var entity = await _unitOfWork.RecipeRepository.FindOneAsync(x => x.Id == id);
            if (entity == null)
                return new ServiceResponse(false);

            if (!string.IsNullOrEmpty(entity.ImageName))
                _imageService.DeleteRecipeImage(entity.ImageName);

            await _unitOfWork.RecipeRepository.DeleteAsync(entity);
            return new ServiceResponse(true);
        }

        public ServiceResponse<List<ListRecipeModel>> GetHomepageRecipes()
        {
            var entities = _unitOfWork.RecipeRepository.Find(x => x.OnHomepage).ToList();
            var data = entities.Select(x => _mapper.Map<ListRecipeModel>(x)).ToList();

            return new ServiceResponse<List<ListRecipeModel>>(true, data: data);
        }

        public async Task<ServiceResponse<Recipe>> GetBySlugAsync(string slug)
        {
            var entity = await _unitOfWork.RecipeRepository.FindOneAsync(x => x.Slug == slug);
            if (entity == null)
                return new ServiceResponse<Recipe>(false);

            return new ServiceResponse<Recipe>(true, data: entity);
        }

        public async Task<ServiceResponse> DeleteRangeByCategoryId(string id)
        {
            var entities = _unitOfWork.RecipeRepository.Find(x => x.Category.Id == id).ToList();

            foreach (var entity in entities)
            {
                await _unitOfWork.RecipeRepository.DeleteAsync(entity);

                if (!string.IsNullOrEmpty(entity.ImageName))
                    _imageService.DeleteRecipeImage(entity.ImageName);
            }

            return new ServiceResponse(true);
        }
    }
}