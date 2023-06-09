﻿using CaseMangementSystem.Contexts;
using CaseMangementSystem.Models;
using CaseMangementSystem.Models.Entities;

namespace CaseMangementSystem.Services
{
    internal class MenuService
    {
        private readonly DataContext _context = new();
        private readonly CommentService _commentService = new();
        private readonly CustomerService _customerService = new();
        private readonly TicketService _ticketService = new();
        private readonly TicketStatusService _ticketStatusService = new();
        public async Task StartApplicationUIAsync()
        {


            if (!_context.TicketStatuses.Any())
            {
                await _ticketStatusService.LoadTicketStatusesAsync();
            }

            Console.WriteLine("Welcome CMS Also know as Case Mangement System \n");
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Display All Tickets");
                Console.WriteLine("2. Display Customer Information");
                Console.WriteLine("3. Display Ticket Information");
                Console.WriteLine("4. Create Customer");
                Console.WriteLine("5. Create Ticket");
                Console.WriteLine("6. End application\n");

                switch (Diverse.CheckIfIntOrNot())
                {
                    case 1:
                        await DisplayAllTickets();
                        break;
                    case 2:
                        await DisplaySpecificCustomer();
                        break;
                    case 3:
                        await DisplaySpecificTicket();
                        break;
                    case 4:
                        await CreateCustomer();
                        break;
                    case 5:
                        await CreateTicket();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;

                }


            }

        }

        #region DisplayAllTickets
        public async Task DisplayAllTickets()
        {


            var customerTickets = _customerService.GetAllAsync();
            if (customerTickets != null)
            {
                Console.WriteLine("LIST OF ALL AVALIABLE TICKETS\n");
                foreach (var user in await customerTickets)
                {
                    DisplayCustomerInformation(user);
                }
            }
            else Console.WriteLine("No existing tickets were found");

            Diverse.WaitForIt();
        }
        #endregion

        #region DisplaySpecificCustomer
        public async Task DisplaySpecificCustomer()
        {
            Console.WriteLine("Enter email address:");
            var userInput = Diverse.CheckIfNullOrEmpty();

            var customer = await _customerService.GetAsync(userInput);

            if (customer != null)
            {
                DisplayCustomerInformation(customer);
            }
            else
            {
                Console.WriteLine("The Email you've entered does not match any customer in our database, please try again\n");
            }
            Diverse.WaitForIt();

        }
        #endregion

        #region DisplaySpecificTicket
        public async Task DisplaySpecificTicket()
        {
            Console.WriteLine("Enter Ticket ID");
            var userInput = Diverse.CheckIfIntOrNot();
            var selectedTicket = await _ticketService.GetTicketByIdAsync(userInput);

            if (selectedTicket != null)
            {
                while (true)
                {

                    Console.Clear();
                    await _ticketService.GetTicketByIdAsync(selectedTicket.TicketId);
                    Console.WriteLine($"Ticket Status: {selectedTicket.TicketStatus.StatusName}");
                    Console.WriteLine($"Title: {selectedTicket.Title}");
                    Console.WriteLine($"Description: {selectedTicket.Description}\n");


                    Console.WriteLine("Select one of the following options:");
                    Console.WriteLine("1. Show all comments");
                    Console.WriteLine("2. Change Ticket Status");
                    Console.WriteLine("3. Create new comment");
                    Console.WriteLine("4. Exit Ticket Overview");
                    switch (Diverse.CheckIfIntOrNot())
                    {
                        case 1:
                            ShowAllComments(selectedTicket);
                            break;
                        case 2:
                            await ChangeTicketStatus(selectedTicket);
                            break;
                        case 3:
                            await CreateComment(selectedTicket);
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Please try again");
                            break;

                    }


                }
            }
            else
            {
                Console.WriteLine("Ticket was not found, please try again!");
                Diverse.WaitForIt();
            }





        }
        #endregion

        #region ShowAllComments
        public void ShowAllComments(TicketEntity ticket)
        {
            var comments = ticket.Comments.ToList();

            if (comments != null)
            {
                Console.WriteLine("Comments: ");
                foreach (var comment in comments)
                {
                    Console.WriteLine(comment.CommentText);
                }
                Console.WriteLine("");
            }
            else Console.WriteLine("Unable to retrieve any comemnts from ticket");

            Diverse.WaitForIt();
        }
        #endregion

        #region ChangeTicketStatus
        public async Task ChangeTicketStatus(TicketEntity ticket)
        {

            Console.WriteLine("CHANGE TICKET STATUS");
            Console.WriteLine("1. Not started");
            Console.WriteLine("2. Started");
            Console.WriteLine("3. Completed");

            var userInput = Diverse.CheckIfIntOrNot();

            switch (userInput)
            {
                case 1:
                case 2:
                case 3:
                    await _ticketService.UpdateTicketStatusAsync(userInput, ticket.TicketId);
                    break;
                default:
                    Console.WriteLine("Failed to change ticket status due to invalid input, please try again\n");
                    break;
            }

        }
        #endregion

        #region CreateComment
        public async Task CreateComment(TicketEntity ticket)
        {
            Console.WriteLine("Enter new comment:");
            var userInput = Diverse.CheckIfNullOrEmpty();


            var _createNewComment = new Comment(userInput, ticket.Id);
            await _commentService.CreateCommentAsync(_createNewComment);
            await _ticketService.GetTicketByIdAsync(ticket.TicketId);

        }
        #endregion

        #region CreateCustomer
        public async Task CreateCustomer()
        {
            Console.WriteLine("Enter first name: ");
            string firstName = Diverse.CheckIfNullOrEmpty();
            Console.WriteLine("Enter last name: ");
            string lastName = Diverse.CheckIfNullOrEmpty();
            Console.WriteLine("Enter email: ");
            string email = Diverse.CheckIfNullOrEmpty();
            Console.WriteLine("Enter phonenumber: ");
            string phoneNumber = Diverse.CheckIfNullOrEmpty();

            var _createNewCustomer = new Customer(firstName, lastName, email, phoneNumber);

            await _customerService.CreateCustomerAsync(_createNewCustomer);

            Console.WriteLine("You have successfully created a customer account!");
        }
        #endregion

        #region CreateTicket
        public async Task CreateTicket()
        {
            Console.WriteLine("Creating a new support ticket");
            Console.WriteLine("Enter ticket Title: ");
            var title = Diverse.CheckIfNullOrEmpty();
            Console.WriteLine("Enter ticket description: ");
            var description = Diverse.CheckIfNullOrEmpty();
            Console.WriteLine("Already a customer? Enter your email address");
            Console.WriteLine("Not a customer? Leave the field empty to create an account!");
            var customerEmail = Console.ReadLine() ?? string.Empty;

            var customerSearch = await _customerService.GetAsync(customerEmail);

            if (customerSearch == null!)
            {
                await CreateCustomer();

                Console.WriteLine("Please enter the email address to verifiy that the account was created.");
                var userInput = Diverse.CheckIfNullOrEmpty();

                customerSearch = await _customerService.GetAsync(userInput);
            }

            var createNewTicket = new Ticket(title, description, customerSearch.Id, 1);

            await _ticketService.CreateTicketAsync(createNewTicket);

            Console.WriteLine("You're ticket has been successfully created!");

        }
        #endregion

        #region DisplayCustomerInformation
        public static void DisplayCustomerInformation(CustomerEntity customer)
        {

            if (customer != null)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------\n");
                Console.WriteLine("CUSTOMER INFORMATION");
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email:{customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}\n");


                var tickets = customer.Tickets.ToList();
                Console.WriteLine($"CUSTOMER TICKETS:\n");
                foreach (var ticket in tickets)
                {
                    Console.WriteLine($"Ticket ID: {ticket.TicketId}");
                    Console.WriteLine(ticket.TicketStatus.StatusName);
                    Console.WriteLine($"Title: {ticket.Title}");
                    Console.WriteLine($"DESCRIPTION: {ticket.Description}\n");

                    var ticketComments = ticket.Comments.ToList();

                    if (ticketComments != null)
                    {
                        Console.WriteLine("Comments:");

                        foreach (var comment in ticketComments)
                        {
                            Console.WriteLine($"{comment.CommentText}");
                        }
                        Console.WriteLine("");
                    }
                }



            }


        }
        #endregion




    }

}
