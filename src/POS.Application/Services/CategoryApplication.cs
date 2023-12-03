using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Category;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Interfaces;

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

    public Task<BaseResponse<CategoryResponseDto>> CategoryById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> EditCategory(int id, CategoryRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> RemoveCategory(int id)
    {
        throw new NotImplementedException();
    }
}
