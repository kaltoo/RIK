namespace WebApp.Pages.Interfaces;

/// <summary>
///     Võimaldab kasutada _PartialTitleView vaadet. Vajalik kujunduselemendi kuvamiseks.
/// </summary>
public interface ITitleViewCompatible
{
    public string TitleViewName { get; set; }
}