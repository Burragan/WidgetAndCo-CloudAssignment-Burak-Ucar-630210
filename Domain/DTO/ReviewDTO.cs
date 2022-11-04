using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Domain.DTO
{
    public class ReviewDTO
    {
        [JsonRequired]
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        [JsonRequired]
        public string reviewText { get; set; }
    }
}
