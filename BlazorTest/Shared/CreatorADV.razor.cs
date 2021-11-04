using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTest.Models.CarModules;
using Microsoft.AspNetCore.Components;

namespace BlazorTest.Shared
{
    public class CreatorADVModel:ComponentBase 
    {
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string Surname { get; set; }
        [Parameter]
        public string Lastname { get; set; }
        [Parameter] public string Phone_number { get; set; }
        [Parameter] public string ModelName { get; set; }
        [Parameter] public string Color { get; set; }
        [Parameter] public string YearOfConstruction { get; set; }
        [Parameter] public int Mileage { get; set; }
        [Parameter] public CarCase CarCase { get; set; }
        [Parameter] public double lik { get; set; }
        [Parameter] public int power { get; set; }
        [Parameter] public CarEngine Engine { get; set; }
        [Parameter] public CarEquipment Equipment { get; set; }
        [Parameter] public int Tax { get; set; }
        [Parameter] public Gearbox Gearbox { get; set; }
        [Parameter] public string MoreInfo { get; set; }
        [Parameter] public string VinCode { get; set; }
        [Parameter] public string NumberPlate { get; set; }
        [Parameter] public int Cost { get; set; }
    }
}
