using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AccountObjectDm;

namespace AtTempleteWeb_API.Validators.AccountObject
{
    public class AccountObjectDeleteVd : AbstractValidator<AccountObjectDm_Delete>
    {
        public AccountObjectDeleteVd()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID rỗng").NotNull().WithMessage("ID rỗng");
            
            RuleFor(x => x.AtRowVersion)
            .NotEmpty().WithMessage("AtRowVersion rỗng").NotNull().WithMessage("AtRowVersion rỗng");
        }
    }
}
