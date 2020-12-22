using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtMenuFuntionDm;

namespace AtTempleteWeb_API.Validators.MenuFunction
{
    public class MenuFunctionCreatetOrEditVd  : AbstractValidator<MenuFunctionDmCreatetInputOrEdit>
    {
        public MenuFunctionCreatetOrEditVd()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Tiêu đề rỗng")
            .NotNull()
            .WithMessage("Tiêu đề rỗng")
            .MaximumLength(200).WithMessage("Tiêu đề quá 200 ký tự");
            
            RuleFor(x => x.ControllerName)
            .NotEmpty().WithMessage("ControllerName rỗng")
            .NotNull()
            .WithMessage("ControllerName rỗng")
            .MaximumLength(100).WithMessage(" ControllerName quá 100 ký tự");
            

            RuleFor(x => x.AcctionName)
            .NotEmpty().WithMessage("AcctionName rỗng")
            .NotNull()
            .WithMessage("AcctionName rỗng").MaximumLength(100).WithMessage(" AcctionName quá 100 ký tự");

            RuleFor(x => x.ControllerNameView)
            .NotEmpty().WithMessage("ControllerNameView rỗng")
            .NotNull()
            .WithMessage("ControllerNameView rỗng").MaximumLength(100).WithMessage(" ControllerNameView quá 100 ký tự");

            RuleFor(x => x.AcctionNameView)
            .NotEmpty().WithMessage("AcctionNameView rỗng")
            .NotNull()
            .WithMessage("AcctionNameView rỗng").MaximumLength(100).WithMessage(" AcctionNameView quá 100 ký tự");


            RuleFor(x => x.FK_MenuSubGroup)
            .NotEmpty().WithMessage("Nhóm chức năng rỗng")
            .NotNull()
            .WithMessage("Nhóm chức năng rỗng");
        }
    }
}
