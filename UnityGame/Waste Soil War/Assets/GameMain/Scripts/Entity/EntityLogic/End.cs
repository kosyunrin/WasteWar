using GameFramework;

namespace GameName
{
    public class End : Entity
    {
        EndData m_EndData = null;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_EndData = userData as EndData;
            GameFrameworkLog.Info(m_EndData.Position);

            CachedTransform.SetLocalPositionY(0.5f);
            CachedTransform.SetLocalPositionZ(20);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
       
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            if(other.tag == "Player")
            {
                GameEntry.Event.Fire(this, ReferencePool.Acquire<GotoNextSceneEventArgs>());
            }
        }
    }
}