namespace SOS.Samples.ChatClient
{
	partial class Form1
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
			this.label1 = new System.Windows.Forms.Label();
			this.chatNameTb = new System.Windows.Forms.TextBox();
			this.outputTb = new System.Windows.Forms.TextBox();
			this.messageToSendTb = new System.Windows.Forms.TextBox();
			this.connectToServerBtn = new System.Windows.Forms.Button();
			this.sendMessageBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter your chat name:";
			// 
			// chatNameTb
			// 
			this.chatNameTb.Location = new System.Drawing.Point(132, 13);
			this.chatNameTb.Name = "chatNameTb";
			this.chatNameTb.Size = new System.Drawing.Size(133, 20);
			this.chatNameTb.TabIndex = 1;
			// 
			// outputTb
			// 
			this.outputTb.Location = new System.Drawing.Point(18, 74);
			this.outputTb.Multiline = true;
			this.outputTb.Name = "outputTb";
			this.outputTb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.outputTb.Size = new System.Drawing.Size(247, 136);
			this.outputTb.TabIndex = 3;
			// 
			// messageToSendTb
			// 
			this.messageToSendTb.Location = new System.Drawing.Point(18, 216);
			this.messageToSendTb.Name = "messageToSendTb";
			this.messageToSendTb.Size = new System.Drawing.Size(247, 20);
			this.messageToSendTb.TabIndex = 4;
			// 
			// connectToServerBtn
			// 
			this.connectToServerBtn.Location = new System.Drawing.Point(132, 39);
			this.connectToServerBtn.Name = "connectToServerBtn";
			this.connectToServerBtn.Size = new System.Drawing.Size(133, 23);
			this.connectToServerBtn.TabIndex = 2;
			this.connectToServerBtn.Text = "Connect to Server";
			this.connectToServerBtn.UseVisualStyleBackColor = true;
			this.connectToServerBtn.Click += new System.EventHandler(this.ConnectToServerBtnClick);
			// 
			// sendMessageBtn
			// 
			this.sendMessageBtn.Location = new System.Drawing.Point(132, 242);
			this.sendMessageBtn.Name = "sendMessageBtn";
			this.sendMessageBtn.Size = new System.Drawing.Size(133, 23);
			this.sendMessageBtn.TabIndex = 5;
			this.sendMessageBtn.Text = "Send Message";
			this.sendMessageBtn.UseVisualStyleBackColor = true;
			this.sendMessageBtn.Click += new System.EventHandler(this.SendMessageBtnClick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(285, 304);
			this.Controls.Add(this.sendMessageBtn);
			this.Controls.Add(this.connectToServerBtn);
			this.Controls.Add(this.messageToSendTb);
			this.Controls.Add(this.outputTb);
			this.Controls.Add(this.chatNameTb);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox chatNameTb;
		private System.Windows.Forms.TextBox outputTb;
		private System.Windows.Forms.TextBox messageToSendTb;
		private System.Windows.Forms.Button connectToServerBtn;
		private System.Windows.Forms.Button sendMessageBtn;
	}
}

