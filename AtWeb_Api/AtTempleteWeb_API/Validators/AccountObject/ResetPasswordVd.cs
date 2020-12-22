using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AccountObjectDm;

namespace AtTempleteWeb_API.Validators.AccountObject
{
    public class ResetPasswordVd : AbstractValidator<AccountObjectDm_ResetPassword>
    {
        public ResetPasswordVd()
        {
            RuleFor(x => x.PassWord)
               .NotEmpty().WithMessage("Password rỗng")
               .NotNull()
               .WithMessage("Password rỗng");

            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("ID rỗng")
             .NotNull()
             .WithMessage("ID rỗng");
            
            RuleFor(x => x.AtRowVersion)
             .NotEmpty().WithMessage("AtRowVersion rỗng")
             .NotNull()
             .WithMessage("AtRowVersion rỗng");
        }
    }
}
