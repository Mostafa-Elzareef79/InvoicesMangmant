using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Features.InvoicesFeatures.Query
{
    public class AllInvoicesVM
    {
        public int? Id { get; set; }
        
        public DateTime? InvoiceDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }
    public class InvoiceItem
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? InvoiceId { get; set; }
    }
}
