/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
        private TrackableBehaviour mTrackableBehaviour;
        public ServerController get;
        bool connected = false;
        #endregion // PRIVATE_MEMBER_VARIABLES


        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();

            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {

            get = FindObjectOfType<ServerController>();
            if (get != null)
            {
                connected = true;
            }
            if (!connected)
            {
                Debug.Log("Connected");
                gameObject.AddComponent<ServerController>();
                get = FindObjectOfType<ServerController>();
                if (get != null)
                {
                    get.initial();
                    get.SetMenuName(mTrackableBehaviour.TrackableName);
                    connected = true;
                }
            }
            else
            {
                get.imagefound();
                Debug.Log(get.GetMenuName());
                Debug.Log(mTrackableBehaviour.TrackableName);
                if (!(get.GetMenuName().Equals(mTrackableBehaviour.TrackableName)))
                {
                    get.unloadimage();
                    get.SetMenuName(mTrackableBehaviour.TrackableName);
                }
            }
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
            Debug.Log(mTrackableBehaviour.TrackableName);
            //get.DownloadImage();
            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

        }


        private void OnTrackingLost()
        {
            if (connected)
            {
                get.imagemiss();
            }
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

        }

        #endregion // PRIVATE_METHODS
    }
}