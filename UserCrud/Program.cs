namespace UserCrud
{
    public class Program
    {
        static void Main(string[] args)
        {
            User user = new User(); 

            Productname person = new Productname()
            {
                Id = 1,
                Product = "akjfsdljf a",
                Title = "Title"
            };
            user.Add(person);
        }
    }
}