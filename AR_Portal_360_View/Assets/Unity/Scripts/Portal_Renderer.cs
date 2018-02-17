using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AR_360View.Unity.Script
{
    public class Portal_Renderer : MonoBehaviour
    {
        [Header("Material")]
        [SerializeField]
        private Material[] materials;
        [Header("Device")]
        [SerializeField]
        private Transform deviceControl;
        [Header("World")]
        [SerializeField]
        private bool wasInFrontOfThePortal;
        [SerializeField]
        private bool nextWorld;
        [SerializeField]
        private bool hasCollided;

        private void Start()
        {
            SetMaterial(false);
        }

        private void Update()
        {
            WhileCameraColliding();
        }

        private void SetMaterial(bool fullrender)
        {
            var stencilTest = fullrender ? CompareFunction.NotEqual : CompareFunction.Equal;
            Shader.SetGlobalInt("_StencilTest", (int)stencilTest);
        }

        private bool GetIsInFront()
        {
            Vector3 worldpos = deviceControl.position + deviceControl.forward * Camera.main.nearClipPlane;
            Vector3 pos = transform.InverseTransformPoint(worldpos);
            return pos.z >= 0 ? true : false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform != deviceControl)  
                return;
                wasInFrontOfThePortal = GetIsInFront();
            hasCollided = true;
        }

        //Outside of other World
        private void OnTriggerExit(Collider other)
        {
            if (other.transform != deviceControl) 
                return;
            hasCollided = false;
        }

        void WhileCameraColliding()
        {
            if (!hasCollided)       
                return;
            bool isInFront = GetIsInFront();
            if ((isInFront && !wasInFrontOfThePortal) || (wasInFrontOfThePortal && !isInFront)) 
            {
                nextWorld = !nextWorld;
                SetMaterial(nextWorld);
            }
            wasInFrontOfThePortal = isInFront;
        }

        private void OnDestroy()
        {
            SetMaterial(true);
        }
    }
}
