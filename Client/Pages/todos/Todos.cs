using System;
using System.Collections.Generic;
using System.Linq;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyBlazor.Shared;

namespace MyBlazor.Client.Pages.todos
{
    public class Todos : ComponentBase
    {
        protected List<TodoModel> TodoList = new List<TodoModel>();
        protected string myTodoText = null;
        protected DateTime? myDeadline = DateTime.Now;
        protected int editedID = 0;

        [Inject]
        protected IMatToaster Toaster { get; set; }

        protected void Click(MouseEventArgs e)
        {
            if (myTodoText == null)
            {
                Toaster.Add("Cannot add empty values", MatToastType.Warning, "Todo List", null);
                return;
            }

            if (editedID == 0)
            {
                var myTodoItem = new TodoModel()
                {
                    Id = TodoList.Count() + 1,
                    Deadline = myDeadline == null ? DateTime.Now.AddDays(1) : ((DateTime)myDeadline),
                    Todo = myTodoText,
                    IsCompleted = false
                };
                TodoList.Add(myTodoItem);
                myTodoText = null;
                myDeadline = null;
                Toaster.Add("New todo added.", MatToastType.Info, "Todo List", null);
            }
            else
            {
                var myTodo = TodoList.FirstOrDefault(x => x.Id == editedID);
                myTodo.Todo = myTodoText;
                myTodo.Deadline = myDeadline == null ? DateTime.Now.AddDays(1) : ((DateTime)myDeadline);

                myTodoText = null;
                myDeadline = null;
                Toaster.Add("Todo edit finished.", MatToastType.Info, "Todo List", null);
                editedID = 0;
            }

        }

        protected void deleteItem(int id)
        {
            var myTodo = TodoList.FirstOrDefault(x => x.Id == id);
            TodoList.Remove(myTodo);
            Toaster.Add("Todo removed.", MatToastType.Info, "Todo List", null);
        }

        protected void completeItem(int id)
        {
            var myTodo = TodoList.FirstOrDefault(x => x.Id == id);
            myTodo.IsCompleted = !myTodo.IsCompleted;
            Toaster.Add("Todo status changed.", MatToastType.Info, "Todo List", null);
        }

        protected void editItem(int id)
        {
            var myTodo = TodoList.FirstOrDefault(x => x.Id == id);
            myTodoText = myTodo.Todo;
            myDeadline = myTodo.Deadline;
            editedID = id;
        }
    }
}