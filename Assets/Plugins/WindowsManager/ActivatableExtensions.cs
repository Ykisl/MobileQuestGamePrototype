namespace Plugins.WindowsManager
{
    public static class ActivatableExtensions
    {
        public static bool IsActive(this IActivatable a)
        {
            return a.ActivatableState == ActivatableState.Active;
        }

        public static bool IsInactive(this IActivatable a)
        {
            return a.ActivatableState == ActivatableState.Inactive;
        }

        public static bool IsBusy(this IActivatable a)
        {
            return a.ActivatableState == ActivatableState.ToActive ||
                   a.ActivatableState == ActivatableState.ToInactive;
        }

        public static bool IsActiveOrActivated(this IActivatable a)
        {
            return a.ActivatableState == ActivatableState.Active ||
                   a.ActivatableState == ActivatableState.ToActive;
        }

        public static bool IsInactiveOrDeactivated(this IActivatable a)
        {
            return a.ActivatableState == ActivatableState.Inactive ||
                   a.ActivatableState == ActivatableState.ToInactive;
        }
    }
}
