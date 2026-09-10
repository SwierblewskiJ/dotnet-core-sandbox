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

    foreach(var s in a.Students.Include(s=>s.Courses)){
        WriteLine("{0} {1} attends the courses: {2}", s.FirstName,s.LastName,s.Courses.Count);
    
        foreach(var c in s.Courses)
        {
            WriteLine($"    {c.Title}");
        }
    }
}