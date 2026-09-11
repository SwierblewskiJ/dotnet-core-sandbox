using Microsoft.EntityFrameworkCore;
using Models;

using (Academy a = new())
{
    bool deleted = await a.Database.EnsureDeletedAsync();
    WriteLine($"Database deleted: {deleted}");

    bool created = await a.Database.EnsureCreatedAsync();
    WriteLine($"Database created: {created}");

    WriteLine("SQL script used to create database:");
    WriteLine(a.Database.GenerateCreateScript());

#pragma warning disable CS8604 // Possible null reference argument.
    foreach (var s in a.Students.Include(s=>s.Courses)){
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        WriteLine("{0} {1} attends the courses: {2}", s.FirstName, s.LastName, s.Courses.Count);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        foreach (var c in s.Courses)
        {
            WriteLine($"    {c.Title}");
        }
    }
#pragma warning restore CS8604 // Possible null reference argument.
}