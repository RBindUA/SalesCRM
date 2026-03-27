using System.Linq.Expressions;
using System.Threading.Tasks;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;

namespace SalesCRM
{
    public partial class Form1 : Form
    {

        private readonly ICustomerRepository _customerRepository;
        public Form1(ICustomerRepository customerRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
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
                    dgvCustomers.Columns["CustomerId"].HeaderText = "ID";

                if (dgvCustomers.Columns["FirstName"] != null)
                    dgvCustomers.Columns["FirstName"].HeaderText = "Name";

                if (dgvCustomers.Columns["LastName"] != null)
                    dgvCustomers.Columns["LastName"].HeaderText = "Surname";

                if (dgvCustomers.Columns["EmailAddress"] != null)
                    dgvCustomers.Columns["EmailAddress"].HeaderText = "Email";

                if (dgvCustomers.Columns["OrdersList"] != null)
                {
                    dgvCustomers.Columns["OrdersList"].HeaderText = "Numbers of orders";
                    dgvCustomers.Columns["OrdersList"].Width = 200;
                }

                this.Text = $"Clients loaded: {customerList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }
    }
}
