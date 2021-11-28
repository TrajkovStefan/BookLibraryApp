using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.PaymentDto;

namespace Seavus.BookLibrary.Mappers
{
    public static class PaymentMapper
    {
        public static Payment ToPayment(this AddPaymentDto addPaymentDto)
        {
            return new Payment()
            {
                ReservationPrice = 100,
                PenaltyPrice = 20,
                PaymentMethod = (PaymentEnum)addPaymentDto.PaymentMethod
            };
        }
    }
}