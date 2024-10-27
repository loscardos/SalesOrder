using Microsoft.Extensions.FileProviders;

namespace SalesOrder.Server.Helper;

public static class ApplicationConstant
{
    public const string AppSettingsPath = "secrets/appsettings.json";
    public const string DbContextConnectionStringSection = "OlSoDev";
    public const string DataNotFound = "Data not found";
    public const string NoContentMessage = "Data is empty.";
    public const string OkMessage = "Ok";
    public const string NotOkMessage = "Something went wrong.";

}