using UnityEngine;
using System.Collections;


namespace SK
{
    //[RequireComponent(typeof(MeshCollider))]

    public class CsDragObj : MonoBehaviour
    {

        private Vector3 screenPoint;
        private Vector3 offset;
        public bool StopDragMono { get; set; }
        private void Awake()
        {
        }
        void Start()
        {
            //SceneButtonCSmananger.Instance.CanMoveCamera = true;
            //StopDragMono = false;
        }
        private void Update()
        {
        }

        void OnMouseDown()
        {
            if (StopDragMono) return;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        }

        void OnMouseDrag()
        {
            if (StopDragMono) return;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            SceneButtonCSmananger.Instance.CanMoveCamera = false;
        }
        private void OnMouseUp()
        {
            if (StopDragMono) return;
            SceneButtonCSmananger.Instance.CanMoveCamera = true;
        }
    }
}


