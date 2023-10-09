using AutoMapper;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public RegisterHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<RegisterCommand, ApplicationUser>(request);
        var password = request.Password.NotNull();

        var result = await _identityService.CreateUserAsync(user, password);

        return result.Id;
    }
}
