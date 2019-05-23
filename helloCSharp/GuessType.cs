﻿using System;
using System.Text.RegularExpressions;
namespace helloCSharp{

    public class GuessType{
        private string s = "";
        private double sVal = 0;
        private static readonly double dMax = double.MaxValue;
        private static readonly double dMin = double.MinValue;
        private static readonly float fMax = float.MaxValue;
        private static readonly float fMin = float.MinValue;
        private readonly Object type;

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
         * If str is pure digit ... its should be converted as number
         * If str contains digits but also not digit chars (except - and .)
         * then input is a string
         */
        private bool IsString(){
            if (Regex.IsMatch(s, "^[+-]?\\d+([.]\\d+)?$"))
                return false; //if is a pure number dont evaluate as string
            //number is contaminated so treat as string
            else return Regex.IsMatch(s, ".*([^\\d.]|[.]{2,}|([.].*[.]){1}).*"); 
        }

        private Object GuessNumericType(){
            sVal = Convert.ToDouble(s);

            if (s.Split('.').Length - 1 == 1){
                //One dot so check range to see what decimal type

                //check to see where the dot occurs
                int locOfDot = s.IndexOf('.');
                //check decimal places
                int decPlaces = s.Length - (locOfDot - 1);
                return IsDecimal(decPlaces);
            }
            else{ //can assume no dots (integer) because of regex in ToString()
                //if not byte check short
                //if not short check int
                //if not check for long ..if to big return string
                if (IsByte())
                    return byte.Parse(s);
                else if (IsShort())
                    return short.Parse(s);
                else if (IsInt())
                    return int.Parse(s);
                else if (IsLong())
                    return long.Parse(s);
                else return s; //too long to be Long
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
            return decimal.Parse(s);
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
