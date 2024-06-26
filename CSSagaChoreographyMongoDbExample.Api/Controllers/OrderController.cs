﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrystalSharp.Sagas;
using CSSagaChoreographyMongoDbExample.Api.Dto;
using CSSagaChoreographyMongoDbExample.Application.OrderSaga.Transactions;

namespace CSSagaChoreographyMongoDbExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISagaTransactionExecutor _sagaTransactionExecutor;

        public OrderController(ISagaTransactionExecutor sagaTransactionExecutor)
        {
            _sagaTransactionExecutor = sagaTransactionExecutor;
        }

        [HttpPost]
        [Route("place-order")]
        public async Task<ActionResult<SagaTransactionResult>> PostPlaceOrder([FromBody] PlaceOrderRequest request)
        {
            PlaceOrderTransaction transaction = new()
            {
                Product = request.Product,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                AmountPaid = request.AmountPaid
            };

            return await _sagaTransactionExecutor.Execute(transaction, CancellationToken.None);
        }
    }
}
