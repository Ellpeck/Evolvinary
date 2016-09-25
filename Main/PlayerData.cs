namespace Evolvinary.Main{
    public class PlayerData{
        public int MoneyCounter = 20000;

        public bool requestMoney(int amount, bool extract){
            if(this.MoneyCounter >= amount){
                if(extract){
                    this.MoneyCounter -= amount;
                }

                return true;
            }
            return false;
        }
    }
}