// AUTHOR:  Sihui Zhao
// FILENAME:  main.cs
// DATE:  4/29/2018
// REVISION HISTORY: 
// REFERENCES (optional):
// Description : The class was used to test driver class.  The program mainly use to 
//               test if it can encrypt 6 words, guess the random shift value for each one 
//               and test for reset. The detailed explaination as follow:
//                1. The first test is for checking if the program could run with invalid input. 
//                   When it prompts invalid input, it called driver class to check if it can encrypt. 
//                   A message would display and there should no encryption happen. 
//
//               2. The second test is for checking if the program could run with valid input. 
//                   When it prompts valid input, it called driver class to check if it can encrypt. 
//                   Encryption happens for the objects. It should displays six object's original input
//                   word, encryptResult, EncryptState. If an object is encrypted, its state should be on. 
//
//               3.The third and fourth function is used for user to guess shift number for one spefic 
//                  encryptWord object in the array. The method directly call guess in the driver class and
//                  indirecly call EncryptWordCheck method from encryption class. If not guessing the correct number,
//                  it could print a sentence to guide you. After guessing the correct number, 
//                  the static data of guessing would display for that object. That object state becomes off. 
//
//               4. The fifth test is used to print the static data for guessing game of all six encryptWord
//                  object in the array. It would display objects' inputword, total guesses, 
//                   the number of guesses higher than correct answer, 
//                  the number of guesses lower than correct answer and  the average guesses values. 
//              
//               5. The last test is used to check if driver's class reset method works. It would indirectly call
//                  EncrptWOrd class reset to 
//                  display the encryption and state before guessing, after guessing and after resetting.
//                  The sate and encryptResult should change within the method.
// 
//Assumptions  : there is not much assumption happen in this class, because a lot of them gets handle and 
//               prompts error messages. There are some below: 
//               1. Main class should be in the same file as EncryptWord class and  Driver class.
//               2. Test seven EncryptWord object in the program. One is only for reset, the other six
//                  is for generate use. 
//               3. illegal input is all handle in main and driver class. 
//
//Anticipated Use: I would expect user to follow five tests step by step. The first two would automatic 
//                 appear. The third one and fourth one, user need to follow my requirement to enter 
//                 command. After correctly entering command, it would appear third and fourth test
//                 result. If not correctly enter, it would display a messgae to guide user. The 
//                 fourth and fifth test should also automaticlly appear. 
//
//interface variance : No public function here
//
//class invariance:  No class invaiance used here, but do have local invariance. Local invariance has helped 
//                   with coding 
// Dependencies: This class used encryptWord class and driver class. It also create a driver object. However,
//               no strong dependencies. 
//
// State:     
//            1. State change to true for six EncryptWOrd object in the array if all input valid
//            2. It might have state change when calling guess function. 
//            3. Reset test: State ->on first then state can change on to off once 
//               correct shift number guessed. Sate change back to on once encrypt Class reset happens. 
//
//illegal input: 1. The assumption for the word less than 4 length and not all
//                  lower case letters. The illegal input is handled by printing message on the 
//                  console and no further steps allowed.
//               2. User guess shift number is more than 27 and less than 0. It also not integer. 
//                  This is also handled by printing message on the console and no further steps 
//                  allowed.
//               3. User enter order number should from 0-5. Any other number is not accepted. 

//
//legal input and output: When it enters legal input(which doesnt include in illegal input), 
//                       the program would expect run well. The program would display the encrypt
//                       Results when it put an  input words in. User based on that to guess. When they 
//                       guess, it would either correct or incorrect message to guide them. 
using System;
using System.Collections.Generic;
using System.Text;

namespace p1
{
    class main
    {

        static void Main()
        {
            Console.WriteLine("");

            Console.WriteLine("*******************************************************");
            Console.WriteLine("Welcome to Katty's EncryptWord Program\n");

            Console.WriteLine("This program used to encrypt 6 word, guess the shift numer");
            Console.WriteLine("and show the static of your guesses\n");

            Console.WriteLine("To start with the program, you will need know that it ");
            Console.WriteLine("it could only allow encrypt 6 words per program");
            Console.WriteLine("and the six word is pre-determinded in driver class\n");

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("(1) first function, to encrypt a word based on a random shift key,with invalid input");
            driver testExample = new driver(); //create driver object
            Console.WriteLine("encrpt word 'hel', 'Kitty', 'life %$s', 'LisssSSS','al','@eas'\n");
            string[] example1 = { "hel", "Kitty", "life%$s", "LisssSSS", "al", "@eas" };
            testExample.CheckInputValid(example1);
            
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("(2)Second function, to encrypt a word based on a random shift key,with valid input");
            string[] example2 = { "hello", "kitty", "lifehard", "youshould", "still", "smile" };
            Console.WriteLine("encrpt word'hello', 'kitty', 'lifehard', 'youshould', 'still', 'smile'\n");
            testExample.CheckInputValid(example2);
            testExample.printWord();
            Console.WriteLine("-------------------------------------------------------");

            Console.WriteLine("(3)To guess which the random shif number of one specific word");
            Console.WriteLine("You need to enter:<object-order><space><your guess>");
            Console.WriteLine("Example:1 16");
            Console.WriteLine("Exit enter:99 99");
            Console.WriteLine("orderNum listed from 1-6 of previous section and guess number is 1-26");
            Console.WriteLine("Once you had the correct guess, it will show the correct shift number");
            Console.WriteLine("encyrpt Result changed back to original word, ");
            Console.WriteLine("State change to off to indicate encrypt mode is off\n");
            Console.WriteLine("(4)It will also show the static data for your guess!");

            bool repeat = true;

            //use to ask user enter mutiple guesses until user decide to exit
            while (repeat == true) {
                Console.Write("Enter your command: ");
                string command = Console.ReadLine();
                string[] tokens = command.Split();
                int[] itokens = new int[tokens.Length];
                //check input validation
                if (tokens.Length == 2) {
                    for (int I = 0; I < 2; I++)
                    {
                        int n;
                        bool isNumeric = int.TryParse(tokens[I], out n);
                        if (isNumeric == true)
                        {
                            itokens[I] = n;
                        }
                    }
                    //user can exit once it hit 99
                    if (itokens[0] == 99)
                    {
                        repeat = false;
                    }
                    //check user validation
                    else if (itokens[0] < 0 || itokens[0] >= 6 || itokens[1] <1 || itokens[1] >26){
             
                        Console.WriteLine("Wrong Command. Order num from 1-6. Guess should from 1-26");
                    }
                    else
                    {
                        testExample.guess(itokens[0], itokens[1]);
                    }
                       
                }
                else
                {
                    Console.WriteLine("Wrong Command. Command should be two integer, seprate by space");
                }
            }

            Console.WriteLine("-------------------------------------------------------");

            Console.WriteLine("(5)To display all guess static:   ");
            testExample.printGuess();

            Console.WriteLine("-------------------------------------------------------\n");
            Console.WriteLine("(6) reset function: give the word a new random number for a new Enrypt Result");
            Console.WriteLine("State change from off to on");
            testExample.testReset();

            Console.WriteLine("******************************************************");


            Console.ReadKey();
        }

           
    }

}
        
