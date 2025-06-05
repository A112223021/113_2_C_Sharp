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
        private List<string> winnerList = new List<string>();          // 所有冠軍年份球隊
        private List<TeamData> teamDataList = new List<TeamData>();    // 記錄統計結果
        private List<string> teamList = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        public class TeamData
        {
            public string Name;
            public int WinCount;
            public List<int> WinYears;

            public TeamData(string name)
            {
                Name = name;
                WinCount = 0;
                WinYears = new List<int>();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("請選擇 WorldSeries_Chinese.txt 資料檔案", "載入冠軍紀錄");

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "文字檔案 (*.txt)|*.txt";
            openFile.Title = "選擇冠軍資料";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                winnerList = File.ReadAllLines(openFile.FileName).ToList();
                BuildTeamDataList(); // 建立 teamDataList 結構
                listBox1.Items.AddRange(teamDataList.Select(t => t.Name).ToArray()); // 將球隊名稱放入清單
            }
            else
            {
                MessageBox.Show("未選擇檔案，程式關閉");
                this.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            string selectedTeam = listBox1.SelectedItem.ToString();
            TeamData team = teamDataList.FirstOrDefault(t => t.Name == selectedTeam);

            if (team == null)
                return;

            string message = $"🏆 {team.Name} 共獲得 {team.WinCount} 次世界大賽冠軍\n年份：\n{string.Join("、", team.WinYears)}";
            label1.Text = message;
        }
        private void btnAddData_Click(object sender, EventArgs e)
        {
            MessageBox.Show("請選擇包含 2010 年以後世界大賽冠軍的資料檔案。", "新增資料", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "選擇新冠軍資料檔案";
            openFile.Filter = "文字檔案 (*.txt)|*.txt";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                List<string> newWinners = File.ReadAllLines(openFile.FileName).ToList();
                int currentYear = 2010;

                foreach (string team in newWinners)
                {
                    winnerList.Add(team);

                    int index = teamDataList.FindIndex(t => t.Name == team);
                    if (index != -1)
                    {
                        teamDataList[index].WinCount++;
                        teamDataList[index].WinYears.Add(currentYear);
                    }
                    else
                    {
                        TeamData newTeam = new TeamData(team);
                        newTeam.WinCount = 1;
                        newTeam.WinYears.Add(currentYear);
                        teamDataList.Add(newTeam);
                        teamList.Add(team);
                        if (!listBox1.Items.Contains(team))
                        {
                            listBox1.Items.Add(team); // UI 也更新
                        }
                    }

                    currentYear++;
                }

                MessageBox.Show("資料已成功新增並更新至畫面與記憶體中。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllLines("MLB_Teams_Translated.txt", teamList);
                File.WriteAllLines("WorldSeries_Chinese.txt", winnerList);
                MessageBox.Show("資料已成功儲存。", "結束", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存檔案時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }
        private void BuildTeamDataList()
        {
            teamDataList.Clear();
            int year = 1903;

            foreach (string winner in winnerList)
            {
                if (year == 1904 || year == 1994)
                {
                    year++;
                    continue;
                }

                int index = teamDataList.FindIndex(t => t.Name == winner);

                if (index != -1)
                {
                    teamDataList[index].WinCount++;
                    teamDataList[index].WinYears.Add(year);
                }
                else
                {
                    TeamData newTeam = new TeamData(winner);
                    newTeam.WinCount = 1;
                    newTeam.WinYears.Add(year);
                    teamDataList.Add(newTeam);
                }

                year++;
            }
        }

    }
}


    