using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CloseIncidence;

public record CloseIncidenceCommand(string Id) : IRequest<Result>;
