using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Program7_5
{
    public partial class Form1 : Form
    {
        private List<string> teamList = new List<string>();
        private List<string> winnerList = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        string[] team;
        string[] winner;

        private void Form1_Load(object sender, EventArgs e)
        {
            // 提示使用者將分別選擇球隊與冠軍資料檔
            MessageBox.Show("請先選擇球隊清單檔案（例如：MLB_Teams_Translated.txt）。",
                            "選擇球隊檔案", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OpenFileDialog openTeamFile = new OpenFileDialog();
            openTeamFile.Title = "選擇球隊清單檔案";
            openTeamFile.Filter = "文字檔案 (*.txt)|*.txt";

            if (openTeamFile.ShowDialog() == DialogResult.OK)
            {
                teamList = File.ReadAllLines(openTeamFile.FileName).ToList();
                listBox1.Items.AddRange(teamList.ToArray());
            }
            else
            {
                MessageBox.Show("未選擇球隊檔案，程式將關閉。");
                this.Close();
                return;
            }

            // 提示使用者選擇冠軍紀錄檔案
            MessageBox.Show("接著請選擇世界大賽冠軍紀錄檔案（例如：WorldSeries_Chinese.txt）。",
                            "選擇冠軍紀錄檔案", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OpenFileDialog openWinnerFile = new OpenFileDialog();
            openWinnerFile.Title = "選擇世界大賽冠軍紀錄檔案";
            openWinnerFile.Filter = "文字檔案 (*.txt)|*.txt";

            if (openWinnerFile.ShowDialog() == DialogResult.OK)
            {
                winnerList = File.ReadAllLines(openWinnerFile.FileName).ToList();
            }
            else
            {
                MessageBox.Show("未選擇冠軍紀錄檔案，程式將關閉。");
                this.Close();
                return;
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            string selectedTeam = listBox1.SelectedItem.ToString();
            List<int> winYears = new List<int>();
            int year = 1903;

            foreach (string winner in winnerList)
            {
                if (year == 1904 || year == 1994)
                {
                    year++; // 跳過未舉辦年份
                    continue;
                }

                if (winner == selectedTeam)
                {
                    winYears.Add(year);
                }

                year++;
            }
            int count = winYears.Count;
            string result = $"{selectedTeam} 自 1903～2009 年共奪冠 {count} 次。\n得冠年份：\n{string.Join("、", winYears)}";
            label1.Text = result;
        }
    }
}


    