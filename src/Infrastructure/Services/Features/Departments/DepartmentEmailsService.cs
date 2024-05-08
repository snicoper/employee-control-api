﻿using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Emails;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models.Emails;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Infrastructure.Services.Features.Departments;

public class DepartmentEmailsService(
    IEmailService emailService,
    IOptions<WebApiSettings> webApiSettings,
    IStringLocalizer<TaskResource> localizer)
    : IDepartmentEmailsService
{
    public async Task SendEmployeeAssignDepartmentAsync(
        Department department,
        Company company,
        List<User> users)
    {
        var siteName = webApiSettings.Value.SiteName ?? string.Empty;
        var companyName = company.Name;
        var departmentName = department.Name;

        // ViewModel.
        var model = new SendEmployeeAssignDepartmentViewModel(siteName, companyName, departmentName);

        // Send email.
        emailService.Subject = localizer["Añadido al departamento {0} desde {1}.", departmentName, siteName];

        foreach (var email in users.Select(user => user.Email ?? string.Empty))
        {
            emailService.To.Add(email);
        }

        await emailService.SendMailWithViewAsync(EmailViews.SendEmployeeAssignDepartment, model);
    }
}
