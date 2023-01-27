using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {

        //  This method should return a Customer object that represents the customer based on the CustomerId passed through.
        //  If no custom exists, this method should return NotFound.
        [HttpGet(Name = "GetCustomer")]
        public ActionResult<Customer> Get(string CustomerId)
        {
            int idParse;
            bool validId = int.TryParse(CustomerId, out idParse);
            if (validId)
            {
                Customer? customer = Database.Customers.Where(c => int.Parse(c.CustomerId) == idParse && c.Active == true).SingleOrDefault();
                return customer != null ? customer : NotFound();
            }
            else
            {
                return BadRequest();
            }
                
        }

        //       This method should validate that the customer provided in the body is correctly validated based on the following rules.
        //       A First Name, Last Name, House name or number, Address Line 1, either city or town and postalcode are provided.
        //       if none of these are provided then a BadRequest must be returned.
        //       If a date created is provided, but does not match the date provided in the database a BadRequest should be returned.
        //       If the customer does not exist, then a new customer should be created returning a random integer as the customer number.
        //       The customer number should be 6 digits long and unique.
        //       The LastUpdated property is read only and cannot be edited by the end user and should contain the date and time that the
        //       the customer record was last updated.
        //       CustomerId must equal zero to denote a new record / customer
        [HttpPost(Name = "UpdateCustomer")]
        public ActionResult<string> Post([FromBody] Customer customer)
        {

            //  Handling new customer creation in the case that "0" is passed in as the CustomerId
            if (customer.CustomerId == "0")
            {

                if (
                    !string.IsNullOrEmpty(customer.FirstName) &&
                    !string.IsNullOrEmpty(customer.LastName) &&
                    !string.IsNullOrEmpty(customer.Address.HouseNameOrNumber) &&
                    !string.IsNullOrEmpty(customer.Address.AddressLine1) &&
                    (!string.IsNullOrEmpty(customer.Address.CountyOrState) || !string.IsNullOrEmpty(customer.Address.CityOrState)) &&
                    !string.IsNullOrEmpty(customer.Address.PostalCode)
                    )
                {
                    customer.CustomerId = Database.NextCustomerId();
                    customer.DateLastUpdated = DateTime.Now;
                    customer.DateCreated = DateTime.Now;
                    customer.Active = true;
                    Database.Customers.Add(customer);
                    return customer.CustomerId;
                }

                else
                {
                    return BadRequest();
                }
            }

            //  Handling updates to an existing customer in the case that the CustomerId passed in already exists
            else if (Database.CustomerExists(int.Parse(customer.CustomerId)))
            {
                Customer customerToUpdate = 
                Database.Customers.FirstOrDefault(
                    c => int.Parse(c.CustomerId) == int.Parse(customer.CustomerId)
                );
                
                if (customerToUpdate != null &&
                    !string.IsNullOrEmpty(customer.FirstName) &&
                    !string.IsNullOrEmpty(customer.LastName) &&
                    !string.IsNullOrEmpty(customer.Address.HouseNameOrNumber) &&
                    !string.IsNullOrEmpty(customer.Address.AddressLine1) &&
                    (!string.IsNullOrEmpty(customer.Address.CountyOrState) || !string.IsNullOrEmpty(customer.Address.CityOrState)) &&
                    !string.IsNullOrEmpty(customer.Address.PostalCode)
                    )
                {
                    //  Check that DateCreated matches the value in the specified customer object    
                    //  Only works using format: "2023-01-27T17:48:29.7884639+01:00" 
                    if (customerToUpdate.DateCreated != customer.DateCreated)
                    {
                       return BadRequest();
                    }

                    else
                    {
                        int index = Database.Customers.IndexOf(customerToUpdate);
                        customer.DateCreated = customerToUpdate.DateCreated;
                        Database.Customers[index] = customer;
                        Database.Customers[index].DateLastUpdated = DateTime.Now;
                        return customer.CustomerId;
                    }
                }

                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return BadRequest();
            }
        }     
    }
}