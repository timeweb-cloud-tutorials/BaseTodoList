using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.App.Data;
using ToDoList.App.Models;

namespace ToDoList.App.Controllers;

public class ToDoController : Controller
{
    private readonly TodoListDbContext _context;

    public ToDoController(TodoListDbContext context)
    {
        _context = context;
    }

    // GET: ToDo
    public IActionResult Index()
    {
        var todoItems = _context.ToDoItems.ToList();
        return View(todoItems);
    }

    // GET: ToDo/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ToDo/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TodoItem toDoItem)
    {
        if (ModelState.IsValid)
        {
            _context.Add(toDoItem);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(toDoItem);
    }

    // GET: ToDo/Update/5
    public IActionResult Update(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var toDoItem = _context.ToDoItems.Find(id);
        if (toDoItem == null)
        {
            return NotFound();
        }
        return View(toDoItem);
    }

    // POST: ToDo/Update/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, TodoItem toDoItem)
    {
        if (id != toDoItem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(toDoItem);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(toDoItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(toDoItem);
    }

    private bool ToDoItemExists(int id)
    {
        return _context.ToDoItems.Any(e => e.Id == id);
    }

    // GET: ToDo/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var toDoItem = _context.ToDoItems.FirstOrDefault(m => m.Id == id);
        if (toDoItem == null)
        {
            return NotFound();
        }

        return View(toDoItem);
    }

    // POST: ToDo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var toDoItem = _context.ToDoItems.Find(id);
        _context.ToDoItems.Remove(toDoItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
