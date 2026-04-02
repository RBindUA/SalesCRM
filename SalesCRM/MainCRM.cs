using Sales.Application.DTO;
using Sales.Application.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesCRM
{
    public partial class MainCRM : Form
    {
        private bool _isDirty = false;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDataBaseService _dbService;
        public MainCRM(ICustomerRepository customerRepository, IDataBaseService dbService)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _dbService = dbService;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            dgvOrderDetails.DataError += dgvOrderDetails_DataError;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderDetails.RowValidated += dgvOrderDetails_RowValidated;
            dgvOrderDetails.CellValueChanged += (s, e) => _isDirty = true;


        }


        private async void OrdersButton_Click(object sender, EventArgs e)
        {
            dgvCustomers.Visible = true;
            await LoadCustomerAsync();

        }
        private async Task LoadCustomerAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomersAsync();
                var customerList = customers.ToList();

                dgvCustomers.DataSource = customerList;
                dgvCustomers.Columns["Error"].Visible = false;

                if (dgvCustomers.Columns["CustomerId"] != null)
                {
                    dgvCustomers.Columns["CustomerId"].HeaderText = "ID";
                    dgvCustomers.Columns["CustomerId"].ReadOnly = true;
                    dgvCustomers.Columns["CustomerId"].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (dgvCustomers.Columns["FullName"] != null)
                    dgvCustomers.Columns["FullName"].HeaderText = "Full Name";

                if (dgvCustomers.Columns["FirstName"] != null)
                    dgvCustomers.Columns["FirstName"].HeaderText = "Name";

                if (dgvCustomers.Columns["LastName"] != null)
                    dgvCustomers.Columns["LastName"].HeaderText = "Surname";

                if (dgvCustomers.Columns["EmailAddress"] != null)
                    dgvCustomers.Columns["EmailAddress"].HeaderText = "Email";

                if (dgvCustomers.Columns["OrderCount"] != null)
                    dgvCustomers.Columns["OrderCount"].HeaderText = "Order number";

                if (dgvCustomers.Columns["OrderIds"] != null)
                    dgvCustomers.Columns["OrderIds"].HeaderText = "Order IDs";

                this.Text = $"Clients loaded: {customerList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load ERROR: {ex.Message}");
            }
        }
        private async void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow?.DataBoundItem is CustomerDto selectedCustomer)
            {
                try
                {
                    var details = await _customerRepository.GetCustomerOrderDetailsAsync(selectedCustomer.CustomerId);

                    dgvOrderDetails.DataSource = details.ToList();

                    //Call formatting helper
                    ConfigureOrderDetailsColumns();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Detail Load Error: {ex.Message}");
                }
            }
        }

        private void dgvOrderDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Invalid input format. Please check your data.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var details = dgvOrderDetails.DataSource as List<CustomerOrderDetailDto>;

            if (details != null)
            {
                // Validation check
                bool hasErrors = details.Any(d =>
                    !string.IsNullOrEmpty(d[nameof(d.ProductName)]) ||
                    !string.IsNullOrEmpty(d[nameof(d.Quantity)]) ||
                    !string.IsNullOrEmpty(d[nameof(d.LineTotal)])
                );

                if (hasErrors)
                {
                    MessageBox.Show("Please fix the validation errors before saving.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                await _customerRepository.SaveChangesAsync();
                MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private async void dgvOrderDetails_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Get the specific row that was just edited
            if (dgvOrderDetails.Rows[e.RowIndex].DataBoundItem is CustomerOrderDetailDto editedItem)
            {
                bool hasErrors = !string.IsNullOrEmpty(editedItem[nameof(editedItem.Quantity)]) ||
                         !string.IsNullOrEmpty(editedItem[nameof(editedItem.ProductName)]);

                if (hasErrors) return;
                try
                {
                    await _customerRepository.UpdateOrderDetailsAsync(editedItem);
                    await _customerRepository.SaveChangesAsync();
                    _isDirty = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Auto-save failed: {ex.Message}");
                }
            }
        }

        private void ConfigureOrderDetailsColumns()
        {
            if (dgvOrderDetails.Columns.Count == 0) return;

            // Technical columns NOT visible
            if (dgvOrderDetails.Columns["SalesOrderDetailId"] != null)
                dgvOrderDetails.Columns["SalesOrderDetailId"].Visible = false;

            if (dgvOrderDetails.Columns["Error"] != null)
                dgvOrderDetails.Columns["Error"].Visible = false;

            // Visual Formatting
            if (dgvOrderDetails.Columns["LineTotal"] != null)
                dgvOrderDetails.Columns["LineTotal"].DefaultCellStyle.Format = "C2";

            if (dgvOrderDetails.Columns["ProductName"] != null)
                dgvOrderDetails.Columns["ProductName"].HeaderText = "Product Name";

            dgvOrderDetails.ReadOnly = false;

            // Protect IDs
            if (dgvOrderDetails.Columns["SalesOrderId"] != null)
            {
                dgvOrderDetails.Columns["SalesOrderId"].ReadOnly = true;
                dgvOrderDetails.Columns["SalesOrderId"].DefaultCellStyle.BackColor = Color.LightGray;
            }

            if (dgvOrderDetails.Columns["ProductId"] != null)
            {
                dgvOrderDetails.Columns["ProductId"].ReadOnly = true;
                dgvOrderDetails.Columns["ProductId"].DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private async void btnTestMigration_Click(object sender, EventArgs e)
        {
            btnTestMigration.Enabled = false;

            try
            {
                int countM = await _dbService.GetPendingMigrationsCountAsync();
                if (countM == 0)
                {
                    MessageBox.Show("Database is up to date.", "Migration test",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var res = MessageBox.Show($"Found {countM} pending updates. Apply them now?",
                        "Migration Test", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        await _dbService.MigrateAsync();
                        MessageBox.Show("Migration applied now.");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Migration test failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTestMigration.Enabled = true;
            }
        }

        private async void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.CurrentRow?.DataBoundItem is CustomerOrderDetailDto selectedOrder)
            {
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete {selectedOrder.ProductName}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        await _customerRepository.DeleteOrderAsync(selectedOrder.SalesOrderDetailId);


                        await _customerRepository.SaveChangesAsync();

                        if (dgvCustomers.CurrentRow?.DataBoundItem is CustomerDto selectedCustomer)
                        {
                            var details = await _customerRepository.GetCustomerOrderDetailsAsync(selectedCustomer.CustomerId);
                            dgvOrderDetails.DataSource = details.ToList();
                        }

                        MessageBox.Show("Order deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order to delete.");
            }
        }

    }
}