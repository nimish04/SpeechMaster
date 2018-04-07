using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using System;

using UnityEngine.SceneManagement;

public class player : MonoBehaviour {
    
    public TextMesh infotext;

    public float timer=20f;

    public int flag=0;

    public int[,] arrayaud = new int[6,8]; 

    public int countAud=0;

    public int flag2 = 0;

    public int flag3=0;

    public float score1=0;

    public float score2 = 0;

    public int count=0;

    public float threshold_time=1.0f;

    public float ins_velocity=0;

    public float avg_velocity=0;

    public float dist=0;

    public int t = 0;

    public int flag4=0;

    public float totalscore=0;

    public float averagevelocity = 0;

    //public float no_of_frames=0;

    //public int totalframes=int(timer/Time.del)

    public string[] aud = new string[6300];

    public string current="";

    public float diff;

    public float restarttimer = 5f;


    // Use this for initialization

    void Start () {

        for (int i = 1; i <= 5; i++)
            for (int j = 1; j <= 7; j++) {
                arrayaud [i, j] = 0;
            }
    
        infotext.text="The time limit for this game is 20 seconds.\n";

        infotext.text += "Give your speech and inorder to achieve good score.\n" +
            
            "Make eye-to-eye contact with the people.\n"+

            "You get more score for steady and smooth movement of head.\n"
        
            +"Scoring:\n"

            +"Excellent:4-5\n"

            +"Very Good:3-4\n"+

            "Good and Scope for improvement:2-3\n"

            +"Average and need for improvement:1-2\n"

            +"Practice sppechmaster a lot:0-1\n"

            +"PRESS MAGNETIC TRIGGER TO START!!\n"

            +"ENJOY!!!";
    
    }
    
    // Update is called once per frame
    void Update () {

        RaycastHit hit;

        if (timer>0.0f && flag==1){
        
            if (Physics.Raycast (transform.position, transform.forward, out hit)) {
            
                //Debug.Log (hit.transform.name);
                aud [flag2] = hit.transform.name;

                if (hit.transform.name [1] == ',') {

                    countAud += 1;
                    //Debug.Log ((int)hit.transform.name[0]-48);

                    arrayaud[(int)hit.transform.name[0]-48,(int)hit.transform.name[2]-48]+=1;
                
                }

                flag2++;
            }
            if (flag2 >=1 && flag4==0 && hit.transform.name[1]==',') {
            
                current = hit.transform.name;

                flag4 = 1;

                t = 1;
            
            } else if(flag4==1){

                if (String.Compare (current, hit.transform.name) == 0) {

                    ins_velocity += 0;

                    current = hit.transform.name;

                    t += 1;
                } else {

                    if (current [1] == ',' && hit.transform.name [1] == ',') {

                        t += 1;

                        dist = (float)Math.Pow (((current [0] - hit.transform.name [0]) *(current [0] - hit.transform.name [0]) + (current [2] - hit.transform.name [2]) * (current [2] - hit.transform.name [2])), 2);

                        ins_velocity += dist/(Time.deltaTime*t);

                        current = hit.transform.name;

                        t = 0;

                    }
                }
            }
        }
        if (Input.GetButtonDown ("Fire1") && timer>0.0f) {

            infotext.text = "Timer:" + Math.Round (timer, 1);

            flag = 1;
        
        } else if (flag == 1 && timer>0.0f) {
        
            infotext.text = "Timer:" + Math.Round (timer,1);
        }

        if (timer > 0.0f && flag==1) {
        
            timer -= Time.deltaTime;
        }

        else if(flag==1 && flag3==0){
        
            timer = 0.0f;

            flag3 = 1;
            //Application.Quit ();
            for (int i = 1; i < 6; i++) {
                for (int j = 1; j < 8; j++)
                    if (arrayaud [i,j] != 0) {
                        count += 1;
                    }
            }

            score1 = (float)((count * 2.5) / 35);

            avg_velocity = ins_velocity / flag2;
            if (avg_velocity == 0.0f) {
                score2 = 0.0f;
            }
            else if (avg_velocity > 0.0f && avg_velocity < 2.0f) {

                score2 = 2.5f;
            
            } else {

                diff = 400.0f - avg_velocity;

                score2 = (float)diff * (2.5f) / 398f;
            }

            totalscore =(float) Math.Round(score1 + score2,2);

            infotext.text = "Your final score is=" + totalscore+"/5"+"\n";



            if (totalscore > 4 && totalscore <= 5) {

                infotext.text += "Excellent Performance\n";
            
            } else if (totalscore > 3 && totalscore <= 4) {

                infotext.text += "Very good interaction!!\n";
            
            } else if (totalscore > 2 && totalscore <= 3) {

                infotext.text += "Good and scope of improved\n";
            
            } else if (totalscore > 1 && totalscore <= 2) {

                infotext.text += "Average and can be improved\n";
            
            } else {

                infotext.text += "Practice a lot with SPEECH MASTER\n";
            }

            infotext.text+="Game will automatically restart in 5sec\n";
        }

        if (flag3 == 1) {
        
            restarttimer -= Time.deltaTime;

            if (restarttimer <= 0f) {

                SceneManager.LoadScene (SceneManager.GetActiveScene().name);
            }
        }
    }
}
