using KPMDotNetCore.PizzaApi.Db;
using KPMDotNetCore.PizzaApi.Features.Queries;
using KPMDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPMDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DapperService _dapperService;
        public PizzaController()
        {
            _appDbContext = new AppDbContext();
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzaAsync()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("PizzaExtra")]
        public async Task<IActionResult> GetPizzaExtraAsync()
        {
            var lst = await _appDbContext.PizzasExtra.ToListAsync();
            return Ok(lst);
        }

        
        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var pizzaItem=await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = pizzaItem.Price;
            if (orderRequest.Extras.Length > 0)
            {
                // select * from Tbl_PizzaExtra where PizzaExtraID IN (2,3,4)
                //foreach (var item in orderRequest.Extras) { }

                var lstExtra = await _appDbContext.PizzasExtra.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }

            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            { 
                PizzaId=orderRequest.PizzaId,
                PizzaOrderInvoiceNo= invoiceNo,
                TotalAmount=total
            };

            List<PizzaOrderDetailModel> pizzExtraModel = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel
            { 
                PizzaExtraId=extraId,
                PizzaOrderInvoiceNo=invoiceNo,
                           
            }).ToList();
  
            await _appDbContext.PizzasOrder.AddAsync(pizzaOrderModel);
            await _appDbContext.PizzasOrderDetail.AddRangeAsync(pizzExtraModel);
            await _appDbContext.SaveChangesAsync();

            OrderResponse orderResponse = new OrderResponse()
            { 
                InvoiceNo=invoiceNo,
                Message="Thank you for your order ! Enjoy your Pizza !",
                TotalAmount = total,
            
            };
            return Ok(orderResponse);
        }

        //[HttpGet("Order/{invoiceNo}")]
        //public async Task<IActionResult> GetOrderInvoiceAsync(string invoiceNo)
        //{
        //    var item = await _appDbContext.PizzasOrder.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
        //    var lst = await _appDbContext.PizzasOrderDetail.Where( x =>x.PizzaOrderInvoiceNo ==  invoiceNo).ToListAsync();
        //    return Ok( new
        //    {
        //        order=item,
        //        orderDetail=lst
        //    });
        //}

        [HttpGet("Order/{invoiceNo}")]
        public IActionResult GetOrderInvoice(string invoiceNo)
        {
            var item =_dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>
                (
                    PizzaQuery.PizzaOrderQuery,
                    new { PizzaOrderInvoiceNo = invoiceNo }
                );

            var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
               (
                   PizzaQuery.PizzaOrderDetailQuery,
                   new { PizzaOrderInvoiceNo = invoiceNo }
               );

            var model = new PizzaOrderInvoiceResponse
            { 
                Order=item,
                OrderDetail= lst,
            };

            return Ok(model);
        }
    }
}
