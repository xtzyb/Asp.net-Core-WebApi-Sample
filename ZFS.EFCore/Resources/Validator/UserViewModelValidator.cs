using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using ZFS.EFCore.Resources.ViewModel;

namespace ZFS.EFCore.Resources.Validator
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {

    }

    public class PostAddOrUpdateResourceValidator<T> : AbstractValidator<T> where T : UserAddOrUpdateViewModel
    {
        public PostAddOrUpdateResourceValidator()
        {
            RuleFor(x => x.Account)
                .NotNull()
                .WithName("登录名")
                .WithMessage("required|{PropertyName}是必填的")
                .MinimumLength(5)
                .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");

            RuleFor(x => x.Password)
                .NotNull()
                .WithName("密码")
                .WithMessage("required|{{PropertyName}是必填的")
                .MinimumLength(6)
                .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");
        }
    }
}
