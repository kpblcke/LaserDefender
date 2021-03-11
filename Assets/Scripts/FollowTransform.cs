using System;
using UnityEngine;


    public class FollowTransform : MonoBehaviour
    {
        [SerializeField] private Transform followTransform;

        private void Update()
        {
            if (followTransform)
            {
                transform.position = followTransform.position;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetFollowTransform(Transform toTransform)
        {
            followTransform = toTransform;
        }
    }
