using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class CSPlayerController : CSPlayerBase
    {
        private Animator ani;
        private CSTouchMove touch=null;

        void Start()
        {
            ani = GetComponent<Animator>();
            touch = CSTouchMove.SharedInstance;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            if (touch == null) return; 



            float hor = touch.Horizontal;
            float ver = touch.Vertical;

            Vector3 direction = new Vector3(hor, 0, ver);

            if (direction != Vector3.zero)
            {
                float newSpeed = Mathf.Lerp(ani.GetFloat("Blend"), 10.0f, Time.deltaTime * 5);
                ani.SetFloat("Blend", newSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
            }
            else
            {
                float newSpeed = Mathf.Lerp(ani.GetFloat("Blend"), 0, Time.deltaTime * 5);
                ani.SetFloat("Blend", newSpeed);
            }
        }
    }
}