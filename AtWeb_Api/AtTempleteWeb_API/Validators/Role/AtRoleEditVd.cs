using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtRoleDm;

namespace AtTempleteWeb_API.Validators.Role
{
    public class AtRoleEditVd : AbstractValidator<AtRoleDmInputEdit>
    {
        public AtRoleEditVd()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("ID rỗng").NotNull().WithMessage("ID rỗng");
            
            RuleFor(x => x.AtRowversion)
             .NotEmpty().WithMessage("AtRowversion rỗng").NotNull().WithMessage("AtRowversion rỗng");

            RuleFor(x => x.RoleName)
               .NotEmpty().WithMessage("Tên quyền rỗng").NotNull().WithMessage("Tên quyền rỗng")
               .MaximumLength(100).WithMessage("Tên quyền quá 100 ký tự");

        }
    }
}
