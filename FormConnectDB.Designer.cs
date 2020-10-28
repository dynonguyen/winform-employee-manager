namespace _2020_Nhom16_TH1_18120634
{
  partial class FormConnectDB
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnectDB));
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tbxServerName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.panel4 = new System.Windows.Forms.Panel();
      this.btnConnect = new System.Windows.Forms.Button();
      this.btnExit = new System.Windows.Forms.Button();
      this.panel3 = new System.Windows.Forms.Panel();
      this.txbDBName = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Location = new System.Drawing.Point(58, 21);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(430, 51);
      this.panel1.TabIndex = 5;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(68, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(297, 25);
      this.label1.TabIndex = 1;
      this.label1.Text = "Kết Nối Đến Cơ Sở Dữ Liệu";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.tbxServerName);
      this.panel2.Controls.Add(this.label2);
      this.panel2.Location = new System.Drawing.Point(12, 87);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(510, 62);
      this.panel2.TabIndex = 0;
      // 
      // tbxServerName
      // 
      this.tbxServerName.Location = new System.Drawing.Point(117, 20);
      this.tbxServerName.MaxLength = 500;
      this.tbxServerName.Multiline = true;
      this.tbxServerName.Name = "tbxServerName";
      this.tbxServerName.Size = new System.Drawing.Size(390, 26);
      this.tbxServerName.TabIndex = 0;
      this.tbxServerName.Text = ".";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(3, 21);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(88, 16);
      this.label2.TabIndex = 0;
      this.label2.Text = "Server name:";
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.btnConnect);
      this.panel4.Controls.Add(this.btnExit);
      this.panel4.Location = new System.Drawing.Point(92, 227);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(430, 62);
      this.panel4.TabIndex = 2;
      // 
      // btnConnect
      // 
      this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.btnConnect.Location = new System.Drawing.Point(197, 14);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(103, 34);
      this.btnConnect.TabIndex = 2;
      this.btnConnect.Text = "Kết Nối";
      this.btnConnect.UseVisualStyleBackColor = false;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // btnExit
      // 
      this.btnExit.BackColor = System.Drawing.SystemColors.Control;
      this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnExit.Location = new System.Drawing.Point(324, 14);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(103, 34);
      this.btnExit.TabIndex = 3;
      this.btnExit.Text = "Thoát";
      this.btnExit.UseVisualStyleBackColor = false;
      this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.txbDBName);
      this.panel3.Controls.Add(this.label3);
      this.panel3.Location = new System.Drawing.Point(12, 155);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(510, 66);
      this.panel3.TabIndex = 1;
      // 
      // txbDBName
      // 
      this.txbDBName.Location = new System.Drawing.Point(117, 21);
      this.txbDBName.MaxLength = 500;
      this.txbDBName.Multiline = true;
      this.txbDBName.Name = "txbDBName";
      this.txbDBName.Size = new System.Drawing.Size(390, 26);
      this.txbDBName.TabIndex = 1;
      this.txbDBName.Text = "EmployeeMng2012";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(3, 21);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(108, 16);
      this.label3.TabIndex = 0;
      this.label3.Text = "Database name:";
      // 
      // FormConnectDB
      // 
      this.AcceptButton = this.btnConnect;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnExit;
      this.ClientSize = new System.Drawing.Size(534, 311);
      this.Controls.Add(this.panel4);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormConnectDB";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Kết Nối DataBase";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TextBox tbxServerName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox txbDBName;
    private System.Windows.Forms.Label label3;
  }
}