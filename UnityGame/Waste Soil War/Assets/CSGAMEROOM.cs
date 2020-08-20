using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK.WARRROOM
{
    public class CSGAMEROOM : MonoBehaviour
    {
        public static CSGAMEROOM Instance = null;

        public CSItemChimeras m_Hero { get; set; }
        private void Awake()
        {
            if (!Instance) Instance = this;
        }

        public void CreateMyHero()
        {
            GameObject _MoveMono = Resources.Load<GameObject>(m_Hero.MoveMonoPath);
            _MoveMono = Instantiate(_MoveMono);

            Destroy(_MoveMono.GetComponent<MonoController>());
            _MoveMono.AddComponent<CSMoveChimeraController>();
            CSMoveChimeraController xcon = _MoveMono.GetComponent<CSMoveChimeraController>();
            xcon.FirstState(new CSPlayState(), xcon);
            xcon.SetMonoData(new MonoData(m_Hero.ID, m_Hero.BType, m_Hero.Other));

            ChimeraData Xchimera = m_Hero.PartDates[0];
            GameObject _part = Resources.Load<GameObject>(Xchimera.PartPath);
            _part = GameObject.Instantiate(_part);
            Destroy(_part.GetComponent<CsDragObj>());
            _part.transform.position = Xchimera.partPos;
            _part.transform.rotation = Quaternion.Euler(Xchimera.partRot);

            _part.transform.SetParent(CSCompleteController.Instance.GetBoneParentAtName(_MoveMono.transform, Xchimera.BoneParentName));

            var xg = GameObject.Find("Main Camera");
            if (!xg) return;
            var xcswar = xg.GetComponent<CSWarManagement>();
            if(xcswar)
            {
                xcswar.Player = _MoveMono.GetComponent<Transform>();
                xcswar.Offset = xcswar.Player.position - xg.transform.position;
            }
        }

        public void SetHero()
        {
            if (CSChimeraShowController.Instance.CurrentShowChimera)
            {
                var monodata = CSChimeraShowController.Instance.CurrentShowChimera.transform.GetChild(0).GetComponent<MonoController>().monoData;
                m_Hero = (CSItemChimeras)CSDataManagement.Instance.GetAllBodyPart(monodata.ID);
            }
        }
    }
}
