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
            
            Map map = new Map(width, height);


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
                Console.Write("Enter your move (left/right/up/down): ");
                String characterMovement = Console.ReadLine();
                if (characterMovement.Equals("left"))
                {
                    map.moveCharacterLeft();
                }
                if (characterMovement.Equals("right"))
                {
                    map.moveCharacterRight();
                }
                if (characterMovement.Equals("up"))
                {
                    map.moveCharacterUp();
                }
                if (characterMovement.Equals("down"))
                {
                    map.moveCharacterDown();
                }
            }
            
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
        private Character characterOnMap;
        private List<List<String>> map = new List<List<String>>();

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            generateMap(width, height);
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
        }


        public void moveCharacterRight()
        {
            this.map[currentCharPosY][currentCharPosX] = " : ";
            this.currentCharPosX += 1;
            if (this.currentCharPosX >= this.width)
            {
                this.currentCharPosX -= this.width;
            }
            this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
        }

        public void moveCharacterLeft()
        {
            this.map[currentCharPosY][currentCharPosX] = " : ";
            this.currentCharPosX -= 1;
            if (this.currentCharPosX < 0)
            {
                this.currentCharPosX += this.width;
            }
            this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
        }

        public void moveCharacterUp()
        {
            this.map[currentCharPosY][currentCharPosX] = " : ";

            this.currentCharPosY -= 1;
            if (this.currentCharPosY < 0)
            {
                this.currentCharPosY += this.height;
            }
            this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
        }

        public void moveCharacterDown()
        {
            this.map[currentCharPosY][currentCharPosX] = " : ";
            this.currentCharPosY += 1;
            if (this.currentCharPosY >= this.height)
            {
                this.currentCharPosY -= this.height;
            }
            this.map[currentCharPosY][currentCharPosX] = this.characterOnMap.specialSymbol;
        }

        public void printDescription()
        {
            Console.WriteLine("Width: " + width + " Height: " + height);
        }

    }
}

