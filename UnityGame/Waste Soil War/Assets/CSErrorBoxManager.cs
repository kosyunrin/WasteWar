using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SK.ERROR
{
    public class CSErrorBoxManager : MonoBehaviour
    {
        public static CSErrorBoxManager Instance = null;
        private Image mImage = null;
        private Text mText = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            mImage = this.GetComponent<Image>();
            //mImage.enabled = false;
            mText = this.transform.GetChild(0).GetComponent<Text>();
            //mText.enabled = false;
            CloseErrorBox();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) this.gameObject.SetActive(false);
        }
        public void SendErrorMessages(string Message)
        {
            this.gameObject.SetActive(true);
            mText.text = Message;
        }
        public void CloseErrorBox()
        {
            this.gameObject.SetActive(false);
            //mImage.enabled = false;
            //mText.enabled = false;
        }
        public void OpenCloseErrorbox(bool b)
        {
            this.gameObject.SetActive(b);
        }
        //public void OpenErrorBox()
        //{
        //    mImage.enabled = false;
        //    mText.enabled = false;
        //}
    }
}
