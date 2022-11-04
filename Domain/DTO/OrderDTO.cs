using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Domain.DTO
{
    public class OrderDTO
    {
        [JsonRequired]
        public int ProductId { get; set; }
        [JsonRequired]
        public int UserId{ get; set; }
        public string? ShippingAddress { get; set; }
        [JsonRequired]
        public DateTime OrderDate { get; set; }
        [JsonIgnore]
        public DateTime? ShippingDate { get; set; }
    }
}
