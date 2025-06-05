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
        private List<TeamData> teamDataList = new List<TeamData>();

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
            BuildTeamDataList();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            string selectedTeam = listBox1.SelectedItem.ToString();
            var team = teamDataList.FirstOrDefault(t => t.Name == selectedTeam);

            if (team == null)
                return;


            string result = $"🏆 {team.Name} 共獲得 {team.WinCount} 次冠軍。\n得冠年份：\n{string.Join("、", team.WinYears)}";
            label1.Text = result;
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

                    // 加入年份給對應隊伍
                    TeamData data = teamDataList.FirstOrDefault(t => t.Name == team);
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
                        listBox1.Items.Add(team); // UI 也更新
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
            teamDataList.Clear(); // 清空現有資料
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
                    teamList.Add(winner);
                    if (!listBox1.Items.Contains(winner))
                        listBox1.Items.Add(winner); // 防止重複新增
                }

                year++;
            }
        }

    }
}


    