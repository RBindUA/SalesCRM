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
            dgvOrderDetails = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).BeginInit();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(130, 12);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.Size = new Size(729, 220);
            dgvCustomers.TabIndex = 0;
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
            // dgvOrderDetails
            // 
            dgvOrderDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderDetails.Location = new Point(130, 238);
            dgvOrderDetails.Name = "dgvOrderDetails";
            dgvOrderDetails.Size = new Size(729, 240);
            dgvOrderDetails.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 490);
            Controls.Add(dgvOrderDetails);
            Controls.Add(OrdersButton);
            Controls.Add(dgvCustomers);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCustomers;
        private Button OrdersButton;
        private DataGridView dgvOrderDetails;
    }
}
