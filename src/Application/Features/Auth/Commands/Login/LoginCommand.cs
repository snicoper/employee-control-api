﻿using MediatR;

namespace EmployeeControl.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
