using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.TimeControl.Commands.StartTimeControl;

public record StartTimeControlCommand(string EmployeeId) : IRequest<Result>;
