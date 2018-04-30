// AUTHOR:  Sihui Zhao
// FILENAME:  driver.cs
// DATE:  4/29/2018
// REVISION HISTORY: 
// REFERENCES (optional):
// Description : The class was used to create a driver object. Each driver object
//               contains an encryptWOrd array and an encryptWord object. 
//                The encryptWord array would contains six encryptWOrd object, 
//                which is used to test encryptWord Object's basic functions such 
//                as encryption, guess, showing static data and so on. The other
//                encryptWord outside the array is for test reset method in encryptWord. 
//               The class contains 5 methods. 
//               1. The first method is for checking input validation. If an input is valid,
//                  then it would use the input and add it to encryptWord object. If the input
//                  is not, an error message should diplay and nothing should have changed. 
//
//               2. The second method is used to print the encryptWOrd array's data. It should
//                   display encryptWord's original input, its encryptResult and its current state. 
//                   The function is used for formatting and easy for main to call. 
//
//               3.The second function is used for user to guess shift number for one spefic 
//                  encryptWord object in the array. It took two as parameters, one for choose the 
//                  encryptWord object, the other one is for guess the shift number. some properties
//                  and attributes might change after the method. The method directly call 
//                  EncryptWordCheck method from encryption class. After guessing the correct number, 
//                  the static data of guessing would display for that object. 
//
//               4. The method is used to print the static data for guessing game of all encryptWord
//                  object in the array. It would display object's inputword, total guesses, 
//                   the number of guesses higher than correct answer, 
//                  the number of guesses lower than correct answer and  the average guesses values. 
//                  It is used for formatting. 
//              
//               5. The last method is used to check if EncryptWord class's reset method works. It would 
//                  display the encryption and state before guessing, after guessing and after resetting.
//                  The sate and encryptResult should change within the method. 
// 
//Assumptions  : there is not much assumption happen in this class, because a lot of them gets handle and 
//               prompts error messages. There are some below: 
//               1. EncryptWord class should in the same file as Driver. EncryptWord should also fully implement
//               2. CheckinputValid should be called before calling other methods
//               3. There should no invalid input in the EncryptWord object. State remains off in that case.
//               4. PrintGUess should be called after guess functions being called. 
//
//Anticipated Use: I would expect user to first create a driver object. Then, he/she should
//                 call for checkInput validation which checks validation. If valid, it
//                 setEncryptWord to input six word for six object to play. The state change. 
//                 PrintWord method would help to display the encrypt answer on the console.
//                 He/she would guess the shift number based on the encrypt result. 
//                  It called guesss function to  put on guesses for one encryptWord object.
//                  Once the correct shift number appear, 
//                  a message would appear with input word. The state changed back to false. 
//                 It would also show static data of guessing for that object. 
//                  He/She could call for printGuess for static data of six EncryptWord object. 
//                   Additionally, She/He could call reset to  check reset function to continue the game, 
//                  then all the statistic data would start from 0.
//                  State changed back to true to indicate encrypt happens. 
//
//interface variance : In general, the functions are public which easy for main to call. 
//
//class invariance:  All class invariance set to public for easy access. Because this class 
//                   serves as a bridge to connect main and encryptWord class. Thus, all invariances
//                   should be public for easy access. 
// Dependencies: This class depends on encryptWord class to use some of its functionality. 
//
// State:     1. No state change with constructor
//            2. State change to true for six EncryptWOrd object in the array if all input valid
//            3. It might have state change when calling guess function. 
//            4. When state is false, you can not use guess function. it would prompt message
//            5. State ->on when setEncryptWord happens and state can change on to off once 
//               correct shift number guessed. Sate change back to on once reset has called. 
//
//illegal input: 1. The assumption for the word less than 4 length and not all
//                  lower case letters. The illegal input is handled by printing message on the 
//                  console.
//               2. User guess shift number is more than 27 and less than 0. It also not integer. 
//                  This is also handled by printing message on the console and no further steps 
//                  allowed.

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
    class driver
    {
        public encryptWord[] a = new encryptWord[6];//encryptWord holder
        public encryptWord b;//for reset use

        //description: default constructor that used to create Driver object.
        //             The initial Constructor doesnt need additional informtion 
        //precondition: encryptWord object has to be exist before construt this class
        //postCondition: 
        public driver()
        {
        }

        //description:method used to check input validation.If the input format is invaild, 
        //            there will be an error message prompts and the encryptWord object doesnt
        //            change. If the input is valid, there will be six encryptWord object that set 
        //            with the input words. 
        //precondition: none
        //postCondition: if information is valid, the encryptWord object would call for set method to
        //               do the encryption based on input words. 
        public void CheckInputValid(string[] word)
        {
            for (int K = 0; K < word.Length; K++)
            {
                a[K] = new encryptWord();
                string curWord = word[K];

                
                 if(a[K].setEncryptWord(curWord) == false || curWord.Length <4)
                {
                    Console.WriteLine(curWord + "  Can not be encrypt due to uncorrect format");
                    Console.WriteLine("Correct format: long than 4 character and lower case alphabet letter\n");
                }
            }
         }

        //description: this is the method to print out six encrytWord original input, its 
        //              encryptResult and the state of the encryptWord.
        //precondition: none
        //postCondition: none, there is no change after the method. 
        public void printWord()
        {
            String s = String.Format("{0,10}{1,6} {2,15}{3,18}\n\n","OrderNum  ", "Original Word", "EncryptResult","EncryptState");

            for (int I =0; I < 6; I++)
            {
                s += String.Format("{0,10}  {1,6} {2,15} {3,18:NO}\n",
                    I, a[I].getOriginal(), a[I].getResult(),a[I].checkState());
            }
            Console.WriteLine(s);
        }

        //description: this is the method to guess the random shift number for one specific
        //             encryptWord object. It took two intergers as parameters. First one is used 
        //             to look up which one it needs to guess. The second parameter is user's guess 
        //             of the shift number for that specfic object. User only can guess when that object 
        //             on(means it is still encrypted). If the state is off, it would prompts an message
        //             to indicate the situation. 
        //precondition: none
        //postCondition: there might have guesses, high_guesses, low_guesses and state change after
        //              the method
        public void guess(int order, int guessNum)
        {
            string tmp = a[order].checkState();

            if(tmp.Equals("on"))
            {
                a[order].EncryptWordCheck(guessNum);
            }
            else {
                Console.WriteLine("Encrypt Mode is off for " + a[order].getOriginal()
             + "\nNo guess function available.");
                    }
        }

        //description: this is the method to print out six encrytWord static data for guessing. 
        //             It would display the object original input, total guesses, the number of guesses
        //             higher than correct answer, the number of guesses lower than correct answer and 
        //             the average guesses values. 
        //precondition: none
        //postCondition: none, there is no change after the method. 
        public void printGuess()
        {
            Console.WriteLine("name | total |highest|lowest| average guess value");
            string s = "";

            for (int I=0; I<6; I++)
            {
                s += a[I].getOriginal() + "    " + a[I].getCount().ToString() + "       " + a[I].getHighestCount().ToString() + "        "
                         + a[I].getLowestCount().ToString() + "         " + a[I].getAverage().ToString()+"\n";
            }
            Console.WriteLine(s);

        }

        //description: this is the method to test the reset function of one encryptWord object. 
        //             The encryptWord object is defined in the below. It would display state and 
        //             encryptResult before calling reset. Then it would displaying the same information
        //             after the reset. Thus, it shows how success the encryptWord class 's reset work.
        //precondition: none
        //postCondition: State would change from off to on after the method. EncryptResult will be different
        //               because of different shift number. 
        public void testReset()
        {
            b = new encryptWord();
            b.setEncryptWord("verytired");
            String s = String.Format("{0,6} {1,15}{2,18}\n\n",  "Original Word", "EncryptResult", "EncryptState");
            s += String.Format(" {0,6} {1,15} {2,18:NO}",b.getOriginal(), b.getResult(), b.checkState());
            Console.WriteLine(s);

            Console.WriteLine("\nAfter correctly guess the number: ");
            b.EncryptWordCheck( b.getRdm());
            Console.WriteLine("\n\nAfter reset:   ");
            b.reset();
            String d = String.Format("{0,6} {1,15}{2,18}\n\n", "Original Word", "EncryptResult", "EncryptState");
            d += String.Format(" {0,6} {1,15} {2,18:NO}",b.getOriginal(), b.getResult(), b.checkState());
            Console.WriteLine(d);
        }
    }
}
