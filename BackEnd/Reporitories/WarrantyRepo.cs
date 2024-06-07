using swp391_sap1805_g6.Entities;

namespace swp391_sap1805_g6.Reporitories
{
    public class WarrantyRepo
    {
        private static BanhangContext? _context;
        public static Warranty? GetWarrantyByProductById(int id)
        {
            _context = new();
            return _context.Warranties.FirstOrDefault(x => x.ProductId == id);

        }

        public  static void Update(Warranty war)
        {
            _context = new();
              var warrUpdate = _context.Warranties.First(x => x.ProductId == war.WarrantyId);
            //map duration with warrantyInfo 
            warrUpdate.Duration = war.Duration;
            //1year warInfo=12 duration
            _context.SaveChanges();

        }

        public static List<Warranty>? GetAllWar()
        {

            using (var context = new BanhangContext())
            {
                var war = context.Warranties
                                       .Select(w => new Warranty
                                       {
                                           WarrantyId = w.WarrantyId,
                                           Duration = w.Duration,
                                           
                                       })
                                       .ToList();

                return war;
            }
        }
    }
}
