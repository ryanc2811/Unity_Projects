using Cinemachine;
using Driving.Inputs.Controls;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffsetY = new Vector2(1f, 6f);
    [SerializeField] private Vector2 maxFollowOffsetX = new Vector2(-6f, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(0.25f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null){ return controls; }
            return controls = new Controls();
        }
    }
    private CinemachineTransposer transposer;

    public override void OnStartAuthority()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        virtualCamera.gameObject.SetActive(true);
        enabled = true;

        Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;

        float followOffsetY = Mathf.Clamp(
            transposer.m_FollowOffset.y-(lookAxis.y*cameraVelocity.y*deltaTime),
            maxFollowOffsetY.x,
            maxFollowOffsetY.y);

        float followOffsetX = Mathf.Clamp(
            transposer.m_FollowOffset.x - (lookAxis.x * cameraVelocity.x * deltaTime),
            maxFollowOffsetX.x,
            maxFollowOffsetX.y);
        transposer.m_FollowOffset.y = followOffsetY;
        transposer.m_FollowOffset.x = followOffsetX;
        //playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    }
}
