namespace UI
{
    public class UIPanelMainMenu : UIPanelBase
    {
        public void OnClick_Logout()
        {
            GameManager.Instance.DbHandler.LogoutPlayer();
            
            GameManager.Instance.ClearPlayer();
            PanelHandler.ChangePanel(UIPanelHandler.PanelState.Login);
        }
    }
}