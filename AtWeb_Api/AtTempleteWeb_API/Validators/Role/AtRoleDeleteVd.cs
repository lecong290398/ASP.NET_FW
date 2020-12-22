using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtRoleDm;

namespace AtTempleteWeb_API.Validators.Role
{
    public class AtRoleDeleteVd : AbstractValidator<AtRoleDmInputDelete>
    {
        public AtRoleDeleteVd()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID rỗng").NotNull().WithMessage("ID rỗng");

            RuleFor(x => x.AtRowversion)
            .NotEmpty().WithMessage("AtRowversion rỗng").NotNull().WithMessage("AtRowversion rỗng");
        }
    }
}
