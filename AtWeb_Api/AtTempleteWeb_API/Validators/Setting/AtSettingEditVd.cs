using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtSettingDm;

namespace AtTempleteWeb_API.Validators.Setting
{
    public class AtSettingEditVd : AbstractValidator<AtSettingDmEditInput>
    {
        public AtSettingEditVd()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("ID rỗng").NotNull().WithMessage("ID rỗng");
            
            RuleFor(x => x.Value)
             .NotEmpty().WithMessage("Giá trị rỗng").NotNull().WithMessage("Giá trị rỗng");

            RuleFor(x => x.RowVersion)
             .NotEmpty().WithMessage("AtRowversion rỗng").NotNull().WithMessage("AtRowversion rỗng");
        }
    }
}
