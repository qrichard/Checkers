using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finished_Checkers
{
    class Program
    {
        struct piece
        {

            public bool red; 

            public int row; 

            public int col; 

            public bool king; 

            public bool inPlay;

        }

        static void Main(string[] args)

        {

            Console.WriteLine("           ____            ____    ____            ____    ____    ____");
            Console.WriteLine("          |        |  |   |       |        |  /   |       |    |   \\  ");
            Console.WriteLine("          |        |__|   |____   |        | /    |____   |____|    \\  ");
            Console.WriteLine("          |        |  |   |       |        | \\    |       |    \\     \\ ");
            Console.WriteLine("          |____    |  |   |____   |____    |  \\   |____   |     \\  ___\\");
            Console.WriteLine("          ______________________________________________________________");
            Console.ReadKey();
            Console.Clear();

            piece[] redPieces = new piece[12]; 

            piece[] blackPieces = new piece[12];

            int turnCount = 0;

            int[,] board = new int[8, 8]; 

            drawBoard(board);

            startPieces(redPieces, true, board); 

            startPieces(blackPieces, false, board); 

            int redCount = Count(redPieces);

            int blackCount = Count(blackPieces); 

            drawBoard(board, redCount, blackCount); 


            while (redCount > 0 && blackCount > 0)
            {

                turnCount++; 

                bool done = false;

                bool again = false; 

                int selectedPiece = -1; 

                Console.SetCursorPosition(0, 0); 



               

                if (turnCount % 2 == 1)
                {


                    while (!done)
                    {


                        string input = Prompt("Player One: select your piece");

                        int[] piecePos = getPosition(input);



                        input = Prompt("Where would like to move your piece?");

                        int[] newPos = getPosition(input);




                        decimal rowAbs = Math.Abs((decimal)(newPos[0] - piecePos[0]));

                        decimal colAbs = Math.Abs((decimal)(newPos[1] - piecePos[1]));



                        int colMid = (newPos[1] + piecePos[1]) / 2;

                        int rowMid = (newPos[0] + piecePos[0]) / 2;

                        int[] midPos = { rowMid, colMid };


                        selectedPiece = checkPiece(redPieces, piecePos, board);




                        if (selectedPiece == -1)
                        {

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if ((piecePos[0] < 0 || piecePos[0] > 7) || (piecePos[1] < 0 || piecePos[1] > 7))
                        {

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if ((newPos[0] < 0 || newPos[0] > 7) || (newPos[1] < 0 || newPos[1] > 7))
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if (board[newPos[0], newPos[1]] != 0)
                        {

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if (redPieces[selectedPiece].king == false && newPos[0] <= piecePos[0])
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if (rowAbs > 2 || colAbs > 2)
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if (!(rowAbs == colAbs))
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if ((rowAbs == 2 && colAbs == 2) && (board[rowMid, colMid] % 2 == 1 || board[rowMid, colMid] == 0))
                        { 

                            Console.WriteLine("Invalid move, try again.");                                                         

                            pause();

                        }
                        else if ((rowAbs == 2 && colAbs == 2) && board[rowMid, colMid] % 2 == 0)
                        { 

                            redPieces[selectedPiece] = movePiece(redPieces[selectedPiece], newPos, board);

                            int tempPiece = checkPiece(blackPieces, midPos, board);                      

                            blackPieces[tempPiece] = removePiece(blackPieces[tempPiece], board);

                            blackCount = Count(blackPieces);

                            again = true;

                        }
                        else
                        {

                            redPieces[selectedPiece] = movePiece(redPieces[selectedPiece], newPos, board);

                            done = true;

                        }

                        king(redPieces, board);

                        int tempCount = 1; 


                        Console.Clear();

                        drawBoard(board, redCount, blackCount);

                        Console.SetCursorPosition(0, 0);



                        while (again)
                        {


                            tempCount++;


                            Console.WriteLine("If you gotta jump you gotta jump. Enter 'q' to end turn.");

                            input = Prompt("Please enter the position for jump " + tempCount);


                            if (input.Contains('q'))
                            {

                                again = false;

                                done = true;

                            }


                            if (again)
                            {

                                newPos = getPosition(input);

                                piecePos[0] = redPieces[selectedPiece].row;

                                piecePos[1] = redPieces[selectedPiece].col;

                                rowAbs = Math.Abs((decimal)(newPos[0] - piecePos[0]));

                                colAbs = Math.Abs((decimal)(newPos[1] - piecePos[1]));

                                colMid = (newPos[1] + piecePos[1]) / 2;

                                rowMid = (newPos[0] + piecePos[0]) / 2;

                                midPos[0] = rowMid;

                                midPos[1] = colMid;

                                if ((newPos[0] < 0 && newPos[0] > 7) || (newPos[1] < 0 && newPos[1] > 7))
                                { 

                                    Console.WriteLine("Invalid move, try again.");

                                    pause();

                                }
                                else if (board[newPos[0], newPos[1]] != 0)
                                { 

                                    Console.WriteLine("Invalid move, try again.");

                                    pause();

                                }
                                else if (redPieces[selectedPiece].king == false && newPos[0] <= piecePos[0])
                                { 

                                    Console.WriteLine("Invalid move, try again.");

                                    pause();

                                }
                                else if ((rowAbs == 2 && colAbs == 2) && (board[rowMid, colMid] % 2 == 1 || board[rowMid, colMid] == 0))
                                { 

                                    Console.WriteLine("Invalid move, try again.");                                                         

                                    pause();

                                }
                                else if (!(rowAbs == 2 && colAbs == 2) && board[rowMid, colMid] % 2 == 0)
                                { 
                                    Console.WriteLine("Invalid move, try again.");

                                    pause();

                                }
                                else
                                { 

                                    redPieces[selectedPiece] = movePiece(redPieces[selectedPiece], newPos, board);

                                    int tempPiece = checkPiece(blackPieces, midPos, board);

                                    blackPieces[tempPiece] = removePiece(blackPieces[tempPiece], board);

                                    blackCount = Count(blackPieces);

                                }


                                king(redPieces, board);


                                Console.Clear();

                                drawBoard(board, redCount, blackCount);

                                Console.SetCursorPosition(0, 0);

                            }

                        }

                    }


                }
                else
                {


                    while (!done)
                    {



                        string input = Prompt("Player Two: select your piece");

                        int[] piecePos = getPosition(input);


                        input = Prompt("Where would like to move your piece?");

                        int[] newPos = getPosition(input);


                        decimal rowAbs = Math.Abs((decimal)(newPos[0] - piecePos[0]));

                        decimal colAbs = Math.Abs((decimal)(newPos[1] - piecePos[1]));


                        int colMid = (newPos[1] + piecePos[1]) / 2;

                        int rowMid = (newPos[0] + piecePos[0]) / 2;

                        int[] midPos = { rowMid, colMid };


                        selectedPiece = checkPiece(blackPieces, piecePos, board);


                        if (selectedPiece == -1)
                        {

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if ((piecePos[0] < 0 && piecePos[0] > 7) || (piecePos[1] < 0 && piecePos[1] > 7))
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if ((newPos[0] < 0 && newPos[0] > 7) || (newPos[1] < 0 && newPos[1] > 7))
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if (board[newPos[0], newPos[1]] != 0)
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if (blackPieces[selectedPiece].king == false && newPos[0] >= piecePos[0])
                        { 

                            Console.WriteLine("Wrong, try again.");

                            pause();

                        }
                        else if (rowAbs > 2 || colAbs > 2)
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if (!(rowAbs == colAbs))
                        { 

                            Console.WriteLine("Invalid move, try again.");

                            pause();

                        }
                        else if ((rowAbs == 2 && colAbs == 2) && (board[rowMid, colMid] % 2 == 0 || board[rowMid, colMid] == 0))
                        { 

                            Console.WriteLine("Invalid move, try again.");                                                         

                            pause();

                        }
                        else if ((rowAbs == 2 && colAbs == 2) && board[rowMid, colMid] % 2 == 1)
                        { 

                            blackPieces[selectedPiece] = movePiece(blackPieces[selectedPiece], newPos, board);

                            int tempPiece = checkPiece(redPieces, midPos, board);                            

                            redPieces[tempPiece] = removePiece(redPieces[tempPiece], board);

                            redCount = Count(redPieces);

                            again = true;

                        }
                        else
                        {

                            blackPieces[selectedPiece] = movePiece(blackPieces[selectedPiece], newPos, board);

                            done = true;

                        }


                        king(blackPieces, board);

                        int tempCount = 1;


                        Console.Clear();

                        drawBoard(board, redCount, blackCount);

                        Console.SetCursorPosition(0, 0);

        

                        while (again)
                        {


                            tempCount++;


                            Console.WriteLine("You gotta extra jump then take it. Enter 'q' to end turn.");

                            input = Prompt("Enter that position for jump " + tempCount);


                            if (input.Contains('q'))
                            {

                                again = false;

                                done = true;

                            }


                            if (again)
                            {



                                newPos = getPosition(input);

                                piecePos[0] = blackPieces[selectedPiece].row;

                                piecePos[1] = blackPieces[selectedPiece].col;

                                rowAbs = Math.Abs((decimal)(newPos[0] - piecePos[0]));

                                colAbs = Math.Abs((decimal)(newPos[1] - piecePos[1]));

                                colMid = (newPos[1] + piecePos[1]) / 2;

                                rowMid = (newPos[0] + piecePos[0]) / 2;

                                midPos[0] = rowMid;

                                midPos[1] = colMid;

                                if ((newPos[0] < 0 && newPos[0] > 7) || (newPos[1] < 0 && newPos[1] > 7))
                                { //if the new position index is outside of bound of the board, then invalid move

                                    Console.WriteLine("Invalid move, try again.");

                                    pause();

                                }
                                else if (board[newPos[0], newPos[1]] != 0)
                                { //if the selected new position is not an empty black square, then invalid move

                                    Console.WriteLine("Invalid move, try again.");

                                    pause();

                                }
                                else if (blackPieces[selectedPiece].king == false && newPos[0] >= piecePos[0])
                                { //if the piece is not a king and they are trying to move backwards, then invalid move

                                    Console.WriteLine("Invalid move, try again.");

                                    pause();

                                }
                                else if ((rowAbs == 2 && colAbs == 2) && (board[rowMid, colMid] % 2 == 0 || board[rowMid, colMid] == 0))
                                { //if the move is a jump but the jumpped piece is not

                                    Console.WriteLine("Invalid move, try again.");                                                         //the opposite player's, then invalid move

                                    pause();

                                }
                                else if (!((rowAbs == 2 && colAbs == 2) && board[rowMid, colMid] % 2 == 1))
                                { //if the move is anything other than a jump over the opposite player's

                                    Console.WriteLine("Invalid move, try again.");                            //piece, then invalid move

                                    pause();

                                }
                                else
                                {

                                    blackPieces[selectedPiece] = movePiece(blackPieces[selectedPiece], newPos, board); 

                                    int tempPiece = checkPiece(redPieces, midPos, board);

                                    redPieces[tempPiece] = removePiece(redPieces[tempPiece], board); 

                                    redCount = Count(redPieces); 

                                }

                                king(blackPieces, board);


                                Console.Clear();

                                drawBoard(board, redCount, blackCount);

                                Console.SetCursorPosition(0, 0);

                            }

                        }

                    }

                }


                redCount = Count(redPieces);

                blackCount = Count(blackPieces);

                Console.Clear();

                drawBoard(board, redCount, blackCount);

            }

            Console.Clear();

            drawBoard(board, redCount, blackCount);

            if (redCount > 0)
            {

                Console.SetCursorPosition(0, 0);

                Console.WriteLine("Red Win!");

            }
            else
            {

                Console.SetCursorPosition(0, 0);

                Console.WriteLine("Black Win!");

            }


            pauseClose();

        }

        static piece movePiece(piece temp, int[] newPos, int[,] board)
        {

            board[temp.row, temp.col] = 0;

            temp.col = newPos[1];

            temp.row = newPos[0];

            if (temp.king)
            {

                if (temp.red)
                {

                    board[newPos[0], newPos[1]] = 3;

                }
                else
                {

                    board[newPos[0], newPos[1]] = 4;

                }

            }
            else
            {

                if (temp.red)
                {

                    board[newPos[0], newPos[1]] = 1;

                }
                else
                {

                    board[newPos[0], newPos[1]] = 2;

                }

            }

            return temp;

        }

        static string Prompt(string msg)
        {

            Console.Write(msg + ": ");

            return Console.ReadLine();

        }

        static void drawBoard(int[,] board)
        {

            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {

                    if (i % 2 == 0 && j % 2 == 1)
                    {

                        board[i, j] = -1;

                    }
                    else if (i % 2 == 1 && j % 2 == 0)
                    {

                        board[i, j] = -1;

                    }
                    else
                    {

                        board[i, j] = 0;

                    }

                }

            }

        }

        static void king(piece[] array, int[,] board)
        {

            for (int i = 0; i < array.Length; i++)
            {

                if (array[i].red)
                {

                    if (array[i].row == 7)
                    {

                        array[i].king = true;

                        board[array[i].row, array[i].col] = 3;

                    }

                }
                else
                {

                    if (array[i].row == 0)
                    {

                        array[i].king = true;

                        board[array[i].row, array[i].col] = 4;

                    }

                }

            }

        }

        static int checkPiece(piece[] array, int[] position, int[,] board)
        {

            int temp = -1;

            if (position[0] == -1 || position[1] == -1)
            {

                return temp;

            }

            for (int i = 0; i < array.Length; i++)
            {

                if (array[i].row == position[0] && array[i].col == position[1])
                {

                    temp = i;

                }

            }

            return temp;

        }

        static void pause()
        {

            Console.WriteLine("Please press ANY key to continue.");

            Console.ReadKey();

        }

        static int Count(piece[] array)
        {

            int count = 0;

            for (int i = 0; i < array.Length; i++)
            {

                if (array[i].inPlay)
                {

                    count++;

                }

            }

            return count;

        }

        static piece removePiece(piece temp, int[,] board)
        {

            temp.inPlay = false;

            board[temp.row, temp.col] = 0;

            return temp;

        }

        static void startPieces(piece[] array, bool red, int[,] board)
        {

            int count = 0;

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 8; j++)
                {

                    if (i % 2 == 0 && j % 2 == 0)
                    {

                        if (red)
                        {

                            array[count].red = true;

                            array[count].row = i;

                            array[count].col = j;

                            array[count].king = false;

                            array[count].inPlay = true;

                            board[i, j] = 1;

                            count++;



                        }



                    }
                    else if (i % 2 == 1 && j % 2 == 1)
                    {

                        if (red)
                        {

                            array[count].red = true;

                            array[count].row = i;

                            array[count].col = j;

                            array[count].king = false;

                            array[count].inPlay = true;

                            board[i, j] = 1;

                            count++;

                        }

                    }
                    else if (i % 2 == 0 && j % 2 == 1)
                    {

                        if (!red)
                        {

                            array[count].red = false;

                            array[count].row = 7 - i;

                            array[count].col = j;

                            array[count].king = false;

                            array[count].inPlay = true;

                            board[(7 - i), (j)] = 2;

                            count++;

                        }

                    }
                    else if (i % 2 == 1 && j % 2 == 0)
                    {

                        if (!red)
                        {

                            array[count].red = false;

                            array[count].row = 7 - i;

                            array[count].col = j;

                            array[count].king = false;

                            array[count].inPlay = true;

                            board[(7 - i), j] = 2;

                            count++;

                        }

                    }

                }

            }

        }

        static void pauseTop()
        {

            Console.SetCursorPosition(0, 0);

            Console.WriteLine("Please press ANY key to continue.");

            Console.ReadKey();

        }

        static void pauseClose()
        {

            Console.SetCursorPosition(0, 1);

            Console.WriteLine("Please press ANY key to close the game.");

            Console.ReadKey();

        }

        static int[] getPosition(string input)
        {

            string temp = input.ToLower();

            int[] returnArray = new int[2];

            if (input.Length >= 2)
            {

                if (temp[0] >= 49 && temp[0] <= 57)
                {

                    switch (temp[0])
                    {

                        case '1': { returnArray[0] = 0; break; }

                        case '2': { returnArray[0] = 1; break; }

                        case '3': { returnArray[0] = 2; break; }

                        case '4': { returnArray[0] = 3; break; }

                        case '5': { returnArray[0] = 4; break; }

                        case '6': { returnArray[0] = 5; break; }

                        case '7': { returnArray[0] = 6; break; }

                        case '8': { returnArray[0] = 7; break; }

                        default: { returnArray[0] = -1; break; }

                    }

                    switch (temp[1])

                    {

                        case 'a': { returnArray[1] = 0; break; }

                        case 'b': { returnArray[1] = 1; break; }

                        case 'c': { returnArray[1] = 2; break; }

                        case 'd': { returnArray[1] = 3; break; }

                        case 'e': { returnArray[1] = 4; break; }

                        case 'f': { returnArray[1] = 5; break; }

                        case 'g': { returnArray[1] = 6; break; }

                        case 'h': { returnArray[1] = 7; break; }

                        default: { returnArray[1] = -1; break; }

                    }



                }
                else if (temp[0] >= 97 && temp[0] <= 104)
                {

                    switch (temp[0])

                    {

                        case 'a': { returnArray[1] = 0; break; }

                        case 'b': { returnArray[1] = 1; break; }

                        case 'c': { returnArray[1] = 2; break; }

                        case 'd': { returnArray[1] = 3; break; }

                        case 'e': { returnArray[1] = 4; break; }

                        case 'f': { returnArray[1] = 5; break; }

                        case 'g': { returnArray[1] = 6; break; }

                        case 'h': { returnArray[1] = 7; break; }

                        default: { returnArray[1] = -1; break; }

                    }

                    switch (temp[1])
                    {

                        case '1': { returnArray[0] = 0; break; }

                        case '2': { returnArray[0] = 1; break; }

                        case '3': { returnArray[0] = 2; break; }

                        case '4': { returnArray[0] = 3; break; }

                        case '5': { returnArray[0] = 4; break; }

                        case '6': { returnArray[0] = 5; break; }

                        case '7': { returnArray[0] = 6; break; }

                        case '8': { returnArray[0] = 7; break; }

                        default: { returnArray[0] = -1; break; }

                    }

                }

            }

            return returnArray;



        }

        static void drawBoard(int[,] board, int redCount, int blackCount)
        {

            string block = "███";

            Console.SetCursorPosition(4, 4);

            Console.Write(" A  B  C  D  E  F  G  H ");

            for (int i = 0; i < 8; i++)
            {

                Console.SetCursorPosition(2, (5 + i));

                Console.ForegroundColor = ConsoleColor.Black;

                Console.BackgroundColor = ConsoleColor.White;

                Console.Write(i + 1 + " ");

                for (int j = 0; j < 8; j++)
                {

                    switch (board[i, j])

                    {

                        case -1: { Console.ForegroundColor = ConsoleColor.DarkCyan; Console.BackgroundColor = ConsoleColor.White; Console.Write(block); break; }

                        case 0: { Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White; Console.Write(block); break; }

                        case 1: { Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Black; Console.Write(" 0 "); break; }

                        case 2: { Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black; Console.Write(" O "); break; }

                        case 3: { Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Black; Console.Write(" K "); break; }

                        case 4: { Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black; Console.Write(" K "); break; }

                        default:

                            break;

                    }

                }

            }

            Console.BackgroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine();

            Console.Write("     A  B  C  D  E  F  G  H \n\n");
        }
    }
}
