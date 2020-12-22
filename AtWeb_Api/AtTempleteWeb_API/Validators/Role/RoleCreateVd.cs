using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtRoleDm;

namespace AtTempleteWeb_API.Validators.Role
{
    public class RoleCreateVd : AbstractValidator<AtRoleDmInputCreate>
    {
        public RoleCreateVd()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Mã rỗng").NotNull().WithMessage("Mã rỗng")
                .MaximumLength(10).WithMessage("Mã quá 10 ký tự");
            
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Tên quyền rỗng").NotNull().WithMessage("Tên quyền rỗng")
                .MaximumLength(100).WithMessage("Tên quyền quá 100 ký tự");
            
            RuleFor(x => x.Prioty)
                .InclusiveBetween(1,10).WithMessage("Dộ ưu tiên từ 1 đến 10 ");


        }
    }
}
