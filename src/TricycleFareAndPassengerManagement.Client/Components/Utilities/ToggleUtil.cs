using MudBlazor;

namespace TricycleFareAndPassengerManagement.Client.Components.Utilities
{
    public static class ToggleUtil
    {
        #region Fields

        public static DrawerVariant _drawerVariant = DrawerVariant.Persistent;
        public static bool _drawerOpen = true;

        #endregion Fields

        #region Public Methods

        public static void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }

        #endregion Public Methods

        #region Protected Methods

        public static void OnBreakpointChanged(Breakpoint breakpoint)
        {
            if (breakpoint <= Breakpoint.Sm)
            {
                _drawerVariant = DrawerVariant.Temporary;
                _drawerOpen = false;
            }
            else
            {
                _drawerVariant = DrawerVariant.Persistent;
                _drawerOpen = true;
            }
        }

        #endregion Protected Methods
    }
}