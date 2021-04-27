
using System;
using System.IO;
namespace CSPreASSkelton
{
    class Program
    {
        public const int N_S_DISTANCE = 5;
        public const int W_E_DISTANCE = 7;
        public struct CellReference { public int NoOfCellsSouth; public int NoOfCellsEast; }
        static void Main(string[] args)
        {
            char[,] Cavern = new char[N_S_DISTANCE, W_E_DISTANCE];
            int Choice = 0;
            CellReference MonsterPosition = new CellReference();
            CellReference PlayerPosition = new CellReference();
            CellReference FlaskPosition = new CellReference();

            while (Choice != 3)
            {
                DisplayMenu();
                Choice = GetMainMenuChoice();
                switch (Choice)
                {
                    case 1:
                        SetUpGame(Cavern, ref MonsterPosition, ref PlayerPosition, ref FlaskPosition);
                        PlayGame(Cavern, ref MonsterPosition, ref PlayerPosition, ref FlaskPosition);
                        break;
                    case 2:
                        SetUpTrainingGame(Cavern, ref MonsterPosition, ref PlayerPosition);
                        PlayGame(Cavern, ref MonsterPosition, ref PlayerPosition, ref FlaskPosition);
                        break;
                }
            }
        }
        public static void DisplayMenu()
        {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("1. Start new game");
            Console.WriteLine("2. Play training game");
            Console.WriteLine("3. Quit");
            Console.WriteLine();
            Console.WriteLine("Please enter your choice: ");
        }
        public static int GetMainMenuChoice()
        {
            int Choice; Choice = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return Choice;
        }
        public static void ResetCavern(char[,] Cavern)
        {
            int Count1;
            int Count2;
            for (Count1 = 0; Count1 < N_S_DISTANCE; Count1++)
            {
                for (Count2 = 0; Count2 < W_E_DISTANCE; Count2++)
                {
                    Cavern[Count1, Count2] = ' ';
                }
            }
        }
        public static CellReference GetNewRandomPosition()
        {
            CellReference Position = new CellReference();
            Random rnd = new Random();
            Position.NoOfCellsSouth = rnd.Next(0, N_S_DISTANCE);
            Position.NoOfCellsEast = rnd.Next(0, W_E_DISTANCE);
            return Position;
        }
        public static void SetUpGame(char[,] Cavern, ref CellReference MonsterPosition, ref CellReference PlayerPosition, ref CellReference FlaskPosition)
        {
            ResetCavern(Cavern);
            //PlayerPosition.NoOfCellsSouth = 0;
            //PlayerPosition.NoOfCellsEast = 0;

            PlayerPosition = GetNewRandomPosition();

            Cavern[PlayerPosition.NoOfCellsSouth, PlayerPosition.NoOfCellsEast] = '*';

            do
            {
                MonsterPosition = GetNewRandomPosition();
            }

            while (!(Cavern[MonsterPosition.NoOfCellsSouth, MonsterPosition.NoOfCellsEast] == ' '));

            Cavern[MonsterPosition.NoOfCellsSouth, MonsterPosition.NoOfCellsEast] = 'M';

            do
            {
                FlaskPosition = GetNewRandomPosition();
            }

            while (!(Cavern[FlaskPosition.NoOfCellsSouth, FlaskPosition.NoOfCellsEast] == ' '));

            Cavern[FlaskPosition.NoOfCellsSouth, FlaskPosition.NoOfCellsEast] = 'F';

        }
        public static void SetUpTrainingGame(char[,] Cavern, ref CellReference MonsterPosition, ref CellReference PlayerPosition)
        {
            ResetCavern(Cavern);
            PlayerPosition.NoOfCellsSouth = 2;
            PlayerPosition.NoOfCellsEast = 4;
            Cavern[PlayerPosition.NoOfCellsSouth, PlayerPosition.NoOfCellsEast] = '*';
            MonsterPosition.NoOfCellsSouth = 0;
            MonsterPosition.NoOfCellsEast = 3;
            Cavern[MonsterPosition.NoOfCellsSouth, MonsterPosition.NoOfCellsEast] = 'M';
        }
        public static void DisplayCavern(char[,] Cavern)
        {
            int Count1;
            int Count2;
            for (Count1 = 0; Count1 < N_S_DISTANCE; Count1++)
            {
                Console.WriteLine(" -------------");
                for (Count2 = 0; Count2 < W_E_DISTANCE; Count2++)
                {
                    if (Cavern[Count1, Count2] == ' ' || Cavern[Count1, Count2] == '*' || Cavern[Count1, Count2] == 'M' || Cavern[Count1, Count2] == 'F')
                    {
                        Console.Write("|" + Cavern[Count1, Count2]);
                    }
                    else
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(" -------------");
            Console.WriteLine();
        }
        public static void DisplayMoveOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Enter N or n to move NORTH");
            Console.WriteLine("Enter E or e to move EAST");
            Console.WriteLine("Enter S or s to move SOUTH");
            Console.WriteLine("Enter W or w to move WEST");
            Console.WriteLine("Enter M to return to the Main Menu");
            Console.WriteLine();
        }
        public static char GetMove()
        {
            char Move;
            Move = char.Parse(Console.ReadLine());
            Console.WriteLine();
            return Move;
        }
        public static void MakeMove(char[,] Cavern, char Direction, ref CellReference PlayerPosition)
        {
            Cavern[PlayerPosition.NoOfCellsSouth, PlayerPosition.NoOfCellsEast] = ' ';
            switch (Direction)
            {
                case 'N':
                    PlayerPosition.NoOfCellsSouth = PlayerPosition.NoOfCellsSouth - 1;
                    break;
                case 'S':
                    PlayerPosition.NoOfCellsSouth = PlayerPosition.NoOfCellsSouth + 1;
                    break;
                case 'W':
                    PlayerPosition.NoOfCellsEast = PlayerPosition.NoOfCellsEast - 1;
                    break;
                case 'E':
                    PlayerPosition.NoOfCellsEast = PlayerPosition.NoOfCellsEast + 1;
                    break;
                case 'n':
                    PlayerPosition.NoOfCellsSouth = PlayerPosition.NoOfCellsSouth - 1;
                    break;
                case 's':
                    PlayerPosition.NoOfCellsSouth = PlayerPosition.NoOfCellsSouth + 1;
                    break;
                case 'w':
                    PlayerPosition.NoOfCellsEast = PlayerPosition.NoOfCellsEast - 1;
                    break;
                case 'e':
                    PlayerPosition.NoOfCellsEast = PlayerPosition.NoOfCellsEast + 1;
                    break;
            }
            Cavern[PlayerPosition.NoOfCellsSouth, PlayerPosition.NoOfCellsEast] = '*';
        }
        public static Boolean CheckValidMove(CellReference PlayerPosition, char Direction)
        {
            Boolean ValidMove;
            ValidMove = true;
            if (!(Direction == 'N' || Direction == 'S' || Direction == 'W' || Direction == 'E' || Direction == 'M' || Direction == 'n' || Direction == 's' || Direction == 'w' || Direction == 'e'))
            {
                ValidMove = false;
            }
            else if ((Direction == 'N' && PlayerPosition.NoOfCellsSouth == 0) || (Direction == 'S' && PlayerPosition.NoOfCellsSouth == N_S_DISTANCE - 1))
            {
                ValidMove = false;
            }
            return ValidMove;
        }
        public static Boolean CheckIfSameCell(CellReference FirstCellPosition, CellReference SecondCellPosition)
        {
            Boolean InSameCell = false;
            if (FirstCellPosition.NoOfCellsSouth == SecondCellPosition.NoOfCellsSouth && FirstCellPosition.NoOfCellsEast == SecondCellPosition.NoOfCellsEast)
            {
                InSameCell = true;
            }
            return InSameCell;
        }

        public static void MoveFlask(char [,] Cavern, CellReference NewCellForFlask, ref CellReference FlaskPosition)
        {
            Cavern[NewCellForFlask.NoOfCellsSouth, NewCellForFlask.NoOfCellsEast] = 'F';
            Cavern[FlaskPosition.NoOfCellsSouth, FlaskPosition.NoOfCellsEast] = ' ';
            FlaskPosition = NewCellForFlask;
        }
        public static void MakeMonsterMove(char[,] Cavern, ref CellReference MonsterPosition, CellReference PlayerPosition, ref CellReference FlaskPosition, ref CellReference NewCellForFlask)
        {
            CellReference OriginalMonsterPosition = new CellReference();
            OriginalMonsterPosition.NoOfCellsSouth = MonsterPosition.NoOfCellsSouth;
            OriginalMonsterPosition.NoOfCellsEast = MonsterPosition.NoOfCellsEast;
            Cavern[MonsterPosition.NoOfCellsSouth, MonsterPosition.NoOfCellsEast] = ' ';
            
            if (MonsterPosition.NoOfCellsSouth < PlayerPosition.NoOfCellsSouth)
            {
                MonsterPosition.NoOfCellsSouth = MonsterPosition.NoOfCellsSouth + 1;
            }
            else if (MonsterPosition.NoOfCellsSouth > PlayerPosition.NoOfCellsSouth)
            {
                MonsterPosition.NoOfCellsSouth = MonsterPosition.NoOfCellsSouth - 1;
            }

            else if (MonsterPosition.NoOfCellsEast < PlayerPosition.NoOfCellsEast)
            {
                MonsterPosition.NoOfCellsEast = MonsterPosition.NoOfCellsEast + 1;

            }

            else if (MonsterPosition.NoOfCellsEast > PlayerPosition.NoOfCellsEast)
            {
                MonsterPosition.NoOfCellsEast = MonsterPosition.NoOfCellsEast - 1;
            }
            Boolean Flipped = false;

            Flipped = CheckIfSameCell(MonsterPosition, FlaskPosition);
            if (!Flipped)
            {
                Cavern[MonsterPosition.NoOfCellsSouth, MonsterPosition.NoOfCellsEast] = 'M';
            }
            else if (Flipped)
            {
                //east
                MonsterPosition.NoOfCellsEast = FlaskPosition.NoOfCellsEast;
                FlaskPosition.NoOfCellsEast = OriginalMonsterPosition.NoOfCellsEast;
                //south
                MonsterPosition.NoOfCellsSouth = FlaskPosition.NoOfCellsSouth;
                FlaskPosition.NoOfCellsSouth = OriginalMonsterPosition.NoOfCellsSouth;
            }
        }
        public static void DisplayLostGameMessage()
        {
            Console.WriteLine("ARGHHHHHH! The monster has eaten you. GAME OVER.");
            Console.WriteLine("Maybe you will have better luck the next time you play MONSTER!");
            Console.WriteLine();
        }
        public static void DisplayWonGameMessage()
        {
            Console.WriteLine("Yaaaaayyyyyy! You have got the flask and beaten the monster.");
            Console.WriteLine();
        }
        public static void PlayGame(char[,] Cavern, ref CellReference MonsterPosition, ref CellReference PlayerPosition, ref CellReference FlaskPosition)
        {
            Boolean Win = false;
            Boolean Eaten = false;
            Boolean ValidMove = false;
            int Count = 0;
            char MoveDirection = ' ';
            DisplayCavern(Cavern);
            while (!(Eaten || MoveDirection == 'M' || Win))
            {
                ValidMove = false;
                while (!ValidMove)
                {
                    DisplayMoveOptions();
                    MoveDirection = GetMove();
                    ValidMove = CheckValidMove(PlayerPosition, MoveDirection);
                }
                if (MoveDirection != 'M')
                {
                    MakeMove(Cavern, MoveDirection, ref PlayerPosition);
                    DisplayCavern(Cavern);
                    Eaten = CheckIfSameCell(MonsterPosition, PlayerPosition);
                    Win = CheckIfSameCell(FlaskPosition, PlayerPosition);      
                }
                if (!Eaten && !Win)
                {
                    DisplayCavern(Cavern);

                    Count = 0;
                    while (Count < 2 && !Eaten && !Win)
                    {
                        MakeMonsterMove(Cavern, ref MonsterPosition, PlayerPosition, ref FlaskPosition);
                        Eaten = CheckIfSameCell(MonsterPosition, PlayerPosition);
                        Console.WriteLine();
                        Console.WriteLine("Press Enter key to continue");
                        Console.ReadLine();
                        DisplayCavern(Cavern);
                        Count = Count + 1;
                    }
                }

              
                if (Eaten)
                {
                    DisplayLostGameMessage();
                }

                if (Win)
                {
                    DisplayWonGameMessage();
                }
            }
        }
    }
}