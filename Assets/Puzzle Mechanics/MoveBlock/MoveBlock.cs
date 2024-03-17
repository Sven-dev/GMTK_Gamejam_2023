﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveBlock : Powerable
{
    [Header("Moveblock variables")]
    [SerializeField] [Range(1, 20)] private int Length = 3;
    [SerializeField] private Face Direction = Face.Right;

    [Header("System variables")]
    [SerializeField] private Transform Block;
    [SerializeField] private Tilemap Renderer;

    private void FixedUpdate()
    {
        if (!AutoPower)
        {
            Vector2 position = Vector2.Lerp(
                transform.position,
                transform.position + FaceToVector(Direction) * Length,
                PowerLevel);

            Block.position = Vector2.MoveTowards(Block.position, position, 0.75f);

        }
        else //if (AutoPower)
        {
            Vector2 position = Vector2.Lerp(
                transform.position + FaceToVector(Direction) * Length,
                transform.position,
                PowerLevel);

            Block.position = Vector2.MoveTowards(Block.position, position, 0.75f);
        }
    }

    public override void UpdatePower(float power)
    {
        if (PowerLevel + power > PowerLevel)
        {
            Renderer.color = ColorDictionary.Instance.Powered;
        }
        else
        {
            Renderer.color = ColorDictionary.Instance.Unpowered;
        }

        base.UpdatePower(power);
    }

    private Vector3 FaceToVector(Face face)
    {
        switch (face)
        {
            case Face.Up:
                return Vector2.up;
            case Face.Down:
                return Vector2.down;
            case Face.Left:
                return Vector2.left;
            case Face.Right:
                return Vector2.right;
            default:
                throw new System.Exception("Unknown direction");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector2 startPosition = transform.position;
        startPosition += Vector2.left / 2;
        startPosition += Vector2.up / 2;
        Gizmos.DrawWireCube(startPosition, Vector3.one);

        Vector3 endPosition = startPosition;
        endPosition += FaceToVector(Direction) * Length;
        Gizmos.DrawWireCube(endPosition, Vector3.one);

        Gizmos.DrawLine(startPosition, endPosition);
    }
}
