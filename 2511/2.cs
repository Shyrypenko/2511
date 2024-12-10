using System;
using System.Collections.Generic;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
}

class Library
{
    private LinkedList<Book> books = new();

    public void AddBook(Book book) => books.AddLast(book);

    public void InsertBookAtStart(Book book) => books.AddFirst(book);

    public void InsertBookAtPosition(Book book, int position)
    {
        if (position < 0 || position > books.Count) throw new ArgumentOutOfRangeException(nameof(position));
        var node = books.First;
        for (int i = 0; i < position && node != null; i++) node = node.Next;
        if (node == null) books.AddLast(book);
        else books.AddBefore(node, book);
    }

    public void RemoveBookFromStart() => books.RemoveFirst();

    public void RemoveBookFromEnd() => books.RemoveLast();

    public void RemoveBookAtPosition(int position)
    {
        if (position < 0 || position >= books.Count) throw new ArgumentOutOfRangeException(nameof(position));
        var node = books.First;
        for (int i = 0; i < position && node != null; i++) node = node.Next;
        if (node != null) books.Remove(node);
    }

    public void RemoveBook(string title)
    {
        var node = books.First;
        while (node != null)
        {
            if (node.Value.Title == title)
            {
                books.Remove(node);
                return;
            }
            node = node.Next;
        }
        Console.WriteLine("Book not found.");
    }

    public Book FindBook(Func<Book, bool> predicate)
    {
        foreach (var book in books)
        {
            if (predicate(book)) return book;
        }
        return null;
    }

    public void PrintBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} | {book.Author} | {book.Genre} | {book.Year}");
        }
    }
}

class Program
{
    static void Main()
    {
        var library = new Library();
        library.AddBook(new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = 1949 });
        library.AddBook(new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Year = 1960 });

        Console.WriteLine("All books:");
        library.PrintBooks();

        Console.WriteLine("\nAdding a book at the start:");
        library.InsertBookAtStart(new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Novel", Year = 1925 });
        library.PrintBooks();

        Console.WriteLine("\nRemoving the last book:");
        library.RemoveBookFromEnd();
        library.PrintBooks();

        Console.WriteLine("\nFinding a book by title:");
        var book = library.FindBook(b => b.Title == "1984");
        if (book != null) Console.WriteLine($"{book.Title} found!");
    }
}