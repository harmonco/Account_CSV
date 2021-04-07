using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace AccountCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"C:\Users\cjhar\source\repos\AccountCSV\Accounts.txt", FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                string content = sr.ReadToEnd();
                //split into seperate lines
                string[] lines = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in lines)
                {
                    //write data in file
                    Console.WriteLine(s);
                }

                int lineCount = 0;
                List<Account> accounts = new List<Account>();
                foreach (string line in lines)
                {
                    //split by commas
                    string[] column = line.Split(',');
                    //disregard first line
                    if (lineCount != 0)
                    {
                        Account account = new Account();
                        account.AccountName = column[0];
                        account.InvoiceDate = column[1];
                        account.DueDate = column[2];
                        account.AmountDue = column[3];
                        accounts.Add(account);

                    }
                    lineCount++;
                }
            }
            //ask if they want to add more data
            Console.WriteLine("Would you like to enter another account? [y/n] then press enter");

            string click = Console.ReadLine();

            if (click == "y")
            {
                //call method to add new account
                NewAccount.CreateNewAccount();
            }
            else
            {
                Console.WriteLine("You may close the program");
            }
        }
    }
    public class Account
    {
        public string AccountName { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public string AmountDue { get; set; }
    }
    public class Accountshelper
    {
        public static string GetCSV(Account account)
        {
            string returnValue = account.AccountName + "," + account.InvoiceDate + "," + account.DueDate + "," + account.AmountDue;
            return returnValue;
        }
    }

    public class NewAccount
    {
        public static string CreateNewAccount() 
        {
            string returnvalue = "Your data has been saved.";
            //instantiate new account
            Account newAccount1 = new Account();
            //prompt user for input about account
            Console.WriteLine("Please enter the account name:");
            newAccount1.AccountName = Console.ReadLine();
            Console.WriteLine("Please enter the Invoice Date:");
            string date1 = Console.ReadLine();
            DateTime correctdate1;
            //tryparse to make sure they enter a correct date format
            while (!DateTime.TryParse(date1, out correctdate1))
            {
                Console.WriteLine("This is not a valid date. Please enter again");
                date1 = Console.ReadLine();
            }
            string correctdatestring1 = correctdate1.ToString();
            newAccount1.InvoiceDate = correctdatestring1;
            Console.WriteLine("Please enter the Due Date:");
            string date2 = Console.ReadLine();
            DateTime correctdate2;
            //tryparse to make sure they enter a correct date format
            while (!DateTime.TryParse(date2, out correctdate2))
            {
                Console.WriteLine("This is not a valid date. Please enter again");
                date2 = Console.ReadLine();
            }
            string correctdatestring2 = correctdate2.ToString();
            newAccount1.DueDate = correctdatestring2;
            Console.WriteLine("Please enter the Amount Due:");
            string amntdue = Console.ReadLine();
            decimal correctamountdue;
            //tryparse to make sure they enter a correct decimal amount
            while (!Decimal.TryParse(amntdue, out correctamountdue))
            {
                Console.WriteLine("This is not a valid date. Please enter again");
                amntdue = Console.ReadLine();
            }
            string correctamountduestring = correctamountdue.ToString();
            newAccount1.AmountDue = correctamountduestring;
            //open filestream and new streamwrite to write new data to csv file
            using (FileStream fs = new FileStream(@"C:\Users\cjhar\source\repos\AccountCSV\Accounts.txt", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {

                // write the new account to the csv file
                sw.WriteLine(Accountshelper.GetCSV(newAccount1));
                
            }
            return returnvalue;

        }
    }
}






