namespace Evolvinary.Main.Items{
    public class Item{
        private readonly string title;
        private readonly string description;

        public Item(string title, string description){
            this.title = title;
            this.description = description;
        }

        public string getTitle(){
            return this.title;
        }

        public string getDescription(){
            return this.description;
        }
    }
}