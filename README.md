# RL-Unity-Self-Driving-Car
A Reinforcement Learning implementation to train an agent, a car to drive itself in an uneven terrain to reach the destination object, a man radomly spawned in the region. This repository contains some of the essential files and their description. Since uploading all the necessary files would be impossible, we have tried to provide as much context as possible.

## Reinforcement Learning

The concept behind reinforcement learning is quite simple to understand. The whole task of learning is modeled after the way humans learn how to calibrate good and bad behaviour in life. As in life, a child may touch a flame, realize that it burns him or her. This would discourage the child from putting his hand into the flame again. On the other hand, doing house chores may earn him a few treats from his parents. This positively reinforces the act of doing house chores as the child knows he would get rewards for it. Fig 1.1, gives a general idea about reinforcement learning in general. The agent is an independent entity that represents a simple human being that will learn to perform a task. The environment is a model of the world, where the agent takes a particular action. Based on the action taken, the state (, or the current condition) of the environment changes into a new state and it gives out some rewards (, or feedback) to the agent that suggests whether the action it took resulted in a condition that is favourable to the agent or not.

<img src="https://github.com/crazylazylife/RL-Unity-Self-Driving-Car/blob/master/rl.png" alt="Reinforcement Learning" height="300px" width="400px">

## Environment Development in Unity

<img src="https://github.com/crazylazylife/RL-Unity-Self-Driving-Car/blob/master/pic2.png" alt="Environment" height="300px" width="400px">

The environment was developed in Unity. Unity provides a flexible interface with a large set of assets and libraries that make the task easier. First we got hold of the [ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents) of Unity. The github repository has one of the best documentation to get started with. We used **ML-Agents version 0.15**, however the current version id **release 1**.
**Note: Please make sure to check the dependencies of the libraries and the tools. Various versions had various support requirement which are sometime confusing.**
We used the Standard Unity Assets pack from its extensive Assets Store and built the terrain, the car. The human figure was imported from a separate asset. A little tinkering with the settings got us easily to interact with the environment. The next part was defining the script to control the **Reward Function.** The details are updated in the CarAgent.cs script. This is a part of all the scripts associated with the car asset. Without changing much of the controller function, we hopped straight into defining out function. The script is docuented enough to understand the functions and the rewards.

## Reward Function
The reward function for the agent includes three main components:
1.	A reward of -0.04f for each step taken.
2.	It receives a reward of -.6f if the car falls off the platform.
3.	On reaching the target, the final reward of 1.0f is received.
A -0.04f reward at each step is to force the agent to take a step at each instance instead of staying ideal. 

## Training

We first trained the agent using PPO available with the ML-Agents Toolkit. In the next part we defined an A2C model that trains the agent by interacting with the application using the [Gym](https://gym.openai.com/) environment. Gym from openAI is a great resource for people actively working in the field of Reinforcement Learning. Most of the results are detailed in our paper that is uploaded in the repository. Some interesting training scenarios are shown here:

### Trained model in action:
The agent is able to reach the destination

