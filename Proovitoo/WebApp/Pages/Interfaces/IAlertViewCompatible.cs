namespace WebApp.Pages.Interfaces;

/// <summary>
///     Võimaldab kasutada _PartiaAlertView vaadet. Vajalik erinevate teadete lehele kuvamiseks.
/// </summary>
public interface IAlertViewCompatible
{
    public string? ErrorMessage { get; set; }
    public string? WarningMessage { get; set; }
    public string? SuccessMessage { get; set; }
}