using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniProject
{
    public static class LibrarySystem
    {
        public static List<Book> Books { get; private set; } = new List<Book>
        {
            new Book { Name = "The Lord of the Rings", Author = "J.R.R.Tolkien", ISBN = "48923847", AcquisitionDate = new DateTime(2024, 1, 15), Category = "General" },
            new Book { Name = "Skandar and the Unicorn Thief", Author = "A.F.Steadman", ISBN = "78942875", AcquisitionDate = new DateTime(2024, 2, 23), Category = "Children" },
            new Book { Name = "Battlefield Earth", Author = "L. Ron Hubbard", ISBN = "34859486", AcquisitionDate = new DateTime(2022, 8, 6), Category = "General" },
            new Book { Name = "The Dinosaur that Pooped Christmas", Author = "Tom Fletcher", ISBN = "91239543", AcquisitionDate = new DateTime(2023, 11, 20), Category = "Children" },
            new Book { Name = "Conversations with God", Author = "Neale Donald Walsch", ISBN = "22382593", AcquisitionDate = new DateTime(2021, 12, 25), Category = "General" },
            new Book { Name = "Lifeshocks", Author = "Sophie Sabbage", ISBN = "19275843", AcquisitionDate = new DateTime(2020, 9, 18), Category = "General" }
        };

        public static List<Customer> Customers { get; private set; } = new List<Customer>
        {
            new Customer { Id = 254, Name = "John Doe", DateOfBirth = new DateTime(1986, 6, 2) },
            new Customer { Id = 13, Name = "Geoff Watson", DateOfBirth = new DateTime(2013, 8, 8) },
            new Customer { Id = 119, Name = "Kelly Pyke", DateOfBirth = new DateTime(2006, 3, 25) },
            new Customer { Id = 87, Name = "Susan Boyle", DateOfBirth = new DateTime(1995, 2, 2) }
        };

        public static List<Loan> Loans { get; private set; } = new List<Loan>
        {
            new Loan { CustomerId = 254, BookISBN = "34859486", DueDate = new DateTime(2024, 9, 18) },
            new Loan { CustomerId = 254, BookISBN = "19275843", DueDate = new DateTime(2024, 9, 25) },
            new Loan { CustomerId = 87, BookISBN = "48923847", DueDate = new DateTime(2024, 10, 2) }
        };

        public static void AddBook(Book book) => Books.Add(book);
        public static void RemoveBook(string isbn) => Books.RemoveAll(b => b.ISBN == isbn);
        public static void AddCustomer(Customer customer) => Customers.Add(customer);
        public static void RemoveCustomer(int id) => Customers.RemoveAll(c => c.Id == id);
        public static void AddLoan(Loan loan) => Loans.Add(loan);
        public static void RemoveLoan(int customerId, string isbn) => Loans.RemoveAll(l => l.CustomerId == customerId && l.BookISBN == isbn);
        public static void RenewLoan(int customerId, string isbn)
        {
            var loan = Loans.FirstOrDefault(l => l.CustomerId == customerId && l.BookISBN == isbn);
            if (loan != null) loan.DueDate = DateTime.Today.AddDays(21);
        }
    }
}
