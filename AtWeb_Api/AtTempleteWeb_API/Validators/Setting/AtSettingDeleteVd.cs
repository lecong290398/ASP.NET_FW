using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtSettingDm;

namespace AtTempleteWeb_API.Validators.Setting
{
    public class AtSettingDeleteVd : AbstractValidator<AtSettingDmInputDelete>
    {
        public AtSettingDeleteVd()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID rỗng").NotNull().WithMessage("ID rỗng");

            RuleFor(x => x.RowVersion)
            .NotEmpty().WithMessage("AtRowversion rỗng").NotNull().WithMessage("AtRowversion rỗng");
        }
    }
}
