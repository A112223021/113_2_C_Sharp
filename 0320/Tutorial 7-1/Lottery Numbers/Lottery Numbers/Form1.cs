using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lottery_Numbers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            const int SIZE =5; // The size of the array.
            int[] lotteryNumbers = new int[SIZE]; // Array to hold the lottery numbers.
            Random rand = new Random(); // Random object.

            for(int i = 0; i < lotteryNumbers.Length;i++)
            {
                lotteryNumbers[i] = rand.Next(1, 43); // Get a random number between 1 and 42.
            }

            //fifthLabel.Text = lotteryNumbers[0].ToString();
            //fourthLabel.Text = lotteryNumbers[1].ToString();
            //thirdLabel.Text = lotteryNumbers[2].ToString();
            //secondLabel.Text = lotteryNumbers[3].ToString();
            //firstLabel.Text = lotteryNumbers[4].ToString();

            Label[] showlabels = { fifthLabel, fourthLabel, thirdLabel, secondLabel, firstLabel };
            for (int i = 0; i < lotteryNumbers.Length; i++)
            {
                showlabels[i].Text = lotteryNumbers[i].ToString();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
