#include <iostream>
#include <random>

using namespace std;

/* MAX and MIN is to specify the types of items that can be present on the conveyor belt.
   We can have either 'A' or 'B' type component or Empty component on the belt.
   The 'P' type is the finished product that cannnot be present in the conveyor belt by default.
   The 'P' type finished product can only be their in the belt when put in by a worker.*/
#define MAX 3 
#define MIN 1

struct worker
{
   bool hasComponentA; //True when worker has component 'A'. A worker can have only one component of type 'A' at a time.
   bool hasComponentB; //True when worker has component 'B'. A worker can have only one component of type 'B' at a time.
   bool hasProduct;    // True when worker has finished product.
   int timeStamp;      // Stores the simulation step when worker had both component A and component B and starts assembling to get finished product.
   int itemCount;      // Stores the total number of items the worker currently has. A worker can have atmost 2 items which can be from 'A', 'B' or 'P'.

   worker()
   {
      hasComponentA = false;
      hasComponentB = false;
      hasProduct = false;
      timeStamp = 0;
      itemCount = 0;
   }
};

//This array is used to maintain a mapping between item number to item type which can be 'A' or 'B' or ' ';
char itemNumberToTypeMap[MAX+1];

//This method is used to update contents of slot and worker details when worked places a finished product in the conveyor belt.
void UpdateSlotContentAndWorkerDetailsOnProductPlaced(char *contentOfBelt, worker *workers, int slot, int &productCount)
{
   contentOfBelt[slot] = 'P';
   workers[slot].hasProduct = false;
   workers[slot].timeStamp = 0;
   workers[slot].itemCount--;
   productCount++;
}

//This method is used to update contents of slot and worker details when he picks an item from the conveyor belt.
void UpdateSlotContentAndWorkerDetailsOnItemPicked(char *contentOfBelt, worker *workers, int slot, char itemType)
{
   contentOfBelt[slot] = ' ';
   workers[slot].itemCount++;
   if(itemType == itemNumberToTypeMap[1])
   workers[slot].hasComponentA = true;
   else workers[slot].hasComponentB = true;
}

//This method is used to update worker details when he starts to assemble a product after procuring both component types.
void UpdateWorkerDetailsOnStartAssembling(worker *workers, int slot, int step)
{
   workers[slot].itemCount--;
   workers[slot].hasComponentA = workers[slot].hasComponentB = false;
   workers[slot].hasProduct = true;
   workers[slot].timeStamp = step;
}

//Utility method to display contents of the conveyor belt.
void DisplayConveyorBelt(char *contentOfBelt, int conveyorBeltLength)
{
   for(int slots = 0; slots < conveyorBeltLength; slots++)
   cout << contentOfBelt[slots] << " ";
   cout << endl;
}

int main()
{
   // The number of times the simulation will be carried out.
   int simulationSteps;
   cout << "Enter Number of Steps to Simulate" << endl;
   cin >> simulationSteps;

   // The number of pairs of workers.
   int workerPairsCount;
   cout << "Enter the number of pairs of workers" << endl;
   cin >> workerPairsCount;

   /*Assumption is that the conveyor belt length and number of pairs of workers should be the same. 
     Hence the each slot in the conveyor belt is of width 1.*/   
   int conveyorBeltLength;
   cout << "Enter the length of the Conveyor Belt" << endl;
   cin >> conveyorBeltLength;

   /*Initialise the worker pairs.
     workersUp and workersDown correspond to workers on either side of the conveyor belt.
     At any given slot on the conveyor belt, workers from group workersUp will always be given preference to take action the current item in that slot.*/
   worker *workersUp = new worker[workerPairsCount];
   worker *workersDown = new worker[workerPairsCount];
   
   //Initialize the mapping.
   itemNumberToTypeMap[1] = 'A';
   itemNumberToTypeMap[2] = 'B';
   itemNumberToTypeMap[3] = ' ';

   //Initialise the fixed width slots of the conveyor belt with all slots having empty content.
   char *contentOfBelt = new char[conveyorBeltLength];
   for(int i = 0; i<conveyorBeltLength; i++)
   contentOfBelt[i] = itemNumberToTypeMap[3];

   //Counter for Total Number of finished products.
   int productCount = 0;

   //Counter for Total Number of Component Type of 'A' not picked up by any worker and passes through the conveyor unutilized.
   int wastedTypeACount = 0;

   //Counter for Total Number of Component Type of 'B' not picked up by any worker and passes through the conveyor unutilized.
   int wastedTypeBCount = 0;

   for(int steps = 1; steps <= simulationSteps; steps++)
   {
      cout << "Simulation Step :" << steps << endl;

      /*Generate the new item entering the conveyor belt in the first slot.      
        This is guaranteed to always randomly generate a number between MAX and MIN inclusive.*/
      int randNum = (rand()%(MAX - MIN + 1)) + MIN;
      contentOfBelt[0] = itemNumberToTypeMap[randNum];

      cout << "Content of Conveyor Belt :" << endl;
      DisplayConveyorBelt(contentOfBelt, conveyorBeltLength);

      for(int slots = 0; slots < conveyorBeltLength; slots++)
      {
         if(contentOfBelt[slots] != 'P')
         {
            if(contentOfBelt[slots] == itemNumberToTypeMap[3])
            {
               if(workersUp[slots].hasProduct && workersUp[slots].timeStamp && ((steps - workersUp[slots].timeStamp) >= 4))
               {
                  UpdateSlotContentAndWorkerDetailsOnProductPlaced(contentOfBelt, workersUp, slots, productCount);
               }
               else if(workersDown[slots].hasProduct && workersDown[slots].timeStamp && ((steps - workersDown[slots].timeStamp) >= 4))
               {
                  UpdateSlotContentAndWorkerDetailsOnProductPlaced(contentOfBelt, workersDown, slots, productCount);                  
               }
            }
            else if(contentOfBelt[slots] == itemNumberToTypeMap[1])
            {
               if(!workersUp[slots].hasComponentA && workersUp[slots].itemCount != 2)
               {
                  UpdateSlotContentAndWorkerDetailsOnItemPicked(contentOfBelt, workersUp, slots, itemNumberToTypeMap[1]);
                  if(workersUp[slots].hasComponentB)
                  {
                     UpdateWorkerDetailsOnStartAssembling(workersUp, slots, steps);
                  }
               }
               else if(!workersDown[slots].hasComponentA && workersDown[slots].itemCount != 2)
               {
                  UpdateSlotContentAndWorkerDetailsOnItemPicked(contentOfBelt, workersDown, slots, itemNumberToTypeMap[1]);
                  if(workersDown[slots].hasComponentB)
                  {
                     UpdateWorkerDetailsOnStartAssembling(workersDown, slots, steps);
                  }
               }
            }
            else if(contentOfBelt[slots] == itemNumberToTypeMap[2])
            {
               if(!workersUp[slots].hasComponentB && workersUp[slots].itemCount != 2)
               {
                  UpdateSlotContentAndWorkerDetailsOnItemPicked(contentOfBelt, workersUp, slots, itemNumberToTypeMap[2]);
                  if(workersUp[slots].hasComponentA)
                  {
                     UpdateWorkerDetailsOnStartAssembling(workersUp, slots, steps);
                  }
               }
               else if(!workersDown[slots].hasComponentB && workersDown[slots].itemCount != 2)
               {
                  UpdateSlotContentAndWorkerDetailsOnItemPicked(contentOfBelt, workersDown, slots, itemNumberToTypeMap[2]);
                  if(workersDown[slots].hasComponentA)
                  {
                     UpdateWorkerDetailsOnStartAssembling(workersDown, slots, steps);
                  }
               }
            }
         }
      }
      cout << "Content of Conveyor Belt After Processing By Workers:" << endl;
      DisplayConveyorBelt(contentOfBelt, conveyorBeltLength);

      //Item at the last slot of the conveyor belt will not participate in the next simulation step and will go wasted.
      if(contentOfBelt[conveyorBeltLength - 1] == itemNumberToTypeMap[1])
      wastedTypeACount++;
      else if(contentOfBelt[conveyorBeltLength - 1] == itemNumberToTypeMap[2])
      wastedTypeBCount++;

      //Right shift contents of the conveyor belt by one position.
      for(int slots = conveyorBeltLength - 2; slots >= 0; slots--)
      {
         contentOfBelt[slots+1] = contentOfBelt[slots];
      }

      cout << endl;

   }

   cout << "Total Finished Products in this Simulation :" << productCount << endl;

   cout << "Total Component Type A not utilized : " << wastedTypeACount << endl;

   cout << "Total Component Type B not utilized : " << wastedTypeBCount << endl;

   cout << endl << endl;

   //Garbage collect dynamic memeory allocated.
   delete(contentOfBelt);
   delete(workersUp);
   delete(workersDown);

   system("pause");
}

/* If m is the number of simulation steps and n is the length of the conveyor belt then the worst case time complexity of the above
   algorithm is O(m*n). The space complexity is proportional to O(n); */
