using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = -1;
            while(width < 3 || width > 100){
                Console.Write("Enter map width: ");
                width = Int32.Parse(Console.ReadLine());
            }
            
            int height = -1;
            while(height < 3 || height > 100){
                Console.Write("Enter map height: ");
                height = Int32.Parse(Console.ReadLine());
            }

            int treasureCount = -1;
            while (treasureCount <= 0 || treasureCount >= width*height)
            {
                Console.Write("Enter treasure count: ");
                treasureCount = Int32.Parse(Console.ReadLine());
            }
            
            Map map = new Map(width, height, treasureCount);


            String characterSpecialSymbol = "";
            while (characterSpecialSymbol.Length != 1)
            {
                Console.Write("Enter your character special symbol: ");
                characterSpecialSymbol = Console.ReadLine();
            }
            
            Character mainCharacter = new Character(characterSpecialSymbol);

            map.addCharacter(mainCharacter);

            while (true)
            {
                map.print();
                Console.Write("Enter your move (left/right/up/down/suicide): ");
                String characterMovement = Console.ReadLine();
                if (characterMovement.Equals("suicide"))
                {
                    Environment.Exit(0);
                }
                if (characterMovement.Equals("left") || characterMovement.Equals("a"))
                {
                    map.moveCharacterLeft();
                }
                if (characterMovement.Equals("right") || characterMovement.Equals("d"))
                {
                    map.moveCharacterRight();
                }
                if (characterMovement.Equals("up") || characterMovement.Equals("w"))
                {
                    map.moveCharacterUp();
                }
                if (characterMovement.Equals("down") || characterMovement.Equals("s"))
                {
                    map.moveCharacterDown();
                }

                if (map.checkAllTreasuresFound())
                {
                    break;
                }

            }
            map.print();
            Console.WriteLine("Congrats, you won the game!");
            Console.ReadKey();            
        }
    }

    class Character
    {
        public String specialSymbol;

        public Character(String specialSymbol)
        {
            this.specialSymbol = " " + specialSymbol + " ";
        }
    }

    class Map
    {
        private int width;
        private int height;
        private int currentCharPosX;
        private int currentCharPosY;
        private int treasuresFound;
        private int treasuresOnMap;
        private Character characterOnMap;
        private List<List<String>> map = new List<List<String>>();
        private List<List<Boolean>> treasureMap = new List<List<Boolean>>();

        public Map(int width, int height, int treasureCount)
        {
            this.width = width;
            this.height = height;
            this.treasuresOnMap = treasureCount;
            generateMap(width, height);
            generateTreasures(treasureCount);
            printTreasureMap();
        }

        private void generateMap(int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                this.map.Add(new List<String>());
                for (int x = 0; x < width; x++)
                {
                    this.map[y].Add(" * ");
                }
            }
        }

        public void print()
        {
            Console.WriteLine();
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    if (x < this.width - 1)
                    {
                        Console.Write(this.map[y][x]);
                    }
                    else
                    {
                        Console.WriteLine(this.map[y][x]);
                    }
                }
            }
        }

        public void addCharacter(Character character)
        {
            this.characterOnMap = character;
            this.map[0][0] = character.specialSymbol;
            this.currentCharPosX = 0;
            this.currentCharPosY = 0;
            checkOnTreasure();
        }


        public void moveCharacterRight()
        {
            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = " : ";
            }
            this.currentCharPosX += 1;
            if (this.currentCharPosX >= this.width)
            {
                this.currentCharPosX -= this.width;
            }
            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
            }
            checkOnTreasure();
        }

        public void moveCharacterLeft()
        {
            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = " : ";
            }
            this.currentCharPosX -= 1;
            if (this.currentCharPosX < 0)
            {
                this.currentCharPosX += this.width;
            }
            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
            }
            checkOnTreasure();
        }

        public void moveCharacterUp()
        {
            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = " : ";
            }

            this.currentCharPosY -= 1;
            if (this.currentCharPosY < 0)
            {
                this.currentCharPosY += this.height;
            }
            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
            }
            checkOnTreasure();
        }

        public void moveCharacterDown()
        {
            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = " : ";
            }
            
            this.currentCharPosY += 1;
            if (this.currentCharPosY >= this.height)
            {
                this.currentCharPosY -= this.height;
            }

            if (!ifWasTreasureBefore())
            {
                this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
            }
            
            checkOnTreasure();
        }

        public void printDescription()
        {
            Console.WriteLine("Width: " + width + " Height: " + height);
        }

        private void generateTreasures(int neededTreasuresCount)
        {
            int treasuresGenerated = 0;
            Random rand = new Random();
            List<int> treasureIndexes = new List<int>();

            
            for (int y = 0; y < this.height; y++)
            {
                treasureMap.Add(new List<Boolean>());
                for (int x = 0; x < this.width; x++)
                {
                    treasureMap[y].Add(false);
                }
            }
            

            for (int i = 0; i < this.width * this.height; i++ )
            {
                treasureIndexes.Add(i);
            }

            while (treasuresGenerated != neededTreasuresCount)
            {

                int takingFromIndex = rand.Next(treasureIndexes.Count);
                int putTreasureTo = treasureIndexes.ElementAt(takingFromIndex);
                treasureIndexes.RemoveAt(takingFromIndex);
                int toW = putTreasureTo % width;
                int toH = putTreasureTo / width;
                this.treasureMap[toH][toW] = true;
                treasuresGenerated += 1;
            }
            Console.WriteLine("Generated " + treasuresGenerated.ToString() + " treasures.");
        }

        private void checkOnTreasure()
        {
            if (this.treasureMap[currentCharPosY][currentCharPosX] == true)
            {
                this.treasureMap[currentCharPosY][currentCharPosX] = false;
                this.treasuresFound += 1;
                Console.WriteLine("You found a treasure!");
                Console.WriteLine("Total score for now: " + this.treasuresFound.ToString());
                this.map[currentCharPosY][currentCharPosX] = " T ";
            }

        }

        public Boolean checkAllTreasuresFound()
        {
            if (this.treasuresFound < this.treasuresOnMap)
            {
                return false;
            }
            return true;
        }

        private Boolean ifWasTreasureBefore(){
            if (this.map[currentCharPosY][currentCharPosX] == " T ")
            {
                return true;
            }
            return false;
        }

        public void printTreasureMap()
        {
            Console.WriteLine();
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    if (x < this.width - 1)
                    {
                        Console.Write(this.treasureMap[y][x].ToString());
                    }
                    else
                    {
                        Console.WriteLine(this.treasureMap[y][x].ToString());
                    }
                }
            }
        }

    }
}

