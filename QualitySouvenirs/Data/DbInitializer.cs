using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any categorys.
            if (context.Suppliers.Any())
            {
                return;   // DB has been seeded
            }

            var categorys = new Category[]
            {
            new Category{Name="Maori Gifts"},
            new Category{Name="Mugs"},
            new Category{Name="T-Shirts"},
            new Category{Name="Hats"}
            };
            foreach (Category c in categorys)
            {
                context.Categorys.Add(c);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
            {
            new Supplier{Name="Maori Local",Email="infor@mlocal.com",ContactPerson="Dahi Morle",Phone="0212523355",Address="99 Queens Road, Lower Hut, Wellington, New Zealand"},
            new Supplier{Name="Natural Goods",Email="infor@ngoods.com",ContactPerson="Eric White",Phone="0212511189",Address="1239 New North Road, Avondale, Auckland, New Zealand"}
            };
            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            var souvenirs = new Souvenir[]
            {
             //gifts
            new Souvenir{Name="Cotton Scarf",Description="100% Cotton, natural non-allergic and soft fabric.You can style this HANDMADE neck scarf as SHAWL, WRAP, as INFINITY scarf or tie around your head. Looks great with tops, dresses, jackets, coats, casual or business outfit.",Price=65.99,PathOfImage="gift001.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Silk Scarf",Description="Great as a shawl, head wrap, hijab, stole or light scarves for women. Wrap this pashmina scarf around your shoulder will warm you body in a evening night. And this silk scarf pashminas will dress up any outfit in any occasion. Such as: warm shawls and wraps for evening dresses or wedding warm shawl.",Price=35.99,PathOfImage="gift002.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Wool Scarf",Description="100% fine wool from Australia, bringing excellent warm feeling without making you feel heavy.Perfect for matching any dress and suitable for daily life, shopping, travelling, wedding, party, ceremonies and any important events.",Price=75.99,PathOfImage="gift003.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Blanket",Description="100% polar fleece blanket - high quality collection of luxury and cozy polar fleece fabric.Lightweight, ultra-soft, warm, and durable blanket. Classic luxuriously looking plaid pattern design in variety of colors. Perfect gift for friends & family for household, camping, &other usages.",Price=55.99,PathOfImage="gift004.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Toy",Description="Embroidered eye and nose details ensure safe use for all ages. Machine-washable. Ages 0+.",Price=15.99,PathOfImage="gift005.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Blanket",Description="100% polar fleece blanket - high quality collection of luxury and cozy polar fleece fabric.",Price=45.99,PathOfImage="gift006.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Green Plants",Description="Incredibly detailed characteristics - these succulents look and feel real. Extremely colorful and lifelike. Great for home and office, wreaths, bouquets, gifts, and other arrangments.",Price=4.99,PathOfImage="gift007.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Bear Toy",Description="Embroidered eye and nose details ensure safe use for all ages. Machine-washable. ",Price=15.99,PathOfImage="gift008.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Letter Toys",Description="Incredibly detailed characteristics",Price=6.99,PathOfImage="gift009.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Carrot Toys",Description="Incredibly detailed characteristics",Price=7.99,PathOfImage="gift010.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Doggy Toy",Description="Incredibly detailed characteristics. Machine-washable.",Price=11.99,PathOfImage="gift011.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Bear Toy",Description="100% fine wool from Australia, bringing excellent warm feeling without making you feel heavy.",Price=10.99,PathOfImage="gift012.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Wool Scarf",Description= "100% fine wool from Australia, bringing excellent warm feeling without making you feel heavy.", Price=37.99,PathOfImage="gift013.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Kiwibird Sculpture",Description="Stretched Canvas ready to be put on the wall. Satisfaction Guarantee 100%. Brighten your home of Office with the art prints from our store.",Price=19.99,PathOfImage="gift014.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="All Black Toy",Description="Bring New Zealand to your home. Best souvenir ever.",Price=25.99,PathOfImage="gift015.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Kiwibird Toy",Description="Stretched Canvas ready to be put on the wall. Satisfaction Guarantee 100%.",Price=12.99,PathOfImage="gift016.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="All Black Toy",Description="Bring New Zealand to your home. Best souvenir ever.",Price=25.99,PathOfImage="gift017.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="All Black Board",Description="Bring New Zealand to your home. Best souvenir ever.",Price=22.99,PathOfImage="gift018.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Pillow Cover",Description="All fabric edges are sewn with zigzag overlock stitch to prevent fray and ensure durability.",Price=36.99,PathOfImage="gift019.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Art Pillow Cover",Description=" Soft, Textured, made with color matching invisible zipper and allows easy insertion and removal of pillow inserts.",Price=31.99,PathOfImage="gift020.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Mari Jade",Description="Green Aventurine Jade Helps with sexual imbalances and grief caused by sexual abuse",Price=55.99,PathOfImage="gift021.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Mari Bag",Description="This is the perfect market tote you can use. You can carry from your groceries to your study books. Use the tote as a beach bag too! Our Style in Print designs are the perfect combination to personalize your totes.",Price=65.99,PathOfImage="gift022.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Mari Sculpture",Description="Incredibly detailed characteristics.",Price=75.99,PathOfImage="gift023.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Mari Sculpture",Description="Incredibly detailed characteristics.",Price=65.99,PathOfImage="gift024.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Jade Necklace",Description="Green Aventurine Jade Helps with sexual imbalances and grief caused by sexual abuse",Price=25.99,PathOfImage="gift025.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Mari Jade",Description="Green Aventurine Jade Helps with sexual imbalances and grief caused by sexual abuse",Price=35.99,PathOfImage="gift026.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Kiwi Lighter",Description="Bring New Zealand to your home. Best souvenir ever.",Price=29.99,PathOfImage="gift027.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Kiwi Toy",Description="Bring New Zealand to your home. Best souvenir ever.",Price=15.99,PathOfImage="gift028.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Jade Bracelet",Description="This Amber Bracelet is made on Elastic Band so it can slightly adjust in size.  This bracelet is packed together with a pouch so it can be a great gift for any occasion.",Price=35.99,PathOfImage="gift029.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            new Souvenir{Name="Jade Bracelet",Description="This Amber Bracelet is made on Elastic Band so it can slightly adjust in size.  This bracelet is packed together with a pouch so it can be a great gift for any occasion.",Price=39.99,PathOfImage="gift030.png",CategoryName="Maori Gifts",SupplierName="Maori Local"},
            //Hats
            new Souvenir{Name="Doggy Hat",Description="Yellow wool doggy hat, best gift for your little doggy.",Price=0.00,PathOfImage="hat001.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="White Hat",Description="White beautiful hat for travel.",Price=7.99,PathOfImage="hat002.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Brown Hat",Description="hat on sale",Price=9.99,PathOfImage="hat003.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Red Hat",Description="hat on sale",Price=15.99,PathOfImage="hat004.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Travel Hat",Description="hat on sale",Price=11.99,PathOfImage="hat005.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Green Hat",Description="hat on sale",Price=12.99,PathOfImage="hat006.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Green Hat",Description="hat on sale",Price=8.99,PathOfImage="hat007.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Catty Hat",Description="Not On sale",Price=0.00,PathOfImage="hat008.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Kids Hat",Description="hat on sale",Price=8.99,PathOfImage="hat009.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Mil Hat",Description="hat on sale",Price=9.99,PathOfImage="hat010.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Love Hat",Description="hat on sale",Price=11.99,PathOfImage="hat011.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Trip Hat",Description="hat on sale",Price=15.99,PathOfImage="hat012.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Lady Hat",Description="hat on sale",Price=18.99,PathOfImage="hat013.png",CategoryName="Hats",SupplierName="Natural Goods"},
            new Souvenir{Name="Sun Hat",Description="hat on sale",Price=8.99,PathOfImage="hat014.png",CategoryName="Hats",SupplierName="Natural Goods"},
            
            //MuSouvenir
            new Souvenir{Name="Coffe Cup",Description="Cup on Sale",Price=5.99,PathOfImage="cup001.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="White Cup",Description="Cup on Sale",Price=6.99,PathOfImage="cup002.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Lady Cup",Description="Cup on Sale",Price=6.99,PathOfImage="cup003.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Coffe Cup",Description="Cup on Sale",Price=5.99,PathOfImage="cup004.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Coffe Cup",Description="Cup on Sale",Price=4.99,PathOfImage="cup005.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Coffe Cups",Description="Cup on Sale",Price=12.99,PathOfImage="cup006.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Tea Cup",Description="Cup on Sale",Price=7.99,PathOfImage="cup007.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="White Cup",Description="Cup on Sale",Price=8.99,PathOfImage="cup008.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="White Cup",Description="Cup on Sale",Price=6.99,PathOfImage="cup009.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Gray Cup",Description="Cup on Sale",Price=8.99,PathOfImage="cup010.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Mug",Description="Cup on Sale",Price=7.99,PathOfImage="cup011.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="White Mug",Description="Cup on Sale",Price=9.99,PathOfImage="cup012.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="White Mug",Description="Cup on Sale",Price=8.99,PathOfImage="cup013.png",CategoryName="Mugs",SupplierName="Natural Goods"},
            new Souvenir{Name="Blue cup",Description="Cup on Sale",Price=6.99,PathOfImage="cup014.png",CategoryName="Mugs",SupplierName="Natural Goods"},
           
            //T-Souvenir
            new Souvenir{Name="Man's Color",Description="On Sale",Price=24.99,PathOfImage="t-shirt001.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Man",Description="On Sale",Price=24.99,PathOfImage="t-shirt002.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Woman",Description="On Sale",Price=24.99,PathOfImage="t-shirt003.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Man",Description="On Sale",Price=24.99,PathOfImage="t-shirt004.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Woman",Description="On Sale",Price=24.99,PathOfImage="t-shirt005.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Woman",Description="On Sale",Price=24.99,PathOfImage="t-shirt006.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Man",Description="On Sale",Price=24.99,PathOfImage="t-shirt007.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Man",Description="On Sale",Price=24.99,PathOfImage="t-shirt008.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Doddy",Description="Not On Sale",Price=0.00,PathOfImage="t-shirt009.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Man",Description="On Sale",Price=24.99,PathOfImage="t-shirt010.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="all Black",Description="On Sale",Price=34.99,PathOfImage="t-shirt011.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Girl",Description="On Sale",Price=18.99,PathOfImage="t-shirt012.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Girl",Description="On Sale",Price=18.99,PathOfImage="t-shirt013.png",CategoryName="T-Shirts",SupplierName="Natural Goods"},
            new Souvenir{Name="T-Shirt Man",Description="On Sale",Price=24.99,PathOfImage="t-shirt014.png",CategoryName="T-Shirts",SupplierName="Natural Goods"}
           
            };
            foreach (Souvenir p in souvenirs)
            {
                context.Souvenirs.Add(p);
            }
            context.SaveChanges();
        }
    }
}
