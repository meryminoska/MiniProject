using System;
using System.Linq;

namespace MiniProject
{
    public static class UI
    {
        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                WriteLineColor("Main Menu", ConsoleColor.Cyan);
                Console.WriteLine("1. Log in as librarian");
                Console.WriteLine("2. Log in as customer");
                Console.WriteLine("3. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowLibrarianMenu();
                        break;
                    case "2":
                        ShowCustomerMenu();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        WriteLineColor("Invalid option, please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private static void ShowLibrarianMenu()
        {
            const string Password = "Bookw0rm";
            WriteColor("Enter librarian password: ", ConsoleColor.Yellow);
            if (Console.ReadLine() != Password)
            {
                WriteLineColor("Invalid password!", ConsoleColor.Red);
                return;
            }

            while (true)
            {
                Console.Clear();
                WriteLineColor("Librarian Menu", ConsoleColor.Cyan);
                Console.WriteLine("1. Add/remove/update data");
                Console.WriteLine("2. Display data");
                Console.WriteLine("3. Exit to Main Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        ManageData();
                        break;
                    case "2":
                        DisplayData();
                        break;
                    case "3":
                        return;
                    default:
                        WriteLineColor("Invalid option, please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private static void ManageData()
        {
            while (true)
            {
                Console.Clear();
                WriteLineColor("Manage Data", ConsoleColor.Cyan);
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Remove book");
                Console.WriteLine("3. Add customer");
                Console.WriteLine("4. Remove customer");
                Console.WriteLine("5. Add loan");
                Console.WriteLine("6. Remove loan");
                Console.WriteLine("7. Renew book");
                Console.WriteLine("8. Back to Librarian Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        RemoveBook();
                        break;
                    case "3":
                        AddCustomer();
                        break;
                    case "4":
                        RemoveCustomer();
                        break;
                    case "5":
                        AddLoan();
                        break;
                    case "6":
                        RemoveLoan();
                        break;
                    case "7":
                        RenewLoan();
                        break;
                    case "8":
                        return;
                    default:
                        WriteLineColor("Invalid option, please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private static void AddBook()
        {
            var book = new Book();
            WriteColor("Enter book name: ", ConsoleColor.Yellow);
            book.Name = Console.ReadLine();
            WriteColor("Enter book author: ", ConsoleColor.Yellow);
            book.Author = Console.ReadLine();
            WriteColor("Enter book ISBN: ", ConsoleColor.Yellow);
            book.ISBN = Console.ReadLine();
            book.AcquisitionDate = DateTime.Today;
            WriteColor("Enter book category (General, Adult, Children): ", ConsoleColor.Yellow);
            book.Category = Console.ReadLine();
            LibrarySystem.AddBook(book);
            WriteLineColor($"{book.Name} with ISBN {book.ISBN} added to the database on {book.AcquisitionDate.ToShortDateString()}.", ConsoleColor.Green);
        }

        private static void RemoveBook()
        {
            WriteColor("Enter book ISBN: ", ConsoleColor.Yellow);
            var isbn = Console.ReadLine();
            LibrarySystem.RemoveBook(isbn);
            WriteLineColor($"Book with ISBN {isbn} removed from the database.", ConsoleColor.Green);
        }

        private static void AddCustomer()
        {
            var customer = new Customer();
            WriteColor("Enter customer name: ", ConsoleColor.Yellow);
            customer.Name = Console.ReadLine();
            WriteColor("Enter customer Date of Birth (yyyy-mm-dd): ", ConsoleColor.Yellow);
            customer.DateOfBirth = DateTime.Parse(Console.ReadLine());
            customer.Id = LibrarySystem.Customers.Any() ? LibrarySystem.Customers.Max(c => c.Id) + 1 : 1;
            LibrarySystem.AddCustomer(customer);
            WriteLineColor($"{customer.Name} with Date of Birth {customer.DateOfBirth.ToShortDateString()} added to the database with customer ID {customer.Id}.", ConsoleColor.Green);
        }

        private static void RemoveCustomer()
        {
            WriteColor("Enter customer ID: ", ConsoleColor.Yellow);
            var id = int.Parse(Console.ReadLine());
            LibrarySystem.RemoveCustomer(id);
            WriteLineColor($"Customer with ID {id} removed from the database.", ConsoleColor.Green);
        }

        private static void AddLoan()
        {
            WriteColor("Enter customer ID: ", ConsoleColor.Yellow);
            var customerId = int.Parse(Console.ReadLine());
            WriteColor("Enter book ISBN: ", ConsoleColor.Yellow);
            var isbn = Console.ReadLine();
            var customer = LibrarySystem.Customers.FirstOrDefault(c => c.Id == customerId);
            var book = LibrarySystem.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (customer == null || book == null)
            {
                WriteLineColor("Invalid customer ID or book ISBN.", ConsoleColor.Red);
                return;
            }
            var loan = new Loan { CustomerId = customerId, BookISBN = isbn, DueDate = DateTime.Today.AddDays(21) };
            LibrarySystem.AddLoan(loan);
            WriteLineColor($"{customer.Name} has been given book {book.Name}. The due date is {loan.DueDate.ToShortDateString()}.", ConsoleColor.Green);
        }

        private static void RemoveLoan()
        {
            WriteColor("Enter customer ID: ", ConsoleColor.Yellow);
            var customerId = int.Parse(Console.ReadLine());
            WriteColor("Enter book ISBN: ", ConsoleColor.Yellow);
            var isbn = Console.ReadLine();
            LibrarySystem.RemoveLoan(customerId, isbn);
            WriteLineColor($"Loan of book with ISBN {isbn} for customer ID {customerId} removed from the database.", ConsoleColor.Green);
        }

        private static void RenewLoan()
        {
            WriteColor("Enter customer ID: ", ConsoleColor.Yellow);
            var customerId = int.Parse(Console.ReadLine());
            WriteColor("Enter book ISBN: ", ConsoleColor.Yellow);
            var isbn = Console.ReadLine();
            LibrarySystem.RenewLoan(customerId, isbn);
            WriteLineColor($"Loan of book with ISBN {isbn} for customer ID {customerId} renewed. The new due date is {DateTime.Today.AddDays(21).ToShortDateString()}.", ConsoleColor.Green);
        }

        private static void DisplayData()
        {
            while (true)
            {
                Console.Clear();
                WriteLineColor("Display Data", ConsoleColor.Cyan);
                Console.WriteLine("1. Display all books");
                Console.WriteLine("2. Display all customers");
                Console.WriteLine("3. Display all loans");
                Console.WriteLine("4. Search for a book");
                Console.WriteLine("5. Search for a customer");
                Console.WriteLine("6. Search for all books on loan to a customer");
                Console.WriteLine("7. Back to Librarian Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        DisplayAllBooks();
                        break;
                    case "2":
                        DisplayAllCustomers();
                        break;
                    case "3":
                        DisplayAllLoans();
                        break;
                    case "4":
                        SearchForBook();
                        break;
                    case "5":
                        SearchForCustomer();
                        break;
                    case "6":
                        SearchBooksOnLoanToCustomer();
                        break;
                    case "7":
                        return;
                    default:
                        WriteLineColor("Invalid option, please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private static void DisplayAllBooks()
        {
            WriteLineColor("All Books:", ConsoleColor.Cyan);
            WriteLineColor("Name, Author, ISBN, Acquisition Date, Category", ConsoleColor.Green);
            foreach (var book in LibrarySystem.Books)
            {
                Console.WriteLine($"{book.Name}, {book.Author}, {book.ISBN}, {book.AcquisitionDate.ToShortDateString()}, {book.Category}");
            }
            WriteLineColor("Press any key to continue...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void DisplayAllCustomers()
        {
            WriteLineColor("All Customers:", ConsoleColor.Cyan);
            WriteLineColor("ID, Name, Date of Birth", ConsoleColor.Green);
            foreach (var customer in LibrarySystem.Customers)
            {
                Console.WriteLine($"{customer.Id}, {customer.Name}, {customer.DateOfBirth.ToShortDateString()}");
            }
            WriteLineColor("Press any key to continue...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void DisplayAllLoans()
        {
            WriteLineColor("All Loans:", ConsoleColor.Cyan);
            WriteLineColor("Customer ID, Customer Name, Book ISBN, Book Name, Due Date", ConsoleColor.Green);
            foreach (var loan in LibrarySystem.Loans)
            {
                var customer = LibrarySystem.Customers.FirstOrDefault(c => c.Id == loan.CustomerId);
                var book = LibrarySystem.Books.FirstOrDefault(b => b.ISBN == loan.BookISBN);
                var dueDateColor = loan.DueDate < DateTime.Today ? ConsoleColor.Red : ConsoleColor.White;
                WriteLineColor($"{loan.CustomerId}, {customer?.Name}, {loan.BookISBN}, {book?.Name}, {loan.DueDate.ToShortDateString()}", dueDateColor);
            }
            WriteLineColor("Press any key to continue...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void SearchForBook()
        {
            WriteColor("Enter book ISBN, Name or Author: ", ConsoleColor.Yellow);
            var searchCriteria = Console.ReadLine();
            var results = LibrarySystem.Books.Where(b => b.ISBN.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                                                          b.Name.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                                                          b.Author.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase)).ToList();
            WriteLineColor("Books found:", ConsoleColor.Cyan);
            if (results.Any())
            {
                foreach (var book in results)
                {
                    Console.WriteLine($"{book.Name}, {book.Author}, {book.ISBN}, {book.AcquisitionDate.ToShortDateString()}, {book.Category}");
                }
            }
            else
            {
                WriteLineColor("None", ConsoleColor.Red);
            }
            WriteLineColor("Press any key to continue...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void SearchForCustomer()
        {
            WriteColor("Enter customer ID or Name: ", ConsoleColor.Yellow);
            var searchCriteria = Console.ReadLine();
            var results = LibrarySystem.Customers.Where(c => c.Id.ToString().Contains(searchCriteria) ||
                                                              c.Name.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase)).ToList();
            WriteLineColor("Customers found:", ConsoleColor.Cyan);
            if (results.Any())
            {
                foreach (var customer in results)
                {
                    Console.WriteLine($"{customer.Id}, {customer.Name}, {customer.DateOfBirth.ToShortDateString()}");
                }
            }
            else
            {
                WriteLineColor("None", ConsoleColor.Red);
            }
            WriteLineColor("Press any key to continue...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void SearchBooksOnLoanToCustomer()
        {
            WriteColor("Enter customer ID: ", ConsoleColor.Yellow);
            var customerId = int.Parse(Console.ReadLine());
            var loans = LibrarySystem.Loans.Where(l => l.CustomerId == customerId).ToList();
            WriteLineColor("Loans found:", ConsoleColor.Cyan);
            if (loans.Any())
            {
                foreach (var loan in loans)
                {
                    var customer = LibrarySystem.Customers.FirstOrDefault(c => c.Id == loan.CustomerId);
                    var book = LibrarySystem.Books.FirstOrDefault(b => b.ISBN == loan.BookISBN);
                    var dueDateColor = loan.DueDate < DateTime.Today ? ConsoleColor.Red : ConsoleColor.White;
                    WriteLineColor($"{loan.CustomerId}, {customer?.Name}, {loan.BookISBN}, {book?.Name}, {loan.DueDate.ToShortDateString()}", dueDateColor);
                }
            }
            else
            {
                WriteLineColor("None", ConsoleColor.Red);
            }
            WriteLineColor("Press any key to continue...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void ShowCustomerMenu()
        {
            WriteColor("Enter customer ID: ", ConsoleColor.Yellow);
            if (!int.TryParse(Console.ReadLine(), out var customerId) || !LibrarySystem.Customers.Any(c => c.Id == customerId))
            {
                WriteLineColor("Invalid customer ID!", ConsoleColor.Red);
                return;
            }

            while (true)
            {
                Console.Clear();
                WriteLineColor("Customer Menu", ConsoleColor.Cyan);
                Console.WriteLine("1. Return a book");
                Console.WriteLine("2. Search for a book");
                Console.WriteLine("3. Display my current loans");
                Console.WriteLine("4. Exit to Main Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        ReturnBook(customerId);
                        break;
                    case "2":
                        SearchForBook();
                        break;
                    case "3":
                        DisplayCurrentLoans(customerId);
                        break;
                    case "4":
                        return;
                    default:
                        WriteLineColor("Invalid option, please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private static void ReturnBook(int customerId)
        {
            WriteColor("Enter book ISBN: ", ConsoleColor.Yellow);
            var isbn = Console.ReadLine();
            var loan = LibrarySystem.Loans.FirstOrDefault(l => l.CustomerId == customerId && l.BookISBN == isbn);
            if (loan == null)
            {
                WriteLineColor($"Book with ISBN {isbn} not found on loan.", ConsoleColor.Red);
                return;
            }
            if (loan.DueDate < DateTime.Today)
            {
                WriteLineColor("Book is overdue, please see the librarian to pay your fine.", ConsoleColor.Red);
                return;
            }
            LibrarySystem.RemoveLoan(customerId, isbn);
            WriteLineColor("Book returned, please place the book on the shelf to your right.", ConsoleColor.Green);
        }

        private static void DisplayCurrentLoans(int customerId)
        {
            var loans = LibrarySystem.Loans.Where(l => l.CustomerId == customerId).ToList();
            WriteLineColor("Your current loans:", ConsoleColor.Cyan);
            if (loans.Any())
            {
                foreach (var loan in loans)
                {
                    var book = LibrarySystem.Books.FirstOrDefault(b => b.ISBN == loan.BookISBN);
                    var dueDateColor = loan.DueDate < DateTime.Today ? ConsoleColor.Red : ConsoleColor.White;
                    WriteLineColor($"{loan.BookISBN}, {book?.Name}, {loan.DueDate.ToShortDateString()}", dueDateColor);
                }
            }
            else
            {
                WriteLineColor("None", ConsoleColor.Red);
            }
            WriteLineColor("Press any key to continue...", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void WriteLineColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void WriteColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
