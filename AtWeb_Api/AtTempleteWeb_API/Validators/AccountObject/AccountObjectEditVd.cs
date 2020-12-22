using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AccountObjectDm;

namespace AtTempleteWeb_API.Validators.AccountObject
{
    public class AccountObjectEditVd : AbstractValidator<AccountObjectDmInput_Edit>
    {
        public AccountObjectEditVd()
        {
            RuleFor(x => x.AccountObjectName)
              .NotEmpty()
              .WithMessage("Họ tên rỗng")
              .NotNull()
              .WithMessage("Họ tên rỗng").MaximumLength(250).WithMessage("Họ tên quá 250 ký tự");

            RuleFor(x => x.AtRowVersion)
                .NotEmpty().WithMessage("AtRowVersion rỗng").NotNull().WithMessage("AtRowVersion rỗng");

            RuleFor(x => x.ListIdRole)
                .NotEmpty().WithMessage("Vai trò rỗng").NotNull().WithMessage("Vai trò rỗng");

            RuleFor(x => x.ListIdDepartment)
               .NotEmpty().WithMessage("Phòng ban rỗng").NotNull().WithMessage("Phòng ban rỗng");

        }
    }
}
