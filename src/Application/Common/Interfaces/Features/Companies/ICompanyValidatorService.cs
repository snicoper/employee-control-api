﻿using EmployeeControl.Domain.Common;

namespace EmployeeControl.Application.Common.Interfaces.Features.Companies;

public interface ICompanyValidatorService
{
    Task<Result> UniqueNameValidationAsync(string companyName, Result result, CancellationToken cancellationToken);
}
