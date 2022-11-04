using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Domain.DTO
{
    public class UserDTO
    {
        [JsonRequired]
        public string Email { get; set; }
        [JsonRequired]
        public string FirstName { get; set; }
        [JsonRequired]
        public string LastName { get; set; }
    }
}
