namespace Starbucks.MenuManager.API.Application.Exceptions
{
    public sealed record ValidationError(string PropertyName,  string ErrorMessage);
}
