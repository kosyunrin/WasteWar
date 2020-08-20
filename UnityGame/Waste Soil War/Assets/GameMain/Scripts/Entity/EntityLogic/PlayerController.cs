
namespace GameName
{
    public class PlayerController : Entity
    {
        private PlayerControllerData m_player = null;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_player = userData as PlayerControllerData;
           
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}
