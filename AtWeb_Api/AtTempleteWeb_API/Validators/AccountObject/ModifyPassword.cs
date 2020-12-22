using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AccountObjectDm;

namespace AtTempleteWeb_API.Validators.AccountObject
{
    public class ModifyPassword : AbstractValidator<AccountObjectDm_UpdatePass>
    {
        public ModifyPassword()
        {
            RuleFor(x => x.oldPass)
                .NotEmpty().WithMessage("Mật khẩu củ rỗng ").NotNull().WithMessage("Mật khẩu mới rỗng ")
                .MaximumLength(50).WithMessage("Mật khẩu củ quá 50 ký tự");

            RuleFor(x => x.newPass)
                .NotEmpty().WithMessage("Mật khẩu mới rỗng ").NotNull().WithMessage("Mật khẩu mới rỗng ")
                .MaximumLength(50).WithMessage("Mật khẩu mới quá 50 ký tự");
        }
    }
}
