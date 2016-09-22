namespace Evolvinary.Helper{
    public class MathHelp{
        public static int floor(double value){
            var i = (int) value;
            return value < (double) i ? i-1 : i;
        }

        public static int ceil(double value){
            var i = (int) value;
            return value > (double) i ? i+1 : i;
        }
    }
}