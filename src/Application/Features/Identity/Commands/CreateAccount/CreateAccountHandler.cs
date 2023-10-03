using AutoMapper;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.CreateAccount;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, string>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateAccountHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<CreateAccountCommand, ApplicationUser>(request);
        var password = request.Password.NotNull();

        var result = await _identityService.CreateUserAsync(user, password);

        return result.Id;
    }
}
