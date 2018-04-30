// AUTHOR:  Sihui Zhao
// FILENAME:  encryptWord.cs
// DATE:  4/12/2018
// REVISION HISTORY: 
// REFERENCES (optional):
// Description : The class was used to create the EncryptWord object. The 
//               object has implemented some funtions described as below. 
//               1. The object have created an encryptWord object.There is no
//                  parameter when construct the object. It generates a random
//                  number once constructed. The random number is generated with 
//                  minimum 1 and maxmum 26. The count, guesses, low_guesses and 
//                  high_guesses are also set to 0 when constructed. The constructor
//                  also have defined an Alphabet list to contains 26 alphabet letter
//                  for further use. State initially set to false to indicate no encyrpt 
//                  happens. 
//               2. The object has a setEncryptWord functions, 
//                  which it took a string as parameter. 
//                  The parameter was seen as user input word. The method also called 
//                  setOriginal and encryptWordResult helper method to transfer the
//                  input word into a encrypt version of word and then print out the 
//                  result. The state change to true to indicate encrypt happens. 
//
//               3. This class has a method called EncryptWordCheck, which allows user to 
//                  guess the shift number. The method records every guess that user have 
//                  entered, check if it is correct. Once Correct number has get, it would
//                  display message, state change to false to indicate no encypt happens.
//                  If not, the guess, high_guesses, and low_guesses will 
//                  changed based on condition. It would also display such information
//                  for statistic use. 
//               4. The class also allows reset function. The method was used to set 
//                  the object to a new shift number. Thus, it transfer the input word
//                  to a different encryptResult. The state set to true to incate a encrpt
//                  happens. 
//                  
//Assumptions  : 1. The assumption for the word should be over 4 length and
//                  lower case.
//               2. when users guess for shift number, it should be greater than 0 , 
//                  less than 27, also should be a integer. 
//               3. setEncryptWord should be called prior calling getCount....all get and 
//                  set method. 
//               4. The state is false to called the setEncryptWord. 
//               
//
//Anticipated Use: I would expect user to first create a encryptWord object. Then, he/she should
//                 call for setEncryptWord to input a word to play. The state change. 
//                 The encrypt answer would appear  on the console.
//                 He/she would guess the shift number based on the encrypt result. 
//                  It called EncryptWordCheck to put on guesses. Once the correct shift number appear, 
//                  a message would appear with input word. The state changed back to false. 
//                  He/She could check for the 
//                  total guesses, low_guesses and high_guesses. Additionally, She/He could call 
//                  reset to  continue the game, then all the statistic data would start from 0.
//                  State changed back to true to indicate encrypt happens. 
//
//interface variance : In general, the functions are public which easy for end-user to call. However, 
//                     setOriginal, setState and charCheck method are limited to private, 
//                     because it only allows this class 
//                     to use. All other methods should not have constrains while using it. 
//
//class invariance:  All class invariance set to private to protect data. The char A is pre-definded 
//                   and constant. state is set to false in the constructor. The count, high-guesses, 
//                   low_guesses are set to 0 initially. guesses set to empty. 
//                   These would increase when it called method
//                   encryptWordCheck.Also State might change after the method. 
//                   The alphabet was defined in the constructor as well as the 
//                   randomNum. randomNum, count, high_guesses, low_guesses change to 0 after reset 
//                   method. 
//
// Dependencies: This class has no dependencies to other class.
//
// State:     1. The state start with false when construct the object to indicate no encrypt. 
//            2. It changed to true when input word has put in to indicate encrypt happens. 
//            3. The state changed back to false when correct shift number appears to indicate
//                no encrypt happens since user has known the input word. 
//            3. The state has to be true after call the reset function to indicate encrypt process happens. 
//
//illegal input: 1. The assumption for the word less than 4 length and not all
//                  lower case letters. 
//               2. User guess shift number is more than 27 and less than 0. It also not integer
//               4. No calling setEncryptWord, but expect the program to perfom all functions. 
//               5. State is true before calling setEncryptWord. 
//
//legal input and output: When it enters legal input(which doesnt include in illegal input), 
//                       the program would expect run well. The program would display the encrypt
//                       Result when it put an  input word in. User based on that to guess. When they 
//                       guess, it would either correct or incorrect message to guide them. 




using System;
using System.Collections.Generic;

namespace p1
{
    class encryptWord
    {
        private const char A = 'a';//start letter in alphabet
        private string input_word; // the original word user inputs to create the object
        private string encryptResult; // the encryptResult 
        private bool state; //state to indicate if the word has been encrypted or not 
        private char[] alphabet = new char[26]; // create an array for store 26 alphabet letters
        private int randomNum; // randomly shift the word
        private int count; // number of quries
        private int high_guesses; //count how many time the value is exceed correct shift 
        private int low_guesses; //count how many time the value is less than correct shift 
        private List<int> guesses; // hold all guesses user have made
        private int index; // keep track on the index char within input_word string


        //description: default constructor that used to create EncryptWord object.
        //             The original state set to false to indicate no encrypt happens. 
        //             It generates a random shift number
        //             and set all count, low guesses, high guesses to 0. It also defined
        //             the alphabet for later use. 
        //precondition: none
        //postCondition: a new EncryptWord object created with random shift number and all 
        //               other counts initialized at 0.  The state set to false when creating
        //               the object indicates no encrypt happens.
        public encryptWord()
        {
            Random rnd = new Random();
            this.randomNum = rnd.Next(25) + 1;
            this.count = 0;
            this.high_guesses = 0;
            this.low_guesses = 0;
            this.state = false;
            this.guesses = new List<int>();

            for (int I = 0; I < 26; I++)
            {
                int Tmp1 = A + I;
                char Tmp2 = Convert.ToChar(Tmp1);
                alphabet[I] = Tmp2;
            }

        }

        //************this section is used to encrypt a word ******* *******************************//
        //*********************Section begins******************************************************//

        //description: The method has taken a string as parameter and set the object
        //             with the string. The method also called EncryptWordResult as
        //             a helper method to transfer the word into Encrypt version. State
        //             changed to true to indicate encrypt happens. 
        //precondition: Before the method, the orginial word and result string would 
        //              be null. The user has to satisfy the length of word has to
        //              be over 4 and has to be lower case. State is false before. 
        //postCondition: the orginial word string has taken the given string, the 
        //               result string has recorded the encrypt word string. The 
        //               shift number has to be between 0 and 27. State is true.
        public bool setEncryptWord(string word)
        {
            setOriginal(word);
            encryptResult = encryptWordResult(word);
            return encryptResult.Length != 0;
        }


        //description: the method is a helper function of setEncryptWord. It 
        //             has used the given string and encrypt it based on
        //             shfit key. The string was divided into char, and called 
        //             charCheck to see if it meets my requirement. Then it shifted 
        //             and conbimed to a new string. The state should changed from
        //             false to true.
        //precondition:  String has to be over 4 character and each char
        //               was able to find in Alphabet. Alphabet  has pre-determinated. 
        //               state is false before the method. 
        //postcondition: A encrypted string that contains all lowest letter has been
        //               created. String result can not be null. State changed to true
        //               to indicate a encryption occurs. 
        public string encryptWordResult(string word)
        {
            state = true;
            int index1 = word.Length;
            char[] a = new char[index1];
            int cur_index;

            for (int I = 0; I < word.Length; I++)
            {
                char current = word[I];
                int current2 = (int)current-97;
                if (charCheck(current) != -9999 && word.Length >=4)
                {
                    cur_index = (current2 + this.randomNum) % 26;
                    a[I] = alphabet[cur_index];
                }
                else
                {
                    state = false;
                    return "";
                }

            }
            string tmp4 = "";
            for (int I = 0; I < index1; I++)
            {
                tmp4 += a[I];
            }

            return tmp4;

        }

        //description: the method was used to check if the given char is exits in 
        //             the char array. If it is , recorded the index it has exits;
        //             otherwise, return -9999 as a default number. 
        // preconsition: there should be a char value come in when called the 
        //                method. 
        // postcondition: the index number would have returned, if not exist, -9999
        //                would return. In client code, should have a method to 
        //                handle -9999. 
        private int charCheck(char c)
        {

            int size = alphabet.Length;


            for (int I = 0; I < size; I++)
            {
                if (c == alphabet[I])
                {
                    index = I;
                    return index;
                }
            }
            return -9999;
        }
        //*********************Section ends*******************************************************//



        //************this section is used to guess the shift number*****************************//
        //*********************Section begins****************************************************//


        //description: The method was used to check if the given integer is the 
        //              correct shifting number. Then, it would print out a 
        //              sentences based on the result. If the user has succeed 
        //              guess the right result, the correct input word would 
        //              appear. State might changed. 
        //precondition: Before the method, the count/low guesses/ high guesses and 
        //              guesses doesnt change. User has to ensure that it 
        //              followed the instruction to enter the integer. guesses
        //              List may be empty.  
        //postCondition: After that, the count/low guess/ high guesses and 
        //              guesses has changed in order to do static yield. guess 
        //              value must be non-empty. State might change once user has put 
        //              the correct shift number. 
        public void EncryptWordCheck(int num)
        {
            if ((num < 27 && num > 0))
            {
                count++;
                if (this.randomNum == num)
                {
                    guesses.Add(num);
                    Console.Write("You get the right answer!  shift numer is:  "+randomNum+"\n");
                    setState();
                    Console.Write("The encrypt Result changed back to: " + getResult()
                        + "\nState is: " + checkState() + "\n");

                    string s = "";

                    int average = getAverage();

                    Console.WriteLine("\nname | total |highest|lowest| average guess value");
                    s = this.input_word + "    " +count.ToString() + "       " +high_guesses.ToString()  + "        "
                        + low_guesses.ToString() + "         " + average.ToString();
                    Console.WriteLine(s);
                }
                else if (this.randomNum > num)
                {
                    guesses.Add(num);
                    low_guesses++;
                    Console.Write("You get a low guess. Please Try again. \n");
                }
                else
                {
                    high_guesses++;
                    guesses.Add(num);
                    Console.Write("You get a high guess. Please Try again. \n");
                }
            }
            else
            {
                Console.Write("The shift number should between 0 and 27 integer");
            }
        }
        //*********************Section ends*******************************************************//



        //************this section is used to set,check, reset state*******************************//
        //*********************Section begins******************************************************//


        //description: The method was used to check the object's state.  Then, it 
        //             would return a string on the console. 
        //precondition: there should be object already created in order to check. 
        //postCondition: A string would return to indicate object's state
        public string checkState()
        {
            string a = "";
            if (state == true)
            {
                a = "on";
            }
            else
            {
                a = "off";
            }
            return a;
        }


        //description: The method was used to change the object's state to opposite
        //              side. 
        //precondition: The object should have already created prior using this 
        //               method. It's initial state woudl rather be on or off. 
        //postcondition: The state would set to an opposite value as before. 
        private void setState()
        {
            state = !state;
        }

        //description: The method was used to set the object to a new shift number.
        //             All the count, highest guess, lowest guess, list has to be 
        //             clear out. The string would transfer to a new EncryptWord 
        //             based on a new random shift number. 
        //precondition: guesses, count, high_guess, low_ guesses may not be
        //              null. 
        //postcondition: shift-number should be different, guesses List should
        //               be clear. count, high_guess and low_guess could should all 
        //               be 0. State changed to true. 
        public void reset()
        {
            //recreate random number
            int tmp = this.randomNum;

            while (tmp == this.randomNum)
            {
                Random rnd = new Random();
                tmp = rnd.Next(25) + 1;
            }

            this.randomNum = tmp;

            encryptResult = encryptWordResult(input_word);

            state = true;
            index = 0;
            count = 0;
            high_guesses = 0;
            low_guesses = 0;
            guesses.Clear();

        }
        //*********************Section ends*******************************************************//



        //*********************this Section is used to get statistic result***********************//
        //*********************Section begins****************************************************//

        //description: the method has count the total time client has called 
        //             EncryptWordCheck. It would return an interger as total guess. 
        //precondition: object has to be created prior calling this function. 
        //postcondition: The outcome should have matched the times it called 
        //               EncryptWordCheck(int). 
        public int getCount()
        {
            return count;
        }

        // description: the method has count the time client has called 
        //             EncryptWordCheck and the given value has exceed than the random Number
        //             It would return an interger as total high guess number.
        // precondition:  high_guesses should be an integer over 0. 
        // postCondition: return high_guesses int and it should match the 
        //               times it called EncryptWordCheck that given value is higher
        //               than random number. 
        public int getHighestCount()
        {
            return high_guesses;
        }


        // description: the method has count the time client has called 
        //             EncryptWordCheck and the given value has lower than the random Number
        //             It would return an interger as total low guess number.
        //precondition: low_guesses int should be an integer that might equal or
        //              above 0. 
        //postcondition: it should return low_guesses int and it should match the 
        //               times it called EncryptWordCheck that given value is lower
        //               than random number. 
        public int getLowestCount()
        {
            return low_guesses;
        }

        //description: The method was called and return the List guesses.The 
        //             List has hold all the int number which passed 
        //             EncryWordCheck method.
        //precondition: List might not be null. 
        //postCondition: it would return a List. 
        public List<int> getList()
        {
            return guesses;
        }

        //description: The method was called and return an average value from 
        //             the vector which hold all values passed EncryWordCheck 
        //             method. 
        //precondition: the object should created prior using getAverage method. 
        // postcondition: it would return an integer value to print out the result. 
        public int getAverage()
        {
            int avg = 0;
            if (getList().Count == 0)
            {
                return 0;
            }
            else
            {
                for (int I = 0; I < getCount(); I++)
                {
                    avg += getList()[I];
                }
            }
            avg = avg / getCount();
            return avg;
        }

        //*********************Section ends*******************************************************//


        //**********this Section is used to get input,encryptResult, random number****************//
        //*********************Section begins****************************************************//



        //description: The method was called and based on state return the EncryWord result or original input. 
        // precondition: object has to created prior using this method. 
        //               string result should not be null. 
        // postCondition: return a string based on state
        public string getResult()
        {
            if (state == true)
                return encryptResult;
            else return input_word; 
        }


        //description: The method was used to return the input word user has
        //             Entreated before.
        //precondition: setEncryptWord(string) has to be called before the method
        //postcondition: it would return the original word it has entered.
        public string getOriginal()
        {
            return input_word;
        }

        //description: The method was used to set the input word to the word user has 
        //             entered. 
        //precondition: the word user entered has matched the assumption, which should be
        //              longer than 4 characters and should be 26 alphabet format. 
        //postcondition: the input word matches the word user has entered. 
        private void setOriginal(string word)
        {
            this.input_word = word;
        }

        //description: The method was used to get the random number it has shifted
        //             no.
        //precondition:no
        //postcondition: The state hasnt changed after the method.
        public int getRdm()
        {
            return randomNum;
        }

        //*********************Section ends*******************************************************//

    }
}
