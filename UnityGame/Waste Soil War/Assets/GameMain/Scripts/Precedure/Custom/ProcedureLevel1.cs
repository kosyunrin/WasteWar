
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameName
{
    public class ProcedureLevel1 : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        private ScoreForm m_ScoreForm = null;
        private PlayerHUDForm m_PlayerForm = null;

        private bool isGameClear = false;

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameFrameworkLog.Info("ProcedureLevel1 OnEnter");
           // GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.UI.OpenUIForm(UIFormId.ScoreForm, this);
            GameEntry.UI.OpenUIForm(UIFormId.PlayerHUDForm, this);

            GameEntry.Entity.ShowPlayer(new PlayerControllerData(GameEntry.Entity.GenerateSerialId(),1));
            GameEntry.Entity.ShowStart(new StartData(GameEntry.Entity.GenerateSerialId(), 4));
            GameEntry.Entity.ShowEnd(new EndData(GameEntry.Entity.GenerateSerialId(), 5));

            GameEntry.Event.Subscribe(GotoNextSceneEventArgs.EventId, OnGameClear);

        }

        private void OnGameClear(object sender, GameEventArgs e)
        {
            isGameClear = true;
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            //GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(GotoNextSceneEventArgs.EventId, OnGameClear);

        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (Input.GetKeyDown(KeyCode.G))
            {
                isGameClear = true;

            }

            if (isGameClear)
            {
                isGameClear = false;
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.Level2"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;

            GameFrameworkLog.Info("OpenScoreForm");

            if (ne.UserData != this)
            {
                GameFrameworkLog.Info("OpenScoreForm False");
                return;
            }

            m_ScoreForm = (ScoreForm)ne.UIForm.Logic; ;
            m_PlayerForm = (PlayerHUDForm)ne.UIForm.Logic;
        }
    }
}