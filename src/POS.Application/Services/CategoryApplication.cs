using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Category;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services;

public class CategoryApplication : ICategoryApplication
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly CategoryValidator validationRules;

    public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidator validationRules)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.validationRules = validationRules;
    }

    public async Task<BaseResponse<CategoryResponseDto>> CategoryById(int id)
    {
        var response=new BaseResponse<CategoryResponseDto>();
        var category=await unitOfWork.CategoryRepository.CategoryById(id);
        if (category is not null)
        {
            response.IsSucces = true;
            response.Data = mapper.Map<CategoryResponseDto>(category);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        else
        {
            response.IsSucces = false;
            response.Message= ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> EditCategory(int id, CategoryRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var categoryEdit=await CategoryById(id);
        if (categoryEdit is null)
        {
            response.IsSucces=false;
            response.Message=ReplyMessage.MESSAGE_QUERY_EMPTY;
            return response;
        }

        var validationResult = await validationRules.ValidateAsync(requestDto);
        if (!validationResult.IsValid)
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;
            response.Errors = validationResult.Errors;
            return response;
        }
        var category = mapper.Map<Category>(requestDto);
        category.CategoryId = id;
        response.Data = await unitOfWork.CategoryRepository.EditCategory(category);
        if (response.Data)
        {
            response.IsSucces = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;
    }

    public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters)
    {
        var response=new BaseResponse<BaseEntityResponse<CategoryResponseDto>>();
        var categories=await unitOfWork.CategoryRepository.ListCategories(filters);
        if(categories is not null)
        {
            response.IsSucces=true;
            response.Data = mapper.Map<BaseEntityResponse<CategoryResponseDto>>(categories);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        return response;
    }

    public async Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories()
    {
        var response=new BaseResponse<IEnumerable<CategorySelectResponseDto>>();
        var categories = await unitOfWork.CategoryRepository.ListSelectCategories();
        if(categories is not null)
        {
            response.IsSucces=true;
            response.Data=mapper.Map<IEnumerable<CategorySelectResponseDto>>(categories);
            response.Message= ReplyMessage.MESSAGE_QUERY;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var validationResult = await validationRules.ValidateAsync(requestDto);
        if(!validationResult.IsValid)
        {
            response.IsSucces=false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;
            response.Errors=validationResult.Errors;
            return response;
        }
        var category = mapper.Map<Category>(requestDto);
        response.Data = await unitOfWork.CategoryRepository.RegisterCategory(category);
        if (response.Data)
        {
            response.IsSucces = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> RemoveCategory(int id)
    {
        var response = new BaseResponse<bool>();
        var categoryEdit = await CategoryById(id);
        if (categoryEdit is null)
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            return response;
        }
        response.Data = await unitOfWork.CategoryRepository.RemoveCategory(id);
        if (response.Data)
        {
            response.IsSucces = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        else
        {
            response.IsSucces = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;
    }
}
