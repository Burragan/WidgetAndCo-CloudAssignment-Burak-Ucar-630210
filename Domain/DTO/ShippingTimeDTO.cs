using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ShippingTimeDTO
    {
        [JsonRequired]
        public int ProductId { get; set; }
        [JsonIgnore]
        public DateTime? ShippingDate { get; set; }
    }
}
