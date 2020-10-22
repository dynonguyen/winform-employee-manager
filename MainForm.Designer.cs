namespace _2020_Nhom16_TH1_18120634
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.mainTabControl = new System.Windows.Forms.TabControl();
      this.tabList = new System.Windows.Forms.TabPage();
      this.panel3 = new System.Windows.Forms.Panel();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tabUpdate = new System.Windows.Forms.TabPage();
      this.tabSearch = new System.Windows.Forms.TabPage();
      this.tabstatistic = new System.Windows.Forms.TabPage();
      this.mainTabControl.SuspendLayout();
      this.tabList.SuspendLayout();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainTabControl
      // 
      this.mainTabControl.Controls.Add(this.tabList);
      this.mainTabControl.Controls.Add(this.tabUpdate);
      this.mainTabControl.Controls.Add(this.tabSearch);
      this.mainTabControl.Controls.Add(this.tabstatistic);
      this.mainTabControl.Location = new System.Drawing.Point(12, 13);
      this.mainTabControl.Name = "mainTabControl";
      this.mainTabControl.SelectedIndex = 0;
      this.mainTabControl.Size = new System.Drawing.Size(920, 576);
      this.mainTabControl.TabIndex = 0;
      // 
      // tabList
      // 
      this.tabList.Controls.Add(this.panel3);
      this.tabList.Controls.Add(this.panel2);
      this.tabList.Location = new System.Drawing.Point(4, 22);
      this.tabList.Name = "tabList";
      this.tabList.Padding = new System.Windows.Forms.Padding(3);
      this.tabList.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.tabList.Size = new System.Drawing.Size(912, 550);
      this.tabList.TabIndex = 0;
      this.tabList.Text = "Danh Sách";
      this.tabList.UseVisualStyleBackColor = true;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.comboBox1);
      this.panel3.Controls.Add(this.label1);
      this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.panel3.Location = new System.Drawing.Point(6, 6);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(320, 40);
      this.panel3.TabIndex = 2;
      // 
      // comboBox1
      // 
      this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.comboBox1.DropDownHeight = 100;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.IntegralHeight = false;
      this.comboBox1.Items.AddRange(new object[] {
            "Nhân Viên",
            "Lịch Sử Trả Lương",
            "Phòng Ban",
            "Phòng"});
      this.comboBox1.Location = new System.Drawing.Point(117, 10);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(192, 24);
      this.comboBox1.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(99, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Loại danh sách";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panel2
      // 
      this.panel2.Location = new System.Drawing.Point(6, 52);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(900, 492);
      this.panel2.TabIndex = 1;
      // 
      // tabUpdate
      // 
      this.tabUpdate.Location = new System.Drawing.Point(4, 22);
      this.tabUpdate.Name = "tabUpdate";
      this.tabUpdate.Padding = new System.Windows.Forms.Padding(3);
      this.tabUpdate.Size = new System.Drawing.Size(912, 550);
      this.tabUpdate.TabIndex = 1;
      this.tabUpdate.Text = "Cập Nhật";
      this.tabUpdate.UseVisualStyleBackColor = true;
      // 
      // tabSearch
      // 
      this.tabSearch.Location = new System.Drawing.Point(4, 22);
      this.tabSearch.Name = "tabSearch";
      this.tabSearch.Padding = new System.Windows.Forms.Padding(3);
      this.tabSearch.Size = new System.Drawing.Size(912, 550);
      this.tabSearch.TabIndex = 2;
      this.tabSearch.Text = "Tìm Kiếm";
      this.tabSearch.UseVisualStyleBackColor = true;
      // 
      // tabstatistic
      // 
      this.tabstatistic.Location = new System.Drawing.Point(4, 22);
      this.tabstatistic.Name = "tabstatistic";
      this.tabstatistic.Padding = new System.Windows.Forms.Padding(3);
      this.tabstatistic.Size = new System.Drawing.Size(912, 550);
      this.tabstatistic.TabIndex = 3;
      this.tabstatistic.Text = "Thống Kê";
      this.tabstatistic.UseVisualStyleBackColor = true;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(944, 601);
      this.Controls.Add(this.mainTabControl);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Employee Manager";
      this.mainTabControl.ResumeLayout(false);
      this.tabList.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl mainTabControl;
    private System.Windows.Forms.TabPage tabList;
    private System.Windows.Forms.TabPage tabUpdate;
    private System.Windows.Forms.TabPage tabSearch;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.TabPage tabstatistic;
  }
}

