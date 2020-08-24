using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazor.Client.Services;
using MyBlazor.Shared;
using MatBlazor;
using MyBlazor.Client.Helper;

namespace MyBlazor.Client.Pages.list
{
    public partial class List
    {
        [Inject]
        protected UowService uow { get; set; }

        [Inject]
        protected MyConsole console { get; set; }

        protected User[] dataSource = new User[] { };

        protected User o = new User();

        protected bool dialogIsOpen = false;
        protected string sortBy = "id", sortDir = "desc";
        protected int startIndex = 0, pageSize = 6, Length = 0;

        protected override async Task OnInitializedAsync()
        {
            var r = await uow.users.GetList(startIndex, pageSize, sortBy, sortDir);

            dataSource = r.list;
            Length = r.lenght;
        }

        // private async Task Log()
        // {
        //     await JSRuntime.InvokeVoidAsync("console.log", o);
        // }

        protected void SortData(MatSortChangedEvent sort)
        {
            sortBy = sort.SortId;
            sortDir = sort.Direction == MatSortDirection.Asc ? "asc" : "desc";
        }

        protected void OnPage(MatPaginatorPageEvent e)
        {
            pageSize = e.PageSize;
            startIndex = e.PageIndex * e.PageSize;
        }

        public async void submit(User o)
        {
            Console.WriteLine(dialogIsOpen);
            if (o.Id == 0)
            {
                await uow.users.Post(o);
                dialogIsOpen = false;
                reset();
                await OnInitializedAsync();
            }
            else
            {
                await uow.users.Put(o.Id, o);
                dialogIsOpen = false;
                reset();
                await OnInitializedAsync();
            }


        }

        public void reset()
        {
            this.o = new User();
        }

        public async void edit(User o)
        {

            this.o = o;
            dialogIsOpen = true;
            await console.log(dialogIsOpen);
        }

        public async void delete(User o)
        {
            await uow.users.Delete(o.Id);
            await OnInitializedAsync();
        }
    }
}