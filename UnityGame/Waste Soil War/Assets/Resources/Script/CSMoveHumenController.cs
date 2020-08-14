using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{
    public class CSMoveHumenController : MonoController
    {
        // Start is called before the first frame update
        void Start()
        {
            FirstState(new CSShowState(), this);
           
        }

        // Update is called once per frame
        void Update()
        {
            MonoExecute();
        }
    }
}
