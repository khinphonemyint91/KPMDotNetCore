namespace KPMDotNetCore.PizzaApi.Features.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } = 
            @"select po.*,p.PizzaName,p.Price from Tbl_PizzaOrder po
              inner join Tbl_Pizza p on po.PizzaId=p.PizzaId
              where po.PizzaOrderInvoiceNo= @PizzaOrderInvoiceNo";

        public static string PizzaOrderDetailQuery { get; } =
            @"select pod.*,px.PizzaExtraName,px.Price from Tbl_PizzaOrderDetail pod
              inner join Tbl_PizzaExtra px on pod.PizzaExtraId=px.PizzaExtraId
              where pod.PizzaOrderInvoiceNo= @PizzaOrderInvoiceNo";
    }
}
