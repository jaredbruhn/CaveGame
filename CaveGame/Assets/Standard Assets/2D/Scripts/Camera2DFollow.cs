using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAboveFactor = 3;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private float m_OffsetY;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private Vector3 m_LookAbovePos;

        Animator camAnim;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            m_OffsetY = (transform.position - target.position).y;
            transform.parent = null;
            camAnim = GetComponent<Animator>();
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
                m_LookAbovePos = lookAboveFactor * Vector3.up;
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
                m_LookAbovePos = Vector3.MoveTowards(m_LookAbovePos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.up*m_OffsetY + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

            m_LastTargetPosition = target.position;
        }

        public void MoveForward()
        {
            camAnim.SetTrigger("MoveForward");
        }
        public void MoveZUp()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
        }
        public void MoveBackward()
        {
            camAnim.SetTrigger("MoveBackward");
        }
        public void MoveZDown()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
        }

    }
}
