using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static AtDomain.AccountObjectDm;

namespace AtDomain
{
    public class AtTokensDm
    {
        public class LoginDto
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            public string Device { get; set; }
        }

        public class RefreshDto
        {
            [Required]
            public string AccessToken { get; set; }

            [Required]
            public string RefreshToken { get; set; }
        }

        public class TokenDto
        {
            public string accessToken { get; set; }

            public string refreshToken { get; set; }

            public DateTime expiresRefreshToken { get; set; }
        }

        public class LoginDtoOutput
        {
            public TokenDto Tokens { get; set; }
            public AccountObjectDmOutput AccountObjOutput { get; set; }
            public List<string> ListIdRole { get; set; }
        }
    }
}
