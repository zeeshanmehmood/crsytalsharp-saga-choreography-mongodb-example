using System;
using CrystalSharp.Sagas;

namespace CSSagaChoreographyMongoDbExample.Application.OrderSaga.Transactions
{
    public class DeliverOrderTransaction : ISagaTransaction
    {
        public Guid GlobalUId { get; set; }
    }
}
