using SVL.Core.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace SVL.Core.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto login);
    }
}
