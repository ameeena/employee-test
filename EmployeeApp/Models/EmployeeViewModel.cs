namespace EmployeeApp.Models
{
    /// <summary>
    /// Employee View Model
    /// </summary>
    public class EmployeeViewModel
    {
        /// <summary>
        /// Id for Employee
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        ///  First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Designation
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// Department 
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Phone number
        /// </summary>
        public long PhoneNumber { get; set; }
        /// <summary>
        /// City details
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
    }
}
