using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;

public class CSTestScene : MonoBehaviour
{
    private GameObject asd { get; set; }
    private void Awake()
    {
        asd = Resources.Load<GameObject>("Prefabs/OperatingPart/MainPart/Move/GuaWaMove");
        asd= Instantiate(asd);

        Destroy(asd.GetComponent<MonoController>());
        asd.AddComponent<CSMoveChimeraController>();
        var cschimera = asd.GetComponent<CSMoveChimeraController>();
        MonoData data = new MonoData(30, BodyType.MainPart, 30);
        cschimera.SetMonoData(data);
        cschimera.FirstState(new CSPlayState(), cschimera);

    }
    // Start is called before the first frame update
    void Start()
    {
        //var monocontroller = asd.GetComponent<MonoController>();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("asdaasdad");
           
        }
    }
}
