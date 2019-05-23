using System;
using System.Text.RegularExpressions;
namespace helloCSharp{

    public class GuessType{
        private string s = "";
        private double sVal = 0;
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
            return sVal >= byte.MinValue && sVal <= byte.MaxValue;
        }

        private bool IsChar(){
            //If the string has in 1 char and the first char is not a digit true
            return s.Length == 1 && !Char.IsDigit(s, 0);
        }

        private bool IsDouble(){
            return sVal >= dMin && sVal <= dMax;
        }

        private bool IsFloat(){
            //float.Maximum = 340282300000000000000000000000000000000

            //TODO:Fix runtime error
            //TODO: compare if numbers before decimal <= floatMax chars
            return sVal >= fMin && sVal <= fMax;
        }

        private bool IsInt(){
            return sVal >= int.MinValue && sVal <= int.MaxValue;
        }

        private bool IsLong(){
            return sVal >= long.MinValue && sVal <= long.MaxValue;
        }

        private bool IsShort(){
            return  sVal >= short.MinValue && sVal <= short.MaxValue;
        }

        /*
         * If a char in a string is a letter,
         * special char [!IsDigit], but not '.' (it may be a decimal),
         * then input is a string
         */
        private bool IsString(){
            return Regex.IsMatch(s, ".*[a-zA-Z].*") || !Regex.IsMatch(s, "\\d+");
        }

        private Object GuessNumericType(){
            sVal = Convert.ToDouble(s);
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
                locOfDot = s.IndexOf('.');
                //check decimal places
                int decPlaces = s.Length - (locOfDot - 1);
                return IsDecimal(decPlaces);
            }

            else if (numOfDots > 1){
                //2 or more dots so a string //this shouldnt occur because check for string is already made
                return s;
            }
            else{
                //no dots so integer
                //if not byte check short
                //if not short check int
                //if not int must be long
                if (IsByte())
                    return byte.Parse(s);
                else if (IsShort())
                    return short.Parse(s);
                else if (IsInt())
                    return int.Parse(s);
                else return long.Parse(s);
            }
        }

        private Object IsDecimal(int decimalPlaces){

            if (decimalPlaces > 16 && decimalPlaces <= 29){
                //cant be this precise if not a decimal
                return sVal;
            }

            else if (decimalPlaces > 7 && decimalPlaces <= 16){

                if (IsDouble()){
                    return double.Parse(s);
                }
                else{
                    //If exceeding double range must use something bigger
                    return sVal;
                }

            }
            else if (decimalPlaces < 7){
                if (IsFloat()){
                    return float.Parse(s);
                }
                else if (IsDouble()){
                    return double.Parse(s);
                }
                //last check for decimal is below

            }
            return new decimal.Parse(s);
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
