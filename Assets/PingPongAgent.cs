using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PingPongAgent : Agent
{
   public int agentId;
   public GameObject ball;
   Rigidbody ballRb;


    public override void Initialize(){
        this.ballRb= this.ball.GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor){
        float dir = (agentId == 0)? 1.0f:-1.0f;
        sensor.AddObservation(this.ball.transform.localPosition.x * dir); //ボールのX座標
        sensor.AddObservation(this.ball.transform.localPosition.z * dir); //ボールのZ座標
        sensor.AddObservation(this.ballRb.velocity.x * dir); //ボールのX速度
        sensor.AddObservation(this.ballRb.velocity.x * dir); //ボールのX速度

        sensor.AddObservation(this.transform.localPosition.x*dir); //パドルのX座標 
    }

    void OnCollisionEnter(Collision collision){
        AddReward(0.1f);
    }

    public override void OnActionReceived(float[] vectorAction){
        // PingPongAgent の移動
        float dir = (agentId==0)? 1.0f :-1.0f;
        int action = (int)vectorAction[0];
        Vector3 pos = this.transform.localPosition;
        if(action==1){
            pos.x -= 0.2f * dir;
        }
        else if (action == 2){
            pos.x += 0.2f * dir;
        }
        if(pos.x < -4.0f) pos.x=-4.0f;
        if(pos.x > 4.0f) pos.x = 4.0f;
        this.transform.localPosition = pos;

    }

    public override void Heuristic(float[] actionsOut){
        actionsOut[0]=0;
        if(Input.GetKey(KeyCode.LeftArrow)) actionsOut[0] =1 ;
        if(Input.GetKey(KeyCode.RightArrow)) actionsOut[0] = 2; 
    }

}
