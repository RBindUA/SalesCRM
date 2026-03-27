namespace SalesCRM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvCustomers = new DataGridView();
            OrdersButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(209, 12);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.Size = new Size(650, 462);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.Visible = false;
            // 
            // OrdersButton
            // 
            OrdersButton.Location = new Point(12, 12);
            OrdersButton.Name = "OrdersButton";
            OrdersButton.Size = new Size(112, 41);
            OrdersButton.TabIndex = 1;
            OrdersButton.Text = "List orders";
            OrdersButton.TextImageRelation = TextImageRelation.ImageAboveText;
            OrdersButton.UseVisualStyleBackColor = true;
            OrdersButton.Click += OrdersButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 490);
            Controls.Add(OrdersButton);
            Controls.Add(dgvCustomers);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCustomers;
        private Button OrdersButton;
    }
}
