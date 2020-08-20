using GameFramework.Event;
using System.CodeDom;
//using UnityEditor.PackageManager;

namespace GameName
{

    public class GotoNextSceneEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GotoNextSceneEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public override void Clear()
        {
           
        }
    }
}