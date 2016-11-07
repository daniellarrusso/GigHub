using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        [Required]
        public byte Genre { get; set; }

        public DateTime GetGigDate()
        {
            return DateTime.Parse(String.Format("{0} {1}", Date, Time));
        }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                return (Id != 0) ? "Update" : "Create";
            }
        }

    }
}