using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserTariffDto
    {
        public int user_id { get; set; }

        public int tariff_id { get; set; }

        public bool active { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public virtual tariff tariff { get; set; }

        public virtual users users { get; set; }

        public UserTariffDto() { }
        public UserTariffDto(user_tariff ut)
        {
            this.user_id = ut.user_id;
            this.tariff_id = ut.tariff_id;
            this.active = ut.active;
            this.startDate = ut.startDate;
            this.endDate = ut.endDate;
            this.users = ut.users;
            this.tariff = ut.tariff;

        }
    }
}
