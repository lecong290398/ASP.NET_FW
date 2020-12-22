using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AccountObjectDm;

namespace AtTempleteWeb_API.Validators.AccountObject
{
    public class AccountObjectUpdateInfromation : AbstractValidator<AccountObjectDm_UpdateAccount>
    {
        public AccountObjectUpdateInfromation()
        {
            RuleFor(x => x.AccountObjectName)
            .NotEmpty()
            .WithMessage("Họ tên rỗng")
            .NotNull()
            .WithMessage("Họ tên rỗng")
            .MaximumLength(250).WithMessage("Họ tên quá 250 ký tự"); ;

        }
    }
}
