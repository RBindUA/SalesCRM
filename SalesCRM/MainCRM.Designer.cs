namespace SalesCRM
{
    partial class MainCRM
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
            btnSave = new Button();
            btnTestMigration = new Button();
            btnDeleteOrder = new Button();
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
            // btnSave
            // 
            btnSave.Location = new Point(12, 72);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 41);
            btnSave.TabIndex = 3;
            btnSave.Text = "Change quantity";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnTestMigration
            // 
            btnTestMigration.Location = new Point(12, 442);
            btnTestMigration.Name = "btnTestMigration";
            btnTestMigration.Size = new Size(112, 36);
            btnTestMigration.TabIndex = 4;
            btnTestMigration.Text = "MigrationCheck";
            btnTestMigration.UseVisualStyleBackColor = true;
            btnTestMigration.Click += btnTestMigration_Click;
            // 
            // btnDeleteOrder
            // 
            btnDeleteOrder.Location = new Point(12, 133);
            btnDeleteOrder.Name = "btnDeleteOrder";
            btnDeleteOrder.Size = new Size(112, 39);
            btnDeleteOrder.TabIndex = 5;
            btnDeleteOrder.Text = "Delete order";
            btnDeleteOrder.UseVisualStyleBackColor = true;
            btnDeleteOrder.Click += btnDeleteOrder_Click;
            // 
            // MainCRM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 490);
            Controls.Add(btnDeleteOrder);
            Controls.Add(btnTestMigration);
            Controls.Add(btnSave);
            Controls.Add(dgvOrderDetails);
            Controls.Add(OrdersButton);
            Controls.Add(dgvCustomers);
            Name = "MainCRM";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCustomers;
        private Button OrdersButton;
        private DataGridView dgvOrderDetails;
        private Button btnSave;
        private Button btnTestMigration;
        private Button btnDeleteOrder;
    }
}
