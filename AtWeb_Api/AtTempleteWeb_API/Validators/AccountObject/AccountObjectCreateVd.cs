using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AccountObjectDm;

namespace AtTempleteWeb_API.Validators.AccountObject
{
    public class AccountObjectCreateVd : AbstractValidator<AccountObjectDmInput_Create>
    {
        public AccountObjectCreateVd()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username rỗng")
                .NotNull()
                .WithMessage("Username rỗng")
                .MaximumLength(50).WithMessage("Username quá 50 ký tự");

            RuleFor(x => x.PassWord)
              .NotEmpty().WithMessage("Password rỗng")
              .NotNull().WithMessage("Password rỗng")
              .MaximumLength(50).WithMessage("Password quá 50 ký tự");

            RuleFor(x => x.AccountObjectName)
              .NotEmpty()
              .WithMessage("Họ tên rỗng")
              .NotNull()
              .WithMessage("Họ tên rỗng").MaximumLength(250).WithMessage("Họ tên quá 250 ký tự");

            RuleFor(x => x.ListIdRole)
                .NotEmpty().WithMessage("Vai trò rỗng").NotNull().WithMessage("Vai trò rỗng");

            RuleFor(x => x.ListIdDepartment)
               .NotEmpty().WithMessage("Phòng ban rỗng").NotNull().WithMessage("Phòng ban rỗng");

        }
    }
}
