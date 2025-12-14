using AutoMapper;
using Domain.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    public class ProfileMap:Profile
    {
        public ProfileMap()
        {
            //CreateMap<Source, Destination>();
            CreateMap<Category,ReturnCategoryDto>().ReverseMap();
            CreateMap<Expense,ReturnExpenceDto>().ReverseMap();
            CreateMap<Expense, ReturnTotalExpenseDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.totalExpense,
                           opt => opt.MapFrom(src => src.Amount));

        }
    }
}
