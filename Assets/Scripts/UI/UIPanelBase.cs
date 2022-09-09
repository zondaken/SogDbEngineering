using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanelBase : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        protected UIPanelHandler PanelHandler;
        
        public virtual void Initialize(UIPanelHandler uiPanelHandler)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            PanelHandler = uiPanelHandler;
            OnHide();
        }

        public virtual void OnShow()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public virtual void OnHide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}