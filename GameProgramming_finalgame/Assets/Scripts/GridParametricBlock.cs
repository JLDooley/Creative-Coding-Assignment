using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIDCITY
{
    public class GridParametricBlock : MonoBehaviour
    {

        #region Fields
        public Transform basePrefab;
        private GridCityManager cityManager;
        public Transform startPoint, endPoint;
        public Transform spaceScannerPrefab;

        private int bottomLeftNearIndX, bottomLeftNearIndZ, bottomLeftNearIndY;
        private int topRightFarIndX, topRightFarIndZ, topRightFarIndY;
        private Vector3 midPoint;
        private Vector3 scaleVec;

        private bool scanReady = false;
        #endregion

        #region Properties	
        #endregion

        #region Methods

        public void Initialize(Vector3 startVec, Vector3 endVec)
        {
            //Adjust corners to nearest whole world unit
            startVec = new Vector3(Mathf.RoundToInt(startVec.x), Mathf.RoundToInt(startVec.y), Mathf.RoundToInt(startVec.z));
            endVec = new Vector3(Mathf.RoundToInt(endVec.x), Mathf.RoundToInt(endVec.y), Mathf.RoundToInt(endVec.z));

            int startX = Mathf.RoundToInt(startVec.x + 20.01f);
            int startY = Mathf.RoundToInt(startVec.y);
            int startZ = Mathf.RoundToInt(startVec.z + 20.01f);

            int endX = Mathf.RoundToInt(endVec.x + 20.01f);
            int endY = Mathf.RoundToInt(endVec.y);
            int endZ = Mathf.RoundToInt(endVec.z + 20.01f);

            //Debug.Log("Monolith Adjusted Coords");
            //Debug.Log("StartX: " + startX);
            //Debug.Log("StartY: " + startY);
            //Debug.Log("StartZ: " + startZ);
            //Debug.Log("EndX: " + endX);
            //Debug.Log("EndY: " + endY);
            //Debug.Log("EndZ: " + endZ);

            if (startX<endX)
            {
                bottomLeftNearIndX = startX;
                topRightFarIndX = endX;
            }
            else
            {
                bottomLeftNearIndX = endX;
                topRightFarIndX = startX;
            }

            if (startY < endY)
            {
                bottomLeftNearIndY = startY;
                topRightFarIndY = endY;
            }
            else
            {
                bottomLeftNearIndY = endY;
                topRightFarIndY = startY;
            }

            if (startZ < endZ)
            {
                bottomLeftNearIndZ = startZ;
                topRightFarIndZ = endZ;
            }
            else
            {
                bottomLeftNearIndZ = endZ;
                topRightFarIndZ = startZ;
            }

            


            //int widthx = topRightFarIndX - bottomLeftNearIndX;
            //int widthz = topRightFarIndZ - bottomLeftNearIndZ;
            //int heighty = topRightFarIndY - bottomLeftNearIndY;
            //for (int x = bottomLeftNearIndX+(widthx/5); x < topRightFarIndX-(widthx/5); x++)
            //    for (int y = bottomLeftNearIndY; y < (bottomLeftNearIndY+heighty*2/3); y++)
            //        for (int z = bottomLeftNearIndZ; z < topRightFarIndZ; z++)
            //        {
            //            cityManager.SetSlot(x, y, z, false);
            //        }
            //for (int x = bottomLeftNearIndX; x < topRightFarIndX; x++)
            //    for (int y = bottomLeftNearIndY; y < (bottomLeftNearIndY+heighty * 2 / 3) ; y++)
            //        for (int z = bottomLeftNearIndZ + (widthz/5); z < topRightFarIndZ-(widthz/5); z++)
            //        {
            //            cityManager.SetSlot(x, y, z, false);
            //        }



            midPoint = ((startVec + endVec) / 2.0f);
            scaleVec = (endVec - midPoint)*2.0f;
            midPoint.y = Mathf.Min(startVec.y, endVec.y);
            Transform newObject = Instantiate(basePrefab, midPoint + new Vector3(-0.5f, 0f, -0.5f), Quaternion.identity, cityManager.transform); //Added offset by 0.5 units to align Set Slots
            newObject.localScale = scaleVec;
            scanReady = true;




        }

        #region Unity Methods

        // Use this for internal initialization
        void Awake()
        {
            
        }

        // Use this for external initialization
        void Start()
        {
            cityManager = GridCityManager.Instance;
            Initialize(startPoint.position, endPoint.position);
                        
        }

        private void Update()
        {
            if (scanReady)
            {
                for (int x = bottomLeftNearIndX; x < topRightFarIndX; x++)
                                            for (int y = bottomLeftNearIndY; y < topRightFarIndY; y++)
                                                for (int z = bottomLeftNearIndZ; z < topRightFarIndZ; z++)
                                                {
                                                    //Instantiate(spaceScannerPrefab, new Vector3(x - 20, y, z - 20), Quaternion.identity);

                                                    //Collider[] slotScannerOuter = Physics.OverlapSphere(new Vector3(x - 20f, y + 0.51f, z - 20f), 0.5f);
                                                    Collider[] slotScanner = Physics.OverlapBox(new Vector3(x - 20f, y + 0.51f, z - 20f), new Vector3(0.5f, 0.5f, 0.5f));
                                                    //Debug.Log("Scanner Array Length: " + slotScanner.Length);
                                                    if (slotScanner.Length > 0)
                                                    {
                                                        cityManager.SetSlot(x, y, z, true);
                                                    }

                                                }
                scanReady = false;
            }
        }


        #endregion
        #endregion

    }
}

