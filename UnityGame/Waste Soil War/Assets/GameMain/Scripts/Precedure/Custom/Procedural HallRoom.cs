using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameName
{
    public class ProceduralHallRoom : ProcedureBase
    {
        private bool m_StartGame = false;
        private MenuForm m_MenuForm = null;
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }
        public void StartGame()
        {
            m_StartGame = true;
        }
        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {

            GameFrameworkLog.Info("ProceduralHallRoom OnEnter");

            base.OnEnter(procedureOwner);

            UIComponent ui = GameEntry.UI.GetComponent<UIComponent>();
            ui.OpenUIForm(UIFormId.MenuForm, this);
            
            //m_MenuForm.Visible=false;

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            //GameEntry.UI.OpenUIForm(UIFormId.MenuForm, this);
           
            Debug.Log(m_MenuForm);
           




        }


        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);

            Debug.Log(m_MenuForm);

        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
           GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            Debug.Log("xxxxxlikai");

            //UIComponent ui = GameEntry.UI.GetComponent<UIComponent>();
            //ui.OpenUIForm(UIFormId.MenuForm, this);
            //m_MenuForm = ui.GetUIForm(UIFormId.MenuForm).GetComponent<MenuForm>();
            m_MenuForm.Close(isShutdown);
            //if (m_MenuForm != null)
            //{
            //    m_MenuForm.Close(isShutdown);
            //    m_MenuForm = null;
            //}

            SK.WARRROOM.CSGAMEROOM.Instance.SetHero();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.GameRoom"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }

        }
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            
            m_MenuForm = (MenuForm)ne.UIForm.Logic;
            m_MenuForm.Visible = false;
        }
    }
}
