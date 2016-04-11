using UnityEngine;
using System.Collections;


static class Constants
{
    public const int maxLength = 20;
}


public class MapGenerationScript : MonoBehaviour {

    char[,,] map = new char[Constants.maxLength, Constants.maxLength, 2];
    /*
    * position does x, y, direction 0 equals forward, 1 equals right, 2 equals back, 3 left
    * 0 = x
    * 1 = y
    * 2 = direction
    * 3 = count flag for first loop
    * 4 = level
    */
    int[] position = { 9, 19, 0, 0, 0 };
    int[,] queue = new int[15, 3];
    char lastMove;

    int i;
    int j;
    int k;
    int sentinel = 0;

    public int getDirection()
    {
        return (position[2]);
    }

    public char[,] initialize()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        int i;
        int j;
        int k;
        char[,] move = new char[15,2];

	    for(k = 0; k< 2; k++){
		    for(j = 0; j<Constants.maxLength; j++){
			    for(i = 0; i< Constants.maxLength; i++){
				    map[i,j,k] = '*';
			    }
		    }
	    }
        
        move[0,0] = 'S';
        move[0, 1] = position[2].ToString()[0];
        straight(map, position, queue, sentinel);
        sentinel = 1;
        while (sentinel< 14){
		    move[sentinel,0] = mapStep();
            lastMove = move[sentinel, 0];
            move[sentinel,1] = position[2].ToString()[0];
        }
        return (move);
    }

    public char mapStep()
    {
        int nextVal = 0;
        char step = '*';
        while (step == '*')
        {
            if ((leftPossible(map, position)==1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position)==1))
            {
                nextVal = Random.Range(0,6);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 1;
                        break;
                    case 5:
                        nextVal = 2;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 5);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 1;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 5);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(1, 6);
                switch (nextVal)
                {
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 1;
                        break;
                    case 5:
                        nextVal = 6;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 3);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 1;
                        break;
                    case 2:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 2);
                switch (nextVal)
                {
                    case 0:
                        nextVal = 1;
                        break;
                    case 1:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 4);
                switch (nextVal)
                {
                    case 0:
                        nextVal = 0;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 4);
                switch (nextVal)
                {
                    case 0:
                        nextVal = 0;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 1;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 2);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 2;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 2);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 1;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 4);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = -1;
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = 0;
            }
            else if (!(leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = 1;
            }
            else if (!(leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = 2;
            }
            switch (nextVal)
            {
                case -1:
                    if (leftPossible(map, position)==1)
                    {
                        if (leftLeft(map, position, queue, sentinel) >= 15)
                        {
                            left(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'L';
                        }
                    }
                    break;
                case 0:
                    if (straightPossible(map, position)==1)
                    {
                        if (straightLeft(map, position, queue, sentinel) >= 15)
                        {
                            straight(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'S';
                        }
                    }
                    break;
                case 1:
                    if (levelChangePossible(map, position) == 1)
                    {
                        if (levelChangeLeft(map, position, queue, sentinel) >= 15)
                        {
                            levelChange(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'C';
                        }
                    }
                    break;
                case 2:
                    if (rightPossible(map, position)==1)
                    {
                        if (rightLeft(map, position, queue, sentinel) >= 15)
                        {
                            right(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'R';
                        }
                    }
                    break;
            }
        }
        return (step);
    }

    void straight(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                map[queue[position[3] + 1,0],queue[position[3] + 1,1], queue[position[3] + 1, 2]] = '*';
            }
            else if (position[3] > 13)
            {
                map[queue[0,0],queue[0,1], queue[0, 2]] = '*';
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                break;
            case 1:
                position[0] = position[0] + 1;
                break;
            case 2:
                position[1] = position[1] + 1;
                break;
            case 3:
                position[0] = position[0] - 1;
                break;
        }
        queue[position[3],0] = position[0];
        queue[position[3],1] = position[1];
        queue[position[3],2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0],position[1],position[4]] = 'S';
    }

    void levelChange(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]] = '*';
            }
            else if (position[3] > 13)
            {
                map[queue[0, 0], queue[0, 1], queue[0, 2]] = '*';
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                break;
            case 1:
                position[0] = position[0] + 1;
                break;
            case 2:
                position[1] = position[1] + 1;
                break;
            case 3:
                position[0] = position[0] - 1;
                break;
        }
        switch (position[4])
        {
            case 0:
                position[4] = 1;
                break;
            case 1:
                position[4] = 0;
                break;
        }
        queue[position[3], 0] = position[0];
        queue[position[3], 1] = position[1];
        queue[position[3], 2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0], position[1], position[4]] = 'C';
    }

    void left(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                map[queue[position[3] + 1,0],queue[position[3] + 1,1], queue[position[3] + 1, 2]] = '*';
            }
            else if (position[3] > 13)
            {
                map[queue[0,0],queue[0,1],queue[0,2]] = '*';
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                position[2] = 3;
                break;
            case 1:
                position[0] = position[0] + 1;
                position[2] = 0;
                break;
            case 2:
                position[1] = position[1] + 1;
                position[2] = 1;
                break;
            case 3:
                position[0] = position[0] - 1;
                position[2] = 2;
                break;
        }
        queue[position[3],0] = position[0];
        queue[position[3],1] = position[1];
        queue[position[3], 2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0],position[1],position[4]] = 'L';
    }

    void right(char[,,] map, int[] position, int[,] queue, int sentinel)
        {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                map[queue[position[3] + 1,0],queue[position[3] + 1,1], queue[position[3] + 1, 2]] = '*';
            }
            else if (position[3] > 13)
            {
                map[queue[0,0],queue[0,1], queue[0, 2]] = '*';
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                position[2] = 1;
                break;
            case 1:
                position[0] = position[0] + 1;
                position[2] = 2;
                break;
            case 2:
                position[1] = position[1] + 1;
                position[2] = 3;
                break;
            case 3:
                position[0] = position[0] - 1;
                position[2] = 0;
                break;
        }
        queue[position[3],0] = position[0];
        queue[position[3],1] = position[1];
        queue[position[3], 2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0],position[1],position[4]] = 'R';
    }

    int leftPossible(char[,,] map, int[] position)
    {
        int flag = 0;
        switch (position[2])
        {
            case 0:
                if (position[0] != 0)
                {
                    if ((map[position[0],position[1] - 1, position[4]] == '*') && (map[position[0] - 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 1:
                if (position[1] != 0)
                {
                    if ((map[position[0] + 1,position[1], position[4]] == '*') && (map[position[0] + 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 2:
                if (position[0] != Constants.maxLength - 1)
                {
                    if ((map[position[0],position[1] + 1, position[4]] == '*') && (map[position[0] + 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 3:
                if (position[1] != Constants.maxLength - 1)
                {
                    if ((map[position[0] - 1,position[1], position[4]] == '*') && (map[position[0] - 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
        }
        return flag;
    }

    int straightPossible(char[,,] map, int[] position)
    {
        int flag = 0;
        switch (position[2])
        {
            case 0:
                if (position[1] != 1)
                {
                    if ((map[position[0],position[1] - 1, position[4]] == '*') && (map[position[0],position[1] - 2, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 1:
                if (position[0] != Constants.maxLength - 2)
                {
                    if ((map[position[0] + 1,position[1], position[4]] == '*') && (map[position[0] + 2,position[1], position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 2:
                if (position[1] != Constants.maxLength - 2)
                {
                    if ((map[position[0],position[1] + 1, position[4]] == '*') && (map[position[0],position[1] + 2, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 3:
                if (position[0] != 1)
                {
                    if ((map[position[0] - 1,position[1], position[4]] == '*') && (map[position[0] - 2,position[1], position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
        }
        return flag;
    }

    int levelChangePossible(char[,,] map, int[] position)
    {
        int flag = 0;
        int otherLevel = 0;
        if (!(lastMove.Equals('C'))){
            switch (position[4])
            {
                case 0:
                    otherLevel = 1;
                    break;
                case 1:
                    otherLevel = 0;
                    break;
            }
            switch (position[2])
            {
                case 0:
                    if (position[1] != 1)
                    {
                        if ((map[position[0], position[1] - 1, position[4]] == '*') && (map[position[0], position[1] - 1, otherLevel] == '*') && (map[position[0], position[1] - 2, otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
                case 1:
                    if (position[0] != Constants.maxLength - 2)
                    {
                        if ((map[position[0] + 1, position[1], position[4]] == '*') && (map[position[0] + 1, position[1], otherLevel] == '*') && (map[position[0] + 2, position[1], otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
                case 2:
                    if (position[1] != Constants.maxLength - 2)
                    {
                        if ((map[position[0], position[1] + 1, position[4]] == '*') && (map[position[0], position[1] + 1, otherLevel] == '*') && (map[position[0], position[1] + 2, otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
                case 3:
                    if (position[0] != 1)
                    {
                        if ((map[position[0] - 1, position[1], position[4]] == '*') && (map[position[0] - 1, position[1], otherLevel] == '*') && (map[position[0] - 2, position[1], otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
            }
        }
        return flag;
    }

    int rightPossible(char[,,] map, int[] position)
        {
        int flag = 0;
        switch (position[2])
        {
            case 0:
                if (position[0] != Constants.maxLength - 1)
                {
                    if ((map[position[0],position[1] - 1, position[4]] == '*') && (map[position[0] + 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 1:
                if (position[1] != Constants.maxLength - 1)
                {
                    if ((map[position[0] + 1,position[1], position[4]] == '*') && (map[position[0] + 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 2:
                if (position[0] != 0)
                {
                    if ((map[position[0],position[1] + 1, position[4]] == '*') && (map[position[0] - 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 3:
                if (position[1] != 0)
                {
                    if ((map[position[0] - 1,position[1], position[4]] == '*') && (map[position[0] - 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
        }
        return flag;
    }

    int leftLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
	    int[] copyPosition = new int[5];
        int[,] copyQueue = new int[15,3];
	
	    int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        left(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
	    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
	    return step;
    }

    int straightLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
	    int[] copyPosition = new int[5];
        int[,] copyQueue = new int[15,3];
	
	    int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        straight(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
	    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
	    return step;
    }

    int levelChangeLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength, Constants.maxLength, 2];
        int[] copyPosition = new int[5];
        int[,] copyQueue = new int[15, 3];

        int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        levelChange(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
        step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
        return step;
    }

    int rightLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
	    int[] copyPosition = new int[5];
        int[,] copyQueue = new int[15,3];
	
	    int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        right(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
	    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
	    return step;
    }

    int leftHandMethod(char[,,] map, int[] position, int[,] queue, int sentinel, int step)
    {
        if (sentinel < 15)
        {
            char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
		    int[] copyPosition = new int[5];
            int[,] copyQueue = new int[15,2];
		    int returnedStep;
            
            copyPosition = positionCopier(position);
            copyMap = mapCopier(map);
            copyQueue = queueCopier(queue);
            if (leftPossible(copyMap, copyPosition) == 1){
                left(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
			    if(step< 15){
				    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
			    }
			    else{
				    return(step);
			    }
			    if(returnedStep< 15){
				    step = step - 1;
                    copyPosition = positionCopier(position);
                    copyMap = mapCopier(map);
                    copyQueue = queueCopier(queue);
                    if (straightPossible(copyMap, copyPosition) == 1){
                        straight(copyMap, copyPosition, copyQueue, sentinel);
                        step = step + 1;
					    if(step< 15){
						    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
					    }
					    else{
						    return(step);
                        }
                        if (returnedStep < 15)
                        {
                            step = step - 1;
                            copyPosition = positionCopier(position);
                            copyMap = mapCopier(map);
                            copyQueue = queueCopier(queue);
                            if (levelChangePossible(copyMap, copyPosition) == 1)
                            {
                                levelChange(copyMap, copyPosition, copyQueue, sentinel);
                                step = step + 1;
                                if (step < 15)
                                {
                                    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                }
                                else {
                                    return (step);
                                }
                                if (returnedStep < 15)
                                {
                                    step = step - 1;
                                    copyPosition = positionCopier(position);
                                    copyMap = mapCopier(map);
                                    copyQueue = queueCopier(queue);
                                    if (rightPossible(copyMap, copyPosition) == 1)
                                    {
                                        right(copyMap, copyPosition, copyQueue, sentinel);
                                        step = step + 1;
                                        if (step < 15)
                                        {
                                            returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                            if (returnedStep == 15)
                                            {
                                                return (returnedStep);
                                            }
                                        }
                                        else {
                                            return (step);
                                        }
                                    }
                                }
                            }
                        }
					    else{
						    return(returnedStep);
					    }
				    }
			    }
			    else{
				    return(returnedStep);
			    }
		    }
		    else if(straightPossible(copyMap, copyPosition) == 1){
                straight(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
			    if(step< 15){
				    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
			    }
			    else{
				    return(step);
			    }
			    if(returnedStep< 15){
				    step = step - 1;
                    copyPosition = positionCopier(position);
                    copyMap = mapCopier(map);
                    copyQueue = queueCopier(queue);
				    if(rightPossible(copyMap, copyPosition) == 1){
                        right(copyMap, copyPosition, copyQueue, sentinel);
                        step = step + 1;
					    if(step< 15){
						    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
					    }
					    else{
						    return(step);
					    }
                        if (returnedStep< 15){
				            step = step - 1;
                            copyPosition = positionCopier(position);
                            copyMap = mapCopier(map);
                            copyQueue = queueCopier(queue);
				            if(levelChangePossible(copyMap, copyPosition) == 1){
                                levelChange(copyMap, copyPosition, copyQueue, sentinel);
                                step = step + 1;
					            if(step< 15){
						            step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
					            }
					            else{
						            return(step);
					            }
					            if(returnedStep < 15)
                                {
                                    step = step - 1;
                                    copyPosition = positionCopier(position);
                                    copyMap = mapCopier(map);
                                    copyQueue = queueCopier(queue);
                                    if (rightPossible(copyMap, copyPosition) == 1)
                                    {
                                        right(copyMap, copyPosition, copyQueue, sentinel);
                                        step = step + 1;
                                        if (step < 15)
                                        {
                                            step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                        }
                                        else {
                                            return (step);
                                        }
                                        if (returnedStep < 15)
                                        {
                                            return (returnedStep);
                                        }
                                    }
                                }
				            }
			            }
			            else{
				            return(returnedStep);
			            }
				    }
			    }
			    else{
				    return(returnedStep);
			    }
            }
            else if (levelChangePossible(copyMap, copyPosition) == 1)
            {
                levelChange(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
                if (step < 15)
                {
                    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                }
                else {
                    return (step);
                }
                if (returnedStep < 15)
                {
                    step = step - 1;
                    copyPosition = positionCopier(position);
                    copyMap = mapCopier(map);
                    copyQueue = queueCopier(queue);
                    if (rightPossible(copyMap, copyPosition) == 1)
                    {
                        right(copyMap, copyPosition, copyQueue, sentinel);
                        step = step + 1;
                        if (step < 15)
                        {
                            step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                        }
                        else {
                            return (step);
                        }
                        if (returnedStep < 15)
                        {
                            step = step - 1;
                            copyPosition = positionCopier(position);
                            copyMap = mapCopier(map);
                            copyQueue = queueCopier(queue);
                            if (rightPossible(copyMap, copyPosition) == 1)
                            {
                                right(copyMap, copyPosition, copyQueue, sentinel);
                                step = step + 1;
                                if (step < 15)
                                {
                                    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                }
                                else {
                                    return (step);
                                }
                                if (returnedStep == 15)
                                {
                                    return (returnedStep);
                                }
                            }
                        }
                        else {
                            return (returnedStep);
                        }
                    }
                }
                else {
                    return (returnedStep);
                }
            }
            else if(rightPossible(copyMap, copyPosition) == 1){
                right(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
			    if(step< 15){
				    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
			    }
			    else{
				    return(step);
			    }
			    if(returnedStep == 15){
				    return(returnedStep);
			    }
		    }
	    }
	    return step;
    }

    char[,,] mapCopier(char[,,] tempMap){
        char[,,] copyMap = new char[Constants.maxLength, Constants.maxLength, 2];
        
        int i;
        int j;
        int k;
        for (k = 0; k < 2; k++)
        {
            for (j = 0; j < Constants.maxLength; j++)
            {
                for (i = 0; i < Constants.maxLength; i++)
                {
                    copyMap[i, j, k] = tempMap[i, j, k];
                }
            }
        }
        return copyMap;
    }

    int[] positionCopier(int[] tempPosition)
    {
        int[] copyPosition = new int[5];

        int i;
        int j;
        for (i = 0; i < 5; i++)
        {
            copyPosition[i] = tempPosition[i];
        }
        return copyPosition;
    }

    int[,] queueCopier(int[,] tempQueue)
    {
        int[,] copyQueue = new int[15, 3];
        int i;
        int j;

        for (j = 0; j < 3; j++)
        {
            for (i = 0; i < 15; i++)
            {
                copyQueue[i, j] = tempQueue[i, j];
            }
        }
        return copyQueue;
    }
}