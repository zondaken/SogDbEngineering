using Database;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIPanelLogin : UIPanelBase
    {
        [SerializeField] private TMP_InputField tbxUsername;
        [SerializeField] private TMP_InputField tbxPassword;
        [SerializeField] private TextMeshProUGUI lbAlert;

        private GameManager GameManager => GameManager.Instance;
        private DbHandler DbHandler => GameManager.DbHandler;

        public override void OnShow()
        {
            base.OnShow();
            
            tbxUsername.text = tbxPassword.text = lbAlert.text = "";
        }

        public void OnClick_Login()
        {
            string username = tbxUsername.text;
            string password = tbxPassword.text;
            
            if (DbHandler.TryLogin(username, password, out var player))
            {
                PanelHandler.ChangePanel(UIPanelHandler.PanelState.MainMenu);
                GameManager.Instance.SetPlayer(player);
            }
            else
            {
                tbxPassword.text = "";
                lbAlert.text = "Invalid username or password";
            }
        }
    }
}