using ETicaretAPI.Application.ViewModels.Products;
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
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Ürün Adını Boş Geçmeyiniz")
                .MaximumLength(20)
                .MinimumLength(3)
                    .WithMessage("Ürün Adının Kontrol Ediniz");

            RuleFor(c => c.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Stok Bilgisini Boş Geçmeyiniz")
                .Must(s => s >= 0)
                    .WithMessage("Stok Bilgisi Negatif Olamaz");

            RuleFor(c => c.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Fiyat Bilgisini Boş Geçmeyiniz")
                .Must(s => s >= 0)
                    .WithMessage("Fiyat Bilgisi Negatif Olamaz");
        }

    }
}
