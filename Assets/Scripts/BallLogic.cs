using JetBrains.Annotations;
using System.Numerics;

using Unity.MLAgents.Sensors;
using UnityEngine;
using Unity.MLAgents;
using Vector3 = UnityEngine.Vector3;

public class BallLogic : Agent
{
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    public Transform target;
    public override void OnEpisodeBegin()
    {
        //Reset Agent
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-9, 0.5f, 0);
        //Move target to new spot
        target.localPosition = new Vector3(12 + Random.value * 8, Random.value * 3, Random.value * 10 - 5);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //Target and Agent Positions & Agent velocities
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(rBody.velocity);

    }
    public float speed = 20;
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];

        if (vectorAction[1] == 2)
        {
            controlSignal.z = 1;
        }
        else
        {
            controlSignal.z = -vectorAction[1];
        }
        if (this.transform.localPosition.x < 8.5)
        {
            rBody.AddForce(controlSignal * speed);
        }
        float distancetoTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);
        //reward
        if (distancetoTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        //fall of platform
        if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }

    }
    public override void Heuristic(float[] ActionsOut)
    {
        ActionsOut[0] = Input.GetAxis("Vertical");
        ActionsOut[1] = Input.GetAxis("Horizontal");
    }


}
