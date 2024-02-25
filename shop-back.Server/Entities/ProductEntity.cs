using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


/*

назва
урл або урли картинок
рейтинг від 1 до 5 це кількість зірочок
характеритика товару  це масив з строками
id товару
ціна
знижка це не обовязкове поле
isHit це типу чи цей товар є бесцелером
 категорія
к-сть товару
наявність товару                                                                    
*/
namespace shop_back.Server.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }
        public IList<string> Images { get; set; }
        //public int Rating { get; set; } to relation with feedback
        public IList<string> Characteristics { get; set; }
        public IList<FeedbackEntity> Feedbacks { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; } = 0;
        public bool IsHit { get; set ; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }


#pragma warning disable CS8618 // Required by Entity Framework
        public ProductEntity() { }
#pragma warning restore CS8618 // Required by Entity Framework

    }
}