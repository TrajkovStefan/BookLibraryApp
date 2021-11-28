using Seavus.BookLibrary.Dtos.PaymentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IPaymentService
    {
        void AddPayment(AddPaymentDto addPaymentDto);
    }
}
