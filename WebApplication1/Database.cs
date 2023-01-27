namespace WebApplication1
{

    public class Address
    {
        public string HouseNameOrNumber { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string CityOrState { get; set; }

        public string CountyOrState { get; set; }

        public string PostalCode { get; set; }
    }

    public class Customer
    {   
        public string CustomerId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }
        
        public DateTime DateCreated { get; set; }

        public DateTime DateLastUpdated { get; set; }

        public bool Active { get; set; }
    }

    public static class Database
    {
        public static List<Customer> Customers = new List<Customer>();

        public static void InitData()
        {
            Customers.Add(new Customer()
            {
                CustomerId = "000001",
                FirstName = "Scott",
                LastName = "Blood",
                Address = new Address()
                {
                    HouseNameOrNumber = "72",
                    AddressLine1 = "Whyndham Avenue",
                    CityOrState = "Clifton",
                    CountyOrState = "Manchester",
                    PostalCode = "M27 6PY"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000002",
                FirstName = "Caroline",
                LastName = "Moulsdale",
                Address = new Address()
                {
                    HouseNameOrNumber = "26",
                    AddressLine1 = "Trefoil Close",
                    CityOrState = "Birchwood",
                    CountyOrState = "Warrington",
                    PostalCode = "WA3 6NX"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000003",
                FirstName = "David",
                LastName = "Harrington",
                Address = new Address()
                {
                    HouseNameOrNumber = "39",
                    AddressLine1 = "Goulden Street",
                    CityOrState = "Salford",
                    CountyOrState = "Manchester",
                    PostalCode = "M7 8PE"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000004",
                FirstName = "Rafiq",
                LastName = "Hussain",
                Address = new Address()
                {
                    HouseNameOrNumber = "Altingham Cottage",
                    AddressLine1 = "Partington Lane",
                    CityOrState = "Preston",
                    PostalCode = "PR3 3NE"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000005",
                FirstName = "Scott",
                LastName = "Blood",
                Address = new Address()
                {
                    HouseNameOrNumber = "72",
                    AddressLine1 = "Whyndham Avenue",
                    CityOrState = "Clifton",
                    CountyOrState = "Manchester",
                    PostalCode = "M27 6PY"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000006",
                FirstName = "Mark",
                LastName = "Davies",
                Address = new Address()
                {
                    HouseNameOrNumber = "449",
                    AddressLine1 = "Devonshire Road",
                    CityOrState = "Hexham",
                    CountyOrState = "Chester",
                    PostalCode = "CH2 4TY"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000007",
                FirstName = "Maria",
                LastName = "Bond",
                Address = new Address()
                {
                    HouseNameOrNumber = "5A",
                    AddressLine1 = "Camden Flats",
                    CityOrState = "Liverpool",
                    PostalCode = "L1 8SD"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000008",
                FirstName = "Rachel",
                LastName = "Helen",
                Address = new Address()
                {
                    HouseNameOrNumber = "22",
                    AddressLine1 = "Lambourne Avenue",
                    CityOrState = "Staines",
                    CountyOrState = "Surrey",
                    PostalCode = "TW15 3JE"
                },
                DateCreated = DateTime.Now,
                Active = true
            });

            Customers.Add(new Customer()
            {
                CustomerId = "000009",
                FirstName = "Emma",
                LastName = "Kennedy",
                Address = new Address()
                {
                    HouseNameOrNumber = "Croxtley House",
                    AddressLine1 = "Croxtley Park",
                    CityOrState = "Croxtley",
                    CountyOrState = "Lancashire",
                    PostalCode = "LL15 3RE"
                },
                DateCreated = DateTime.Now,
                Active = true
            });
        }


        //  This method should return a boolean value that indicates whether or not a customer exists.
        //  Return True if the customer exists, else return false
        public static bool CustomerExists(int CustomerId)
        {
            Customer? customer = Database.Customers.Where(c => int.Parse(c.CustomerId) == CustomerId && c.Active == true).SingleOrDefault();
            return customer != null ? true : default;
        }

        public static string NextCustomerId()
        {
            int nextId = Database.Customers.Select(c => int.Parse(c.CustomerId)).Max() +1;
            return nextId.ToString("D6");
        }
    }
}