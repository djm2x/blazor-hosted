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
        protected bool dialogIsOpen = false, isLoadingResults = true;
        protected string sortBy = "id", sortDir = "desc";
        protected int startIndex = 0, pageSize = 10, length = 0;

        protected override async Task OnInitializedAsync()
        {
            await getList();
        }

        private async Task getList() {
            isLoadingResults = true;
            var r = await uow.users.GetList(startIndex, pageSize, sortBy, sortDir);
            
            dataSource = r.list;
            length = r.count;
            isLoadingResults = false;
            await console.log(isLoadingResults);
            await console.log(r);

            StateHasChanged();
        }

        // private async Task Log()
        // {
        //     await JSRuntime.InvokeVoidAsync("console.log", o);
        // }

        protected async void SortData(MatSortChangedEvent sort)
        {
            sortBy = sort.SortId;
            sortDir = sort.Direction == MatSortDirection.Asc ? "asc" : "desc";

            await getList();
        }

        protected async void OnPage(MatPaginatorPageEvent e)
        {
            pageSize = e.PageSize;
            startIndex = e.PageIndex * e.PageSize;

            await getList();
        }

        public async void submit(User o)
        {
            Console.WriteLine(dialogIsOpen);
            if (o.Id == 0)
            {
                await uow.users.Post(o);
                dialogIsOpen = false;
                reset();
                await getList();
            }
            else
            {
                await uow.users.Put(o.Id, o);
                dialogIsOpen = false;
                reset();
                await getList();
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
            await getList();
        }
    }
}