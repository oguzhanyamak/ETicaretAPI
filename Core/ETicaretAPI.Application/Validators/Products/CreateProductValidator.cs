using ETicaretAPI.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {

            RuleFor(P => P.Name)
                .NotEmpty().NotNull().WithMessage("Ürün Adını Boş Geçmeyiniz");
            RuleFor(p => p.Stock)
                .NotEmpty().NotNull().WithMessage("Stok bilgisini boş geçmeyiniz")
                .Must(s => s >= 0).WithMessage("Stock bilgisi en az 0 olabilir");
            RuleFor(p => p.Price)
                .NotEmpty().NotNull().WithMessage("Fiyat bilgisini boş geçmeyiniz")
                .Must(s => s >= 0).WithMessage("Fiyat bilgisi en az 0 olabilir");
            ;

        }
    }
}
