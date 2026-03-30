using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesCRM
{
    public partial class MainCRM : Form
    {
        private bool _isDirty = false;
        private readonly ICustomerRepository _customerRepository;
        public MainCRM(ICustomerRepository customerRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
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

                if (dgvCustomers.Columns["CustomerId"] != null)
                {
                    dgvCustomers.Columns["CustomerId"].HeaderText = "ID";
                    dgvCustomers.Columns["CustomerId"].ReadOnly = true;
                    dgvCustomers.Columns["CustomerId"].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (dgvCustomers.Columns["FirstName"] != null)
                    dgvCustomers.Columns["FirstName"].HeaderText = "Name";

                if (dgvCustomers.Columns["LastName"] != null)
                    dgvCustomers.Columns["LastName"].HeaderText = "Surname";

                if (dgvCustomers.Columns["EmailAddress"] != null)
                    dgvCustomers.Columns["EmailAddress"].HeaderText = "Email";
                if (dgvCustomers.Columns["OrdersList"] != null)
                {
                    dgvCustomers.Columns["OrdersList"].HeaderText = "Numbers of orders";
                }

                if (dgvCustomers.Columns["PersonId"] != null)
                    dgvCustomers.Columns["PersonId"].Visible = false;

                if (dgvCustomers.Columns["Orders"] != null)
                    dgvCustomers.Columns["Orders"].Visible = false;



                this.Text = $"Clients loaded: {customerList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load ERROR: {ex.Message}");
            }
        }
        private async void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow?.DataBoundItem is Customer selectedCustomer)
            {
                try
                {
                    // 1. Await the data properly here
                    var details = await _customerRepository.GetCustomerOrderDetailsAsync(selectedCustomer.CustomerId);

                    // 2. Set the DataSource
                    dgvOrderDetails.DataSource = details.ToList();

                    // 3. Call the formatting method
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
            var details = dgvOrderDetails.DataSource as List<CustomerOrderDetail>;

            if (details != null)
            {
                // Valid check
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
            if (dgvOrderDetails.Rows[e.RowIndex].DataBoundItem is CustomerOrderDetail editedItem)
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
                Console.WriteLine($"Auto-save failed: { ex.Message}");
                }
            }
        }

        private void ConfigureOrderDetailsColumns()
        {
            // If the grid has no columns yet, exit to avoid errors
            if (dgvOrderDetails.Columns.Count == 0) return;

            // 1. Technical columns NOT visible
            if (dgvOrderDetails.Columns["SalesOrderDetailId"] != null)
                dgvOrderDetails.Columns["SalesOrderDetailId"].Visible = false;

            if (dgvOrderDetails.Columns["Error"] != null)
                dgvOrderDetails.Columns["Error"].Visible = false;

            // 2. Visual Formatting
            if (dgvOrderDetails.Columns["LineTotal"] != null)
                dgvOrderDetails.Columns["LineTotal"].DefaultCellStyle.Format = "C2";

            if (dgvOrderDetails.Columns["ProductName"] != null)
                dgvOrderDetails.Columns["ProductName"].HeaderText = "Product Name";

            dgvOrderDetails.ReadOnly = false;

            // 3. Protect IDs
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

    }
}