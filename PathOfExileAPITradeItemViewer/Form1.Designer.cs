namespace PathOfExileAPITradeItemViewer
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewStash = new System.Windows.Forms.TreeView();
            this.treeViewProperties = new System.Windows.Forms.TreeView();
            this.treeViewItems = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentChangeID = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnLastChangeID = new System.Windows.Forms.Button();
            this.btnNextChangeID = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewStash
            // 
            this.treeViewStash.Location = new System.Drawing.Point(12, 12);
            this.treeViewStash.Name = "treeViewStash";
            this.treeViewStash.Size = new System.Drawing.Size(135, 313);
            this.treeViewStash.TabIndex = 0;
            this.treeViewStash.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewStash_NodeMouseClick);
            // 
            // treeViewProperties
            // 
            this.treeViewProperties.Location = new System.Drawing.Point(153, 141);
            this.treeViewProperties.Name = "treeViewProperties";
            this.treeViewProperties.Size = new System.Drawing.Size(176, 97);
            this.treeViewProperties.TabIndex = 1;
            // 
            // treeViewItems
            // 
            this.treeViewItems.Location = new System.Drawing.Point(153, 244);
            this.treeViewItems.Name = "treeViewItems";
            this.treeViewItems.Size = new System.Drawing.Size(176, 97);
            this.treeViewItems.TabIndex = 2;
            this.treeViewItems.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewItems_NodeMouseClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel,
            this.currentChangeID});
            this.statusStrip1.Location = new System.Drawing.Point(0, 357);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(341, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(62, 17);
            this.statusStripLabel.Text = "statusStrip";
            // 
            // currentChangeID
            // 
            this.currentChangeID.Name = "currentChangeID";
            this.currentChangeID.Size = new System.Drawing.Size(92, 17);
            this.currentChangeID.Text = "Next Change ID:";
            // 
            // btnLastChangeID
            // 
            this.btnLastChangeID.Location = new System.Drawing.Point(12, 331);
            this.btnLastChangeID.Name = "btnLastChangeID";
            this.btnLastChangeID.Size = new System.Drawing.Size(62, 23);
            this.btnLastChangeID.TabIndex = 4;
            this.btnLastChangeID.Text = "<";
            this.btnLastChangeID.UseVisualStyleBackColor = true;
            this.btnLastChangeID.Click += new System.EventHandler(this.btnLastChangeID_Click);
            // 
            // btnNextChangeID
            // 
            this.btnNextChangeID.Location = new System.Drawing.Point(85, 331);
            this.btnNextChangeID.Name = "btnNextChangeID";
            this.btnNextChangeID.Size = new System.Drawing.Size(62, 23);
            this.btnNextChangeID.TabIndex = 5;
            this.btnNextChangeID.Text = ">";
            this.btnNextChangeID.UseVisualStyleBackColor = true;
            this.btnNextChangeID.Click += new System.EventHandler(this.btnNextChangeID_Click);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(153, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(176, 123);
            this.picBox.TabIndex = 6;
            this.picBox.TabStop = false;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 379);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.btnNextChangeID);
            this.Controls.Add(this.btnLastChangeID);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.treeViewItems);
            this.Controls.Add(this.treeViewProperties);
            this.Controls.Add(this.treeViewStash);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewStash;
        private System.Windows.Forms.TreeView treeViewProperties;
        private System.Windows.Forms.TreeView treeViewItems;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel currentChangeID;
        private System.Windows.Forms.Button btnLastChangeID;
        private System.Windows.Forms.Button btnNextChangeID;
        private System.Windows.Forms.PictureBox picBox;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}

