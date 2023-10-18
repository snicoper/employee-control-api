using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Models.Settings;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;

namespace EmployeeControl.Infrastructure.Services;

public class LinkGeneratorService(IOptions<WebAppSettings> options) : ILinkGeneratorService
{
    private readonly WebAppSettings _webAppSettings = options.Value;

    public string GenerateWebApp(string path)
    {
        return $"{_webAppSettings.Scheme}://{_webAppSettings.Host}/{path}";
    }

    public string GenerateWebApp(string path, Dictionary<string, string> queryParams, bool encodeParams = true)
    {
        var queryString = new StringBuilder();

        foreach (var (key, value) in queryParams)
        {
            var valueEncode = encodeParams ? HttpUtility.HtmlEncode(value) : value;
            var paramOperator = queryString.Length == 0 ? "?" : "&";
            queryString.Append($"{paramOperator}{key}={valueEncode}");
        }

        return $"{GenerateWebApp(path)}{queryString}";
    }
}
