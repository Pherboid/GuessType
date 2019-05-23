using System;
namespace helloCSharp{

    public class GuessType{
        private string s = "";
        private decimal sVal = 0;
        private static readonly double dMax = double.MaxValue;
        private static readonly double dMin = double.MinValue;
        private static readonly float fMax = float.MaxValue;
        private static readonly float fMin = float.MinValue;
        private Object type = null;

        public GuessType(string str){
            s = str;

            if (s.Length == 0){
                type = new InvalidType();
            }
            else if (IsChar()){
                type = Convert.ToChar(s);
            }
            // a decimal can hold up to 79228162514264337593543950335 (29 digits)
            //and up up to 29 decimal places
            else if (s.Length > 58 || IsString()){
                type = s;
            }
            else{
                type = GuessNumericType();
            }

        }

        private bool IsByte(){
            return sVal >= byte.MinValue && sVal <= byte.MaxValue ? true : false;
        }

        private bool IsChar(){
            //If the string has in 1 char and the first char is not a digit true
            return s.Length == 1 && !Char.IsDigit(s, 0) ? true : false;
        }

        private bool IsDouble(){
            return sVal >= (decimal)dMin && sVal <= (decimal)dMax ? true : false;
        }

        private bool IsFloat(){
            //float.Maximum = 340282300000000000000000000000000000000

            //TODO:Fix runtime error
            //TODO: compare if numbers before decimal <= floatMax chars
            return sVal >= (decimal) fMin && sVal <= (decimal) fMax ? true:false;
        }

        private bool IsInt(){
            return sVal >= int.MinValue && sVal <= int.MaxValue ? true : false;
        }

        private bool IsLong(){
            return sVal >= long.MinValue && sVal <= long.MaxValue ? true : false;
        }

        private bool IsShort(){
            return  sVal >= short.MinValue && sVal <= short.MaxValue ? true : false;
        }

        /*
         * If a char in a string is a letter,
         * special char [!IsDigit], but not '.' (it may be a decimal),
         * then input is a string
         */
        private bool IsString(){
            foreach (char c in s){
                if (Char.IsLetter(c) || !Char.IsDigit(c) && c != '.' & c != '-'){
                    return true;
                }
            }
            return false;
        }


        private Object GuessNumericType(){
            sVal = Convert.ToDecimal(s);
            int numOfDots = 0;
            int locOfDot = 0;
            //Checking number of dots
            for (int i = 0; i < s.Length; i++){
                if (s[i] == '.'){
                    numOfDots++;
                }
            }

            if (numOfDots == 1){
                //One dot so check range to see what decimal type

                //check to see where the dot occurs
                for (int i = 0; i < s.Length; i++){
                    if (s[i] == '.'){
                        locOfDot = i;
                    }
                }

                int decPlaces = 0;
                for (int i = locOfDot + 1; i < s.Length; i++){
                    decPlaces++;
                }

                return IsDecimal(decPlaces);
            }

            else if (numOfDots > 1){
                //2 or more dots so a string
                return s;
            }
            else{
                //no dots so integer

                if (IsByte()){
                    return new byte();
                }
                if (IsInt()){
                    return new int();
                }
             
            }
            return 0;
        }

        private Object IsDecimal(int decimalPlaces){

            if (decimalPlaces > 16 && decimalPlaces <= 29){
                //cant be this precise if not a decimal
                return sVal;
            }

            else if (decimalPlaces > 7 && decimalPlaces <= 16){

                if (IsDouble()){
                    return new double();
                }
                else{
                    //If exceeding double range must use something bigger
                    return sVal;
                }

            }
            else if (decimalPlaces < 7){
                if (IsFloat()){
                    return new float();
                }
                else if (IsDouble()){
                    return new double();
                }
                //last check for decimal is below

            }
            return new Decimal();
        }

        public object ConvertToType(){
            return type;
        }

        //public string GetType(){

        //}

        //public object GetType(string str)
        //{

        //}
    }


}
