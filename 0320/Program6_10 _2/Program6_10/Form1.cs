using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Program6_10
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        string compChoice;
        int compScore, myScore, comandplayerDraw;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getCompChoice();
        }

        private void getCompChoice()
        {
            int n = rand.Next(1,4);
            switch (n)
            {
                case 1:
                    compChoice = "Rock";
                    break;
                case 2:
                    compChoice = "Paper";
                    break;
                case 3:
                    compChoice = "Scissors";
                    break;
            }
        }

        private void showWinner(string myChoice)
        {
            string winner = "";

            //winner = winnerDecide(myChoice);  //Call by value
            winnerDecide(myChoice, ref winner); //Call by reference

            if (winner == "Draw")
            {
                comandplayerDraw = comandplayerDraw + 1;
            }
            else if (winner == "You Win!")
            {
                myScore = myScore + 1;
            }
            else
            {
                compScore = compScore + 1;
            }

            label1.Text = "Computer:" + compChoice + " You:" + myChoice + "\n" + winner;
        }

        //private string winnerDecide(string myChoice) //Call by value
        //{
        //    string winnerwho;
        //    if (myChoice == compChoice)
        //    {
        //        winnerwho = "Draw";
        //    }
        //    else if (myChoice == "Rock" && compChoice == "Scissors")
        //    {
        //        winnerwho = "You Win!";
        //    }
        //    else if (myChoice == "Paper" && compChoice == "Rock")
        //    {
        //        winnerwho = "You Win!";
        //    }
        //    else if (myChoice == "Scissors" && compChoice == "Paper")
        //    {
        //        winnerwho = "You Win!";
        //    }
        //    else
        //    {
        //        winnerwho = "You Lost!";
        //    }
        //    return winnerwho;
        //}

        private void winnerDecide(string myChoice, ref string winner) //Call by reference
        {
            if (myChoice == compChoice)
            {
                winner = "Draw";
            }
            else if (myChoice == "Rock" && compChoice == "Scissors")
            {
                winner = "You Win!";
            }
            else if (myChoice == "Paper" && compChoice == "Rock")
            {
                winner = "You Win!";
            }
            else if (myChoice == "Scissors" && compChoice == "Paper")
            {
                winner = "You Win!";
            }
            else
            {
                winner = "You Lost!";
            }
        }          


        private void button1_Click(object sender, EventArgs e)
        {
            string myChoice = "Rock";
            showWinner(myChoice);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string myChoice = "Paper";
            showWinner(myChoice);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string myChoice = "Scissors";
            showWinner(myChoice);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getCompChoice();
            label1.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Computers Win : " + compScore +"\nYou Win : " + myScore + "\nDraw : " + comandplayerDraw);
            this.Close();
        }
    }
}
