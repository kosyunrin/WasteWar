using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    
    public enum PlayerEventsState
    {
        StopMove,
        Move,
    }
    public class CSPlayerController : CSPlayerBase
    {
        private Animator ani;
        private CSTouchMove touch = null;
        private Rigidbody controller = null;
        private float moveSpeed = 1.0f;
        private Vector3 WallDir = Vector3.zero;
        private bool IsWall = false;
        public PlayerEventsState mEventsState { get; set; }
        void Start()
        {
            ani = GetComponent<Animator>();
            touch = CSTouchMove.SharedInstance;
            controller = GetComponent<Rigidbody>();
            ani.SetFloat("Blend", 0.0f);
            mEventsState = PlayerEventsState.Move;
            IsWall = false;
        }
        private void Update()
        {
        }

        void FixedUpdate()
        {
            switch(mEventsState)
            {
               
                case PlayerEventsState.Move:
                    CharacterMove();
                    break;
                case PlayerEventsState.StopMove:
                    break;
            }
        }

        private void CharacterMove()
        {
            float hor = touch.Horizontal;
            float ver = touch.Vertical;

            var direction = new Vector3(hor, 0, ver);

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {     //is hit 
                var hit = hitInfo.transform;
                if (hit.tag == "Wall")
                {
                    var fa = hitInfo.normal;
                    var NowDir = Vector3.Cross(fa, Vector3.up);
                    WallDir = NowDir;
                    var angle = Vector3.Dot(NowDir, direction);
                    WallDir = (angle > 0) ? WallDir : -WallDir;
                    var xthisCollider = this.GetComponent<Collider>().bounds;
                    if (hitInfo.collider.bounds.Intersects(xthisCollider))
                    {
                        IsWall = true;
                    }
                }
            }
            else
            {
                IsWall = false;
            }

            if (direction != Vector3.zero)
            {
                float newSpeed = Mathf.Lerp(ani.GetFloat("Blend"), 10.0f, Time.deltaTime * 5);
                ani.SetFloat("Blend", newSpeed);
                if (!IsWall)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
                    controller.velocity = direction.normalized * ani.GetFloat("Blend") * moveSpeed;
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(WallDir), Time.deltaTime * 10);
                    controller.velocity = WallDir.normalized * ani.GetFloat("Blend") * moveSpeed;
                }

            }
            else
            {
                float newSpeed = Mathf.Lerp(ani.GetFloat("Blend"), 0, Time.deltaTime * 5);
                ani.SetFloat("Blend", newSpeed);
            }
        }
    }
}