using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using MyBox;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    [Tag]
    public string m_InteractableTag;

    public Vector3 m_Delta  = Vector3.zero;

    private Vector3 m_LastPosition = Vector3.zero;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private SteamVR_Behaviour_Skeleton m_Skeleton = null;
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable;
    private List<Interactable> m_ContactInteractables = new List<Interactable>();
    private bool m_ObjectInHand = false;

    private VRController m_VRController = null;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Skeleton = GetComponent<SteamVR_Behaviour_Skeleton>();
        m_Joint = GetComponent<FixedJoint>();
        m_VRController = transform.parent.GetComponentInParent<VRController>();
    }

    private void Start()
    {
        m_LastPosition = transform.position;
    }

    private void Update()
    {
        if (m_ObjectInHand == false)
        {
            if (m_ContactInteractables.Count > 0)
            {
                m_CurrentInteractable = GetNearestInteractable();

                if (m_CurrentInteractable.m_Climbable != true)
                {
                    if (m_GrabAction.GetStateDown(m_Pose.inputSource) && m_CurrentInteractable.m_TogglePickUp == true)
                    {
                        if (m_CurrentInteractable.m_ActiveHand == null)
                        {
                            Pickup();
                        }
                        else
                        {
                            Drop();
                        }
                    }
                    else if (m_CurrentInteractable.m_TogglePickUp == false)
                    {
                        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
                        {
                            Pickup();
                        }

                        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
                        {
                            Drop();
                        }
                    }
                }
                else
                {
                    if (m_GrabAction.GetStateDown(m_Pose.inputSource))
                    {
                        Grab();
                    }

                    if (m_GrabAction.GetStateUp(m_Pose.inputSource))
                    {
                        Release();
                    }
                }
            }
        }
        else
        {
            if (m_GrabAction.GetStateUp(m_Pose.inputSource))
            {
                Release();
            }
        }
    }

    private void FixedUpdate()
    {
        m_LastPosition = transform.position;
    }

    private void LateUpdate()
    {
        m_Delta = m_LastPosition - transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(m_InteractableTag))
            return;

        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(m_InteractableTag))
            return;

        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        if (!m_CurrentInteractable)
            return;

        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();

        m_CurrentInteractable.transform.position = transform.position;

        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        m_CurrentInteractable.m_ActiveHand = this;
    }

    public void Drop()
    {
        if (!m_CurrentInteractable)
            return;

        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        m_Joint.connectedBody = null;

        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0f;

        foreach (Interactable interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }

    public void Grab()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        m_ObjectInHand = true;
        m_VRController.m_AllowFall = false;
        m_VRController.SetHand(this);
    }

    public void Release()
    {
        m_VRController.ClearHand();
        m_VRController.m_AllowFall = true;
        m_ObjectInHand = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
