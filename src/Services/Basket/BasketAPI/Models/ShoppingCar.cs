namespace BasketAPI.Models
{
    public class ShoppingCar
    {
        //public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public List<ShoppingCarItem> Items { get; set; } = new();
        public double TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCar(string userName)
        {
            UserName = userName;
        }

        public ShoppingCar()
        {

        }
    }
}
