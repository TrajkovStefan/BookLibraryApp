using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.PaymentDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private IRepository<Payment> _paymentRepository;
        public PaymentService(IRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public void AddPayment(AddPaymentDto addPaymentDto)
        {
            Payment newPayment = addPaymentDto.ToPayment();
            _paymentRepository.Insert(newPayment);
        }
    }
}