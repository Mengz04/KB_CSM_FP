from collections import defaultdict
import matplotlib.pyplot as plt
import math
import numpy as np

class Transform:
    def __init__(self, x, y):
        self.x = x
        self.y = y
    
    def change_x(self, x):
        self.x = x

    def change_y(self, y):
        self.y = y

    def normalized(self):
        magnitude = math.sqrt(math.pow(self.x, 2) + math.pow(self.y, 2))
        self.x = self.x / magnitude
        self.y = self.y / magnitude
    
class Collider2D:
    def __init__(self, x, y):
        self.x = x
        self.y = y
    
    def change_x(self, x):
        self.x = x

    def change_y(self, y):
        self.y = y

class Vector2D:
    def __init__(self, x, y):
        self.x = x
        self.y = y
    
    def change_x(self, x):
        self.x = x

    def change_y(self, y):
        self.y = y

    def normalized(self):
        magnitude = math.sqrt(math.pow(self.x, 2) + math.pow(self.y, 2))
        self.x = self.x / magnitude
        self.y = self.y / magnitude
        return self

    def get_x(self):
        return self.x

    def get_y(self):
        return self.y

class AiData:
    def __init__(self, listtarget, listobstacles, currtarget):
        self.listtarget = listtarget
        self.listobstacles = listobstacles
        self.currtarget = currtarget

class Directions:
    def __init__(self):
        self.listdirections = list()
        self.listdirections.append(Vector2D(0, 1).normalized())
        self.listdirections.append(Vector2D(1, 1).normalized())
        self.listdirections.append(Vector2D(1, 0).normalized())
        self.listdirections.append(Vector2D(1, -1).normalized())
        self.listdirections.append(Vector2D(0, -1).normalized())
        self.listdirections.append(Vector2D(-1, -1).normalized())
        self.listdirections.append(Vector2D(-1, 0).normalized())
        self.listdirections.append(Vector2D(-1, 1).normalized())

def Clamp01(interest, danger):
    if (interest - danger < 0):
        return 0
    elif (interest - danger > 1):
        return 1
    else:
        return interest - danger
    
def dotprod(x1, x2, y1, y2):
    return x1*x2 + y1*y2

def distance_vector(x1, x2, y1, y2):
    return math.sqrt(math.pow((x2-x1), 2) + math.pow((y2-y1), 2))

# Obstacle Avoidance Variables
agentColliderSize = 1.7
radius = 2

# Zombie, Targets, and Obstacle Variables
zombie = Transform(5, 5)
zombiePosX = [5]
zombiePosY = [5]
listtarget = [Transform(10, 10)]
currtarget = listtarget[0]
listobstacle = [Collider2D(8, 7)]
aidata = AiData(listtarget, listobstacle, currtarget)

# Seek Variables
reachedLastTarget = False
allFatherStat = False
playerPos = Transform(10, 10)
targetPositionCached = Vector2D(0, 0)
targetReachedThreshold = 0.5

while(True):
    # Obstacle Avoidance
    danger = [0, 0, 0, 0, 0, 0, 0, 0]
    interest = [0, 0, 0, 0, 0, 0, 0, 0]

    directions_obs = Directions()
    for obstacleCollider in aidata.listobstacles:
        directionToObstacle = Vector2D(obstacleCollider.x - zombie.x, obstacleCollider.y - zombie.y) 
        
        distanceToObstacle = math.sqrt(math.pow(directionToObstacle.x, 2) + math.pow(directionToObstacle.y, 2))

        weight = 1 if distanceToObstacle <= agentColliderSize else (radius - distanceToObstacle) / radius

        directionToObstacleNormalized = Vector2D(directionToObstacle.x / distanceToObstacle, directionToObstacle.y / distanceToObstacle)
        
        idx = 0
        for direction in directions_obs.listdirections:
            vectordir = Vector2D(0, 0)
            vectordir.change_x(direction.x)
            vectordir.change_y(direction.y)
            result_obs = dotprod(directionToObstacleNormalized.x, vectordir.x, directionToObstacleNormalized.y, vectordir.y)
            
            valueToPutIn = result_obs * weight

            if (valueToPutIn > danger[idx]):
                danger[idx] = valueToPutIn
            idx += 1

    # Seek Behaviour
    aidata.currtarget = aidata.listtarget[0]
                
    if (aidata.currtarget != None and aidata.listtarget != None and (True if aidata.currtarget in aidata.listtarget else False)):
        targetPositionCached = aidata.currtarget

    if (distance_vector(zombie.x, targetPositionCached.x, zombie.y, targetPositionCached.y) < targetReachedThreshold):
        reachedLastTarget = True
        aidata.currtarget = None

    directionToTarget = Vector2D(targetPositionCached.x - zombie.x, targetPositionCached.y - zombie.y)
    directions_seek = Directions()
    idx = 0
    for direction in directions_seek.listdirections:
        directionToTargetNormalized = directionToTarget.normalized()
        result_seek = dotprod(directionToTargetNormalized.x, direction.x, directionToTargetNormalized.y, direction.y)

        if (result_seek > 0):
            if (result_seek > interest[idx]):
                interest[idx] = result_seek
        idx += 1

    # Back to ContextSolver

    for i in range(8):
        interest[i] = Clamp01(interest[i], danger[i])

    outputDirection = Vector2D(0, 0)

    directions_context = Directions()
    idx = 0
    for direction in directions_context.listdirections:
        outputDirection.change_x(outputDirection.x + direction.x * interest[idx])
        outputDirection.change_y(outputDirection.y + direction.y * interest[idx])
        
        idx += 1
    if(outputDirection.y == 0 and outputDirection.x ==0):
        print("mark")
        break

    outputDirection.normalized()

    zombie.change_x(zombie.x+outputDirection.x)
    zombiePosX.append(zombie.x)
    zombie.change_y(zombie.y+outputDirection.y)
    zombiePosY.append(zombie.y)
    if(distance_vector(zombie.x, playerPos.x, zombie.y, playerPos.y) < targetReachedThreshold):
        break
    print("outputDIr: (", outputDirection.x, ", ", outputDirection.y,"); ", "zombiePos: (", zombie.x, ", ", zombie.y, ")")

xpoints = np.array(zombiePosX)
ypoints = np.array(zombiePosY)
plt.plot(xpoints, ypoints)
plt.scatter([8], [7], color="red")
plt.show()