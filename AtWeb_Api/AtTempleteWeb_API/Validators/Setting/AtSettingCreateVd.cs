using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtSettingDm;

namespace AtTempleteWeb_API.Validators.Setting
{
    public class AtSettingCreateVd : AbstractValidator<AtSettingDmCreateInput>
    {
        public AtSettingCreateVd()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("ID rỗng").NotNull().WithMessage("ID rỗng")
           .MaximumLength(200).WithMessage("ID quá 200 ký tự");

            RuleFor(x => x.Value)
           .NotEmpty().WithMessage("Giá trị rỗng").NotNull().WithMessage("Giá trị rỗng");
        }
    }
}
