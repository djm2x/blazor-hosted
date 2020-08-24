using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazor.Client.Services;
using MyBlazor.Shared;
using Microsoft.JSInterop;
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

        protected override async Task OnInitializedAsync()
        {
            dataSource = await uow.users.Get();


        }

        // private async Task Log()
        // {
        //     await JSRuntime.InvokeVoidAsync("console.log", o);
        // }

        protected void SortData(/*MatSortChangedEvent*/ dynamic sort)
        {
            // sortedData = desserts.ToArray();
            // if (!(sort == null || sort.Direction == MatSortDirection.None || string.IsNullOrEmpty(sort.SortId)))
            // {
            //     Comparison<Dessert> comparison = null;
            //     switch (sort.SortId)
            //     {
            //         case "name":
            //             comparison = (s1, s2) => string.Compare(s1.Name, s2.Name, StringComparison.InvariantCultureIgnoreCase);
            //             break;
            //         case "calories":
            //             comparison = (s1, s2) => s1.Calories.CompareTo(s2.Calories);
            //             break;
            //         case "fat":
            //             comparison = (s1, s2) => s1.Fat.CompareTo(s2.Fat);
            //             break;
            //         case "carbs":
            //             comparison = (s1, s2) => s1.Carbs.CompareTo(s2.Carbs);
            //             break;
            //         case "protein":
            //             comparison = (s1, s2) => s1.Protein.CompareTo(s2.Protein);
            //             break;
            //     }
            //     if (comparison != null)
            //     {
            //         if (sort.Direction == MatSortDirection.Desc)
            //         {
            //             Array.Sort(sortedData, (s1, s2) => -1 * comparison(s1, s2));
            //         }
            //         else
            //         {
            //             Array.Sort(sortedData, comparison);
            //         }
            //     }
            // }
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