using System;
using UnityEngine;
using MLAgents;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarAgent : Agent
    {
        private CarController m_Car; // the car controller we want to use
        public float rew;
        /*
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }
        */

        Rigidbody rBody;
        public Transform Target;
        void Start()
        {
            rBody = GetComponent<Rigidbody>();
            m_Car = GetComponent<CarController>();

            Screen.SetResolution(640, 480, true);
        }

        /*
        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
        */

        public override void AgentReset()
        {
            //Resetting the car
            this.rBody.angularVelocity = Vector3.zero; //Compulsory changes
            this.rBody.velocity = Vector3.zero; //Compulsory changes
            this.transform.position = new Vector3(40f, 0f, 40f); //To reset the position of the car
            this.transform.eulerAngles = new Vector3(0, 0, 0); //To reset any angular movement of the car due to momentum from previous iteration

            //Resetting the target (passenger)
            Vector3 pos = new Vector3(UnityEngine.Random.Range(100.0f, 180.0f), 0, UnityEngine.Random.Range(100.0f, 180.0f));
            pos.y = Terrain.activeTerrain.SampleHeight(pos);
            Target.position = pos;
            rew = -.003f;
        }

        public override void AgentAction(float[] vectorAction)
        {
            float h = vectorAction[0]; //Horizontal keys, representing 'acceleration' and 'backward-force'
            float v = vectorAction[1]; //Vertical movement keys, representing 'left-turn' or 'right-turn'

            /*
             * We add a small negative reward in order to compel the agent to take a step and not remain
             * idle. Refer the Agent Documentation.
             */
            rew = -.003f;
            //AddReward(rew);
            SetReward(rew);
            Debug.Log(rew);
            m_Car.Move(h, v, v, 0f); 
            /*
             * All values are passed to the concerned function in the CrossPlatformInput class of the
             * Unity Standard Assets package. (Check namespaces used)
             */

            /*
             * Allocate rewards to an Agent by calling the AddReward() method in the AgentAction() function.
             * The reward assigned between each decision should be in the range [-1,1].
             * Values outside this range can lead to unstable training.
             * The reward value is reset to zero when the agent receives a new decision.
             * If there are multiple calls to AddReward() for a single agent decision,
             * the rewards will be summed together to evaluate how good the previous decision was.
             * There is a method called SetReward() that will override all previous rewards given to an
             * agent since the previous decision.
            */
            //Getting distance from Destination
            float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);
            //Defining Reward and resetting the agent when it reaches destination.
            if (distanceToTarget < 15.0f)
            {
                //AddReward(1f);
                SetReward(1f);
                Done();

                /*Add postion to check the velocity if the car
                if (this.rBody.velocity < { 1, 1, 1})
                {
                    SetReward(1f);
                    Done();
                }
                else
                {
                    SetReward(-.3f);
                    Done();
                }
                */
            }
            //Defining Reward and resetting the agent if it falls off the terrain.
            if (this.transform.position.y < 0)
            {
                //AddReward(-3f);  Randomly setting the reward value. **Needs clarification 
                SetReward(-.6f);
                Done();
            }
            
            //base.AgentReset();
        }
        /*
         * When you use vector observations for an Agent, implement the Agent.CollectObservations()
         * method to create the feature vector. When you use Visual Observations, you only need to
         * identify which Unity Camera objects or RenderTextures will provide images and the base Agent
         * class handles the rest. You do not need to implement the CollectObservations() method when 
         * your Agent uses visual observations (unless it also uses vector observations).
        public override void CollectObservations()
        {
            
            
            //base.CollectObservations(sensor);
        }
        */


        /*
         *We still do not have any object to collide to.
        public void OnCollisionEnter(Collision collision)
        {
            
            if (collision.transform.CompareTag("fence"))
            {
                SetReward(-0.4f);
                Done();
            }
            
        }
        */

        //For testing the environment, as given in the documentation
        public override float[] Heuristic()
        {
            var action = new float[2];
            action[0] = CrossPlatformInputManager.GetAxis("Horizontal");
            action[1] = CrossPlatformInputManager.GetAxis("Vertical");
            
            return action;
        }
    }
}
