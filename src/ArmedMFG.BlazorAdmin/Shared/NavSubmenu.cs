namespace ArmedMFG.BlazorAdmin.Shared;

public class NavSubmenu
{
    public NavSubmenuStatus Status { get; set; }

    public NavSubmenu()
    {
        Status = NavSubmenuStatus.None;
    }

    public void Toggle(NavSubmenuStatus status)
    {
        Status = Status == status ? NavSubmenuStatus.None : status;
    }
}

public enum NavSubmenuStatus
{
    None,
    First,
    Second
}
