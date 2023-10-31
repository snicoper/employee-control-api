using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.TimeControl.Commands.FinishTimeControl;

public record FinishTimeControlCommand(string EmployeeId) : IRequest<Result>;
