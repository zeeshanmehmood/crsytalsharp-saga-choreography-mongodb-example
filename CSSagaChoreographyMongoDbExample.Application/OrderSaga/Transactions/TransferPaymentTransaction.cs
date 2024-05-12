using System;
using CrystalSharp.Sagas;

namespace CSSagaChoreographyMongoDbExample.Application.OrderSaga.Transactions
{
    public class TransferPaymentTransaction : ISagaTransaction
    {
        public Guid GlobalUId { get; set; }
    }
}
