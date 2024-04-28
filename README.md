# AStarDemo
This repository contains a Unity project implementing the A* pathfinding algorithm for finding optimal paths in a grid-based environment. The project also allows users to place structures within the environment, provides camera controls for navigation, and enables character movement along the calculated path.

## Overview
The A* Pathfinding algorithm is a fundamental component of many modern video games and simulation systems. It provides a robust and efficient method for determining the optimal path between two points in a graph or grid-based environment. This README serves as a guide to understanding the implementation and application of the A* algorithm within this project.

![1](https://github.com/irfanSOLAK/AStarDemo/assets/108720676/31d0ec23-37e2-4828-9480-92ee797b2beb)

## Features
• __A* Pathfinding Implementation:__ Utilizes the A* algorithm to find the shortest paths between points in a grid-based environment.

• **Grid System:** Manages the creation and organization of nodes within the grid, enabling efficient pathfinding calculations.

• **Structure Placement:** Allows users to place structures within the grid, dynamically altering the navigation environment.

• **Camera Controls:** Provides free view and upper view options for navigating the scene.

• **Path Visualization:** Visualizes the calculated path on the grid for better understanding of the pathfinding process.

• **Character Movement:** Moves the character along the calculated path, enabling dynamic movement within the environment.

• **Teleportation:** Allows users to teleport the character to a clicked position on the ground.

### Project Structure
*GridSystem.cs:* Manages the grid-based representation of the game environment and provides functions for node configuration, neighbor identification, and visualization.

*PathFinding.cs:* Implements the A* pathfinding algorithm to compute the shortest path between two points on the grid.

*CharacterMovement.cs:* Controls the movement of the character by following the computed path.

*Navigator.cs:* Handles user interaction by allowing users to set the start and end points for pathfinding.

## Algorithm Workflow
__1. Identification of Start and Target Nodes:__

• The algorithm begins by identifying the start and target nodes in the graph or grid. These nodes represent the starting position and the destination to which the algorithm will find the shortest path.

__2.Utilization of Open and Closed Sets:__

• A* maintains two sets of nodes: the open set and the closed set. The open set contains nodes that have been discovered but not yet evaluated, while the closed set contains nodes that have already been evaluated.

__3.Calculation of F and G Costs__

• For each node, the algorithm calculates two costs:

&emsp; • G Cost: The actual cost of reaching a node from the starting node.

&emsp; •  H Cost: The heuristic estimated cost of reaching the target node from the current node.

•  The F cost of a node is the sum of its G cost and H cost: F = G + H.

__4.Evaluation of Nodes:__

•  A* evaluates nodes by selecting the one with the lowest F cost from the open set for expansion.

__5.Evaluation of Neighbor Nodes:__

•  For each selected node, A* examines its neighboring nodes and updates their G and H costs if a shorter path is found.

__6.Iteration Until Target Reached:__

•  The algorithm iterates until it reaches the target node or determines that there is no valid path. Once the target node is reached, the algorithm reconstructs the path from the start node to the target node using the parent pointers stored in each node.

## Controls
•  Run the scene containing the pathfinding script (PathFinding.cs).

•  Use the "Place Structure" button to activate structure placement mode.

•  Click and hold the left mouse button to move the structure around the grid, snapping to valid positions.

![4](https://github.com/irfanSOLAK/AStarDemo/assets/108720676/e62d44c3-636d-4e27-b3eb-33844a2db9bb)

•  While in placement mode, hold the right mouse button to rotate the structure.

•  Release the left mouse button to place the structure on the grid.

•  Use the "Clear All Structures" button to remove all placed structures.

![2](https://github.com/irfanSOLAK/AStarDemo/assets/108720676/234a759c-2c99-48bc-a70e-bd13cff391d2)

•  Use the "Show Demo Structures" button to display pre-defined structures in the environment.

•  Use the "Free View" and "Upper View" buttons to control the camera for scene navigation.

![5](https://github.com/irfanSOLAK/AStarDemo/assets/108720676/2c76a45d-e2dd-4bfa-9e92-71b0c264f82d)

•  Use the "Quit" button to exit the application.

•  Click on the ground to initiate pathfinding and move the character along the calculated path.


## How to Use

Clone the repository to your local machine.

Open the Unity project in the Unity Editor.

Explore the provided scripts and scene to understand the implementation.

Run the project in the Unity Editor to observe the A* pathfinding algorithm in action.

Experiment with different obstacles and grid configurations to test the algorithm's robustness.

## Untiy version
2020.3.35f1
