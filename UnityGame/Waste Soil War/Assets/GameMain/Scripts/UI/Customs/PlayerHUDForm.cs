
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameName
{


    public class PlayerHUDForm : UGuiForm
    {
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }
    }
}