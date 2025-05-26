using MudBlazor;

namespace TricycleFareAndPassengerManagement.Client.Components.Utilities
{
    public static class ShowPasswordUtil
    {
        #region Properties

        public static InputType InputType { get; private set; } = InputType.Password;
        public static string Icon { get; private set; } = Icons.Material.Filled.VisibilityOff;
        public static bool IsPasswordVisible => InputType == InputType.Text;

        #endregion Properties

        #region Public Methods

        public static void Toggle()
        {
            if (InputType == InputType.Password)
            {
                InputType = InputType.Text;
                Icon = Icons.Material.Filled.Visibility;
            }
            else
            {
                InputType = InputType.Password;
                Icon = Icons.Material.Filled.VisibilityOff;
            }
        }

        #endregion Public Methods
    }
}
