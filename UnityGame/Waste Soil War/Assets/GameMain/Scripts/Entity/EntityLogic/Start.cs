namespace GameName
{
    public class Start : Entity
    {
        StartData m_StartData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_StartData = userData as StartData;

            CachedTransform.SetLocalPositionY(0.5f);
            CachedTransform.SetLocalPositionZ(-3);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}