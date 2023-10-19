﻿namespace EmployeeControl.Application.Common.Interfaces.Common;

public interface ILinkGeneratorService
{
    /// <summary>
    /// Genera un link con la configuración de frontend.
    /// </summary>
    /// <param name="path">Path después del servidor.</param>
    /// <returns>Url generada para usar en el frontend.</returns>
    string GenerateWebApp(string path);

    /// <summary>
    /// Genera un link con la configuración de frontend.
    /// </summary>
    /// <param name="path">Path después del servidor.</param>
    /// <param name="queryParams">Parámetros a insertar.</param>
    /// <param name="encodeParams">Usar en los valores de parámetros HttpUtility.HtmlEncode.</param>
    /// <returns>Url generada para usar en el frontend.</returns>
    string GenerateWebApp(string path, Dictionary<string, string> queryParams, bool encodeParams = true);
}
