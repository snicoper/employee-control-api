using MediatR;

namespace EmployeeControl.Application.Home.Queries.Prueba;

public class PruebaHandler : IRequestHandler<PruebaQuery, PruebaDto>
{
    public Task<PruebaDto> Handle(PruebaQuery request, CancellationToken cancellationToken)
    {
        var result = new PruebaDto { Nombre = "Salvador Nicolas", Email = "snicoper@example.com" };

        return Task.FromResult(result);
    }
}
