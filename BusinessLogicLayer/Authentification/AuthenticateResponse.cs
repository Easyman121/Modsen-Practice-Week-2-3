using BusinessLogicLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Authentification
{
    public class AuthenticateResponse
    {
        public UserResponseDto User { get; set; }
        public string Token { get; set; }
    }
}
