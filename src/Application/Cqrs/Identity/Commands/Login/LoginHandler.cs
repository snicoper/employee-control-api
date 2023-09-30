﻿using EmployeeControl.Application.Common.Interfaces;
using MediatR;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginDto>
{
    private readonly ILoginService _loginService;

    public LoginHandler(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var jwt = await _loginService.LoginAsync(request.UserName, request.Password);

        return new LoginDto { Token = jwt };
    }
}
