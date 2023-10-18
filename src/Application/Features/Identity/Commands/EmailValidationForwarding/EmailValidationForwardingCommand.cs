﻿using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.EmailValidationForwarding;

public record EmailValidationForwardingCommand(string UserId) : IRequest<Result>;