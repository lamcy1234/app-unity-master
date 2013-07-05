/// <Licensing>
/// © 2011 (Copyright) Path-o-logical Games, LLC
/// If purchased from the Unity Asset Store, the following license is superseded 
/// by the Asset Store license.
/// Licensed under the Unity Asset Package Product License (the "License");
/// You may not use this file except in compliance with the License.
/// You may obtain a copy of the License at: http://licensing.path-o-logical.com
/// </Licensing>
using UnityEngine;
using System.Collections;

/// <description>
///	Constrain this transform to a target's scale, rotation and/or translation.
/// </description>
[AddComponentMenu("Path-o-logical/UnityConstraints/Constraint - Transform - Smooth")]
public class SmoothTransformConstraint : TransformConstraint 
{
    public float positionSpeed = 0.1f;
    public float rotationSpeed = 1;
    public float scaleSpeed = 0.1f;

    public UnityConstraints.INTERP_OPTIONS interpolation =
                                    UnityConstraints.INTERP_OPTIONS.Spherical;


    /// <summary>
    /// Runs each frame while the constraint is active
    /// </summary>
    protected override void OnConstraintUpdate()
    {
        if (this.constrainScale)
            this.SetWorldScale(target);

        this.OutputRotationTowards(this.target.rotation);
        this.OutputPositionTowards(this.target.position);
    }
    

    /// <summary>
    /// Runs when the noTarget mode is set to ReturnToDefault
    /// </summary>
    protected override void NoTargetDefault()
    {
        if (this.constrainScale)
            this.xform.localScale = Vector3.one;

        this.OutputRotationTowards(Quaternion.identity);
        this.OutputPositionTowards(this.target.position);
    }

    /// <summary>
    /// Runs when the constraint is active or when the noTarget mode is set to 
    /// ReturnToDefault
    /// </summary>
    private void OutputPositionTowards(Vector3 destPos)
    {
        // Faster exit if there is nothing to do.
        if (!this.constrainPosition)
            return;

        this.pos = Vector3.Lerp(this.xform.position, destPos, this.positionSpeed);

        // Output only if wanted - faster to invert and set back to current.
        if (!this.outputPosX) this.pos.x = this.xform.position.x;
        if (!this.outputPosY) this.pos.y = this.xform.position.y;
        if (!this.outputPosZ) this.pos.z = this.xform.position.z;

        this.xform.position = pos;
    }

    /// <summary>
    /// Runs when the constraint is active or when the noTarget mode is set to 
    /// ReturnToDefault
    /// </summary>
    private void OutputRotationTowards(Quaternion destRot)
    {
        // Faster exit if nothing to do.
        if (!this.constrainRotation)
            return;

        UnityConstraints.InterpolateRotationTo
        (
            this.xform,
            destRot,
            this.interpolation,
            this.rotationSpeed
        );

        UnityConstraints.MaskOutputRotations(this.xform, this.output);
    }


    /// <summary>
    /// Sets this transform's scale to equal the target in world space.
    /// </summary>
    /// <param name="sourceXform"></param>
    public override void SetWorldScale(Transform sourceXform)
    {
        // Set the scale now that both Transforms are in the same space
        this.xform.localScale = Vector3.Lerp
        (
            this.xform.localScale, 
            this.GetTargetLocalScale(sourceXform), 
            this.scaleSpeed
        );
    }
}


