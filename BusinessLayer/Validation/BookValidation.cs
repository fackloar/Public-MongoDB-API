using FluentValidation;
using MongoDB.BusinessLayer.DTOs;
using System.Text.RegularExpressions;

namespace Safe_Development.BusinessLayer.Validation
{
    public class BookValidation : AbstractValidator<BookDTO>
    {
        public BookValidation()
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .Must(n => Regex.IsMatch(n, @"\A[\p{L}\s]+\Z"))
                .WithMessage("{PropertyName} must be in English")
                .WithErrorCode("NAM-001");
            RuleFor(b => b.Author)
                .NotEmpty()
                .Must(n => Regex.IsMatch(n, @"\A[\p{L}\s]+\Z"))
                .WithMessage("{PropertyName} must be in English")
                .WithErrorCode("NAM-002");
        }
    }
}
