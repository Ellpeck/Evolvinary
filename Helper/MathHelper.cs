namespace Evolvinary.Helper{
    public class MathHelper{
        public static int floor(double value){
            var i = (int) value;
            return value < (double) i ? i-1 : i;
        }
    }
}